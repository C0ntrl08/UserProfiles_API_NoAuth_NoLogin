using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProfiles_API.Databases;
using UserProfiles_API.DataTransferObjects;
using UserProfiles_API.Models;

namespace UserProfiles_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly UserProfilesDbContext _context;

        public UserProfilesController(UserProfilesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            List<UserProfileReadDto> users = _context.UserProfileData
                .Select(u => new UserProfileReadDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateOfBirth = u.DateOfBirth,
                }).ToList();

            if (users.Count == 0)
            {
                return NotFound($"There are {users.Count} user profiles in the database.");
            }

            return Ok(users);

        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            UserProfileReadDto? user = _context.UserProfileData
                .Where(u => u.UserId == id)
                .Select(u => new UserProfileReadDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateOfBirth = u.DateOfBirth,
                })
                .FirstOrDefault();

            if (user is null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] UserProfileCreateDto userDto)
        {
            // Validate the input data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserProfile user = new UserProfile
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            _context.UserProfileData.Add(user);
            _context.SaveChanges();

            // Return HTTP 201 - Created
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);

        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] UserProfileUpdateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserProfile? user = _context.UserProfileData.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            _context.UserProfileData.Attach(user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return Conflict($"An error occurred while updating the user: {ex.Message}");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            UserProfile? user = _context.UserProfileData.FirstOrDefault(u => u.UserId == id);

            if (user is null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            _context.UserProfileData.Remove(user);
            _context.SaveChanges(true);

            // HTTP - 204
            return NoContent();
        }
    }
}
