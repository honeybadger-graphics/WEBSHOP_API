
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ServiceStack.Logging;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Claims;
using WEBSHOP_API.Database;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{ //TODO: Redo
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WebshopDbContext _context;
        private readonly UserDbContext _userContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        public AccountController(WebshopDbContext context, UserDbContext userDb, UserManager<User> userManager, ILogger<AccountController> logger )
        {
            _context = context;
            _userContext = userDb;
            _userManager = userManager;
            _logger = logger;
        }

        // Post: api/Account/
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserData()
        {
            var claims = HttpContext.User.Claims;
            string uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(uId);
            return await _userContext.Users.FindAsync(uId);
        }

        // POST: api/Account
        [Authorize (Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddUserToAdmins(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation("Adding {email} to admins.", userEmail);
                return StatusCode(StatusCodes.Status200OK, "Succesfully added a new admin");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Adding {email} member to admins went wrong: {error}",userEmail, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding to admins record");
            }
           

        }

        // GET: api/Account/5
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Account>> GetAccountInformation(LoginCreds account)
        {
            var existingAccount = await _context.Accounts.FindAsync(AccountId(account));
            existingAccount.Cart = await _context.Carts.FindAsync(existingAccount.AccountId);

            if (existingAccount == null)
            {
                return NotFound();
            }

            return existingAccount;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Account>> UpdateAccount(Account accountToUpdate)
        {
            if (accountToUpdate.AccountEmail != null && AccountExists(accountToUpdate))
            {

                if (await _context.Accounts.FindAsync(AccountId(accountToUpdate)) is Account existingAccount)
                {
                    try
                    {
                        accountToUpdate.AccountId = existingAccount.AccountId;
                        accountToUpdate.AccountEmail = existingAccount.AccountEmail;
                        accountToUpdate.Cart = existingAccount.Cart;
                        _context.Entry(existingAccount).CurrentValues.SetValues(accountToUpdate);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;

                    }
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(LoginCreds account)
        {
            return _context.Accounts.Any(a => a.AccountEmail == account.Email);
        }
        private bool AccountExists(Account account)
        {
            return _context.Accounts.Any(a => a.AccountEmail == account.AccountEmail);
        }
        private bool AccountExists(int accountID)
        {
            return _context.Accounts.Any(a => a.AccountId == accountID);
        }

        private int AccountId(LoginCreds account)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.AccountEmail == account.Email && a.AccountPassword == account.Password);
            if (existAccount != null)
            {
                return existAccount.AccountId;
            }
            else
            {
                return -1;
            }

        }
        private int AccountId(Account account)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.AccountEmail == account.AccountEmail && a.AccountPassword == account.AccountPassword);
            if (existAccount != null)
            {
                return existAccount.AccountId;
            }
            else
            {
                return -1;
            }

        }
    }
}
