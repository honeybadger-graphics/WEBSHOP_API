using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{
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
        public async Task<ActionResult<string>> LoginRequest(Account Acc)
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
        public async Task<ActionResult<string>> CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccountInformation), new { id = account.AccountId }, account);
        }

        // GET: api/Account/5
        [HttpPost]
        public async Task<ActionResult<Account>> GetAccountInformation(Account account)
        {
            var existingAccount = await _context.Accounts.FindAsync(AccountId(account));

            if (existingAccount == null)
            {
                return NotFound();
            }

            return existingAccount;
        }

       
        // Change account escalator to something else for to use ID for escalated account or email cos it requires now a password.... shise
        [HttpPost]
        public async Task<ActionResult<string>> EscalateAccount(AccountEscalatorHelper accounts)
        {
            if (accounts.AccountAdmin.AccountName != null && AccountExists(accounts.AccountAdmin) &&
                accounts.AccountToEscalate.AccountName != null && AccountExists(accounts.AccountToEscalate))
            {
                var account = _context.Accounts.First(a => a.AccountId == AccountId(accounts.AccountAdmin));
                if (account.IsAdmin)
                {
                    var existingAccount = _context.Accounts.First(a => a.AccountId == AccountId(accounts.AccountToEscalate));

                    try
                    {
                        existingAccount.IsAdmin = true;
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
        public async Task<ActionResult<Account>> UpdateAccount(Account account)
        {
            if (account.AccountEmail != null && AccountExists(account))
            {
                var existingAccount = _context.Accounts.Find(AccountId(account));
                if(existingAccount != null) {
                    try
                    {
                        existingAccount.AccountPassword = account.AccountPassword;
                        existingAccount.AccountEmail = account.AccountEmail;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;

                    }
                    return account.ToJson();
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

        private bool AccountExists(Account account)
        {
            return _context.Accounts.Any(a => a.AccountEmail == account.AccountEmail);
        }

        private int AccountId(Account account)
        {
            var existAccount = _context.Accounts.First(a => a.AccountEmail == account.AccountEmail);
            return existAccount.AccountId;
        }
    }
}
