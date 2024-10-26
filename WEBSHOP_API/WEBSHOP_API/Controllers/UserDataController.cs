
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBSHOP_API.Database;
using WEBSHOP_API.DTOs;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Controllers
{ //TODO: Redo
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        
        private readonly UserDbContext _userContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IMapper _mapper;

        public UserDataController( UserDbContext userDb, UserManager<IdentityUser> userManager, ILogger<UserDataController> logger, IUserDataRepository userDataRepository, IMapper mapper)
        {
           
            _userContext = userDb;
            _userManager = userManager;
            _logger = logger;
            _userDataRepository = userDataRepository;
            _mapper = mapper;
        }

        // Post: api/Account/
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IdentityUser>> GetIdentityUserData()
        {
            var claims = HttpContext.User.Claims;
            var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //Console.WriteLine(uId);
            return await _userContext.Users.FindAsync(uId);
        }

        // POST: api/Account
        [Authorize (Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddUserToAdmins(AdminDTO adminDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(adminDTO.userEmail);
                await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation("Adding {email} to admins.", adminDTO.userEmail);
                return StatusCode(StatusCodes.Status200OK, "Succesfully added a new admin");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Adding {email} member to admins went wrong: {error}",adminDTO.userEmail, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding to admins record");
            }
           

        }
        //rewrite
        // GET: api/Account/5
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDataDTO>> GetUserDataInformation()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _userDataRepository.GetUserDataById(uId);
                return Ok(_mapper.Map<UserDataDTO>(result));

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
          
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateUserData(UserDataDTO userDataDTO)
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                userDataDTO.UserId = uId;
                var userData = _mapper.Map<UserData>(userDataDTO);
                await _userDataRepository.CreateUserData(userData);
                return StatusCode(StatusCodes.Status200OK, "Succesfully added user's data");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UpdateUserData(UserDataDTO userDataDTO)
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                userDataDTO.UserId = uId;
                var userData = _mapper.Map<UserData>(userDataDTO);
                await _userDataRepository.UpdateUserData(userData);
                return StatusCode(StatusCodes.Status200OK,"Succesfully updated user's data");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }

        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                await _userDataRepository.DeleteUserData(uId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Deleting a userData went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        
    }
}
