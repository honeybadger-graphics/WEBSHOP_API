using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ServiceStack.Logging;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{ //TODO: Redo
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WebshopDbContext _context;

        public AccountController(WebshopDbContext context)
        {
            _context = context;
        }

        // Post: api/Account/
        [HttpPost]
        public async Task<ActionResult> LoginRequest(LoginCreds Acc)
        {
            if (AccountExists(Acc))
            {
                var account = await _context.Accounts.FindAsync(AccountId(Acc));

                if (account != null)
                {
                    return Ok();
                   
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
           
        }
        // POST: api/Account
        [HttpPost]
        public async Task<ActionResult<string>> CreateAccount(LoginCreds login) //modify the return....
        {
            if (!AccountExists(login))
            {
                Account account = new()
                {
                    AccountEmail = login.Email,
                    AccountPassword = login.Password
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                Cart cart = new()
                {
                    CartId = AccountId(account),
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
               return BadRequest();
            }
            
        }

        // GET: api/Account/5
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

       
        [HttpPost]
        public async Task<ActionResult> EscalateAccount(AccountEscalatorHelper accounts)
        {
            if (accounts.Admin.Email != null && AccountExists(accounts.Admin) && AccountExists(accounts.AccountToEscalateId))
            {
                var account = await _context.Accounts.FindAsync(AccountId(accounts.Admin));
                if (account.IsAdmin)
                {
                    var existingAccount = await _context.Accounts.FindAsync(accounts.AccountToEscalateId);

                    try
                    {
                        existingAccount.IsAdmin = true;  // not going to be null cos AccountExists(accounts.Admin) !!
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
                    return NoContent();
                }

            }
            else
            {
                return NoContent();
            }

        }

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

        // DELETE: api/Account/5
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
