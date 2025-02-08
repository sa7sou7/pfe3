using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pfe3.Data;
using System.Linq;
using System.Threading.Tasks;

namespace pfe3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // POST: api/user/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid data.");

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.Role))
                {
                    var roleExist = await _roleManager.RoleExistsAsync(model.Role);
                    if (roleExist)
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                    }
                    else
                    {
                        return BadRequest($"Role {model.Role} does not exist.");
                    }
                }

                return Ok(new { user.Id, user.UserName, user.Email });
            }

            return BadRequest(result.Errors);
        }

        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid credentials.");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result)
            {
                // You can return a JWT or a simple confirmation message.
                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized("Invalid credentials.");
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(new { user.Id, user.UserName, user.Email });
        }
// PUT: api/user/update/{id}
[HttpPut("update/{id}")]
public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
{
    var user = await _userManager.FindByIdAsync(id);
    if (user == null)
        return NotFound("User not found.");

    user.UserName = model.UserName ?? user.UserName;
    user.Email = model.Email ?? user.Email;

    var result = await _userManager.UpdateAsync(user);
    if (result.Succeeded)
    {
        return Ok(new { user.Id, user.UserName, user.Email });
    }

    return BadRequest(result.Errors);
}


        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User deleted successfully." });
            }

            return BadRequest(result.Errors);
        }

        // GET: api/user/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();  // Get all users
            if (users == null || !users.Any())
                return NotFound("No users found.");

            var userList = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);  // Get roles for the user
                userList.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Roles = roles
                });
            }

            return Ok(userList);
        }
    }

    
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
