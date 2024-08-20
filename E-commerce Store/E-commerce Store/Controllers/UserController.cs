using E_Commerce_Store.DTO;
using E_Commerce_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Store.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CommerceDbContext _context;

        public UserController(CommerceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Name)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.Geolocation)
                .ToListAsync();

            var userDtos = users.Select(user => new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                Phone = user.Phone,
                Name = new NameDto
                {
                    FirstName = user.Name.FirstName,
                    LastName = user.Name.LastName
                },
                Addresses = user.Addresses.Select(address => new AddressDto
                {
                    City = address.City,
                    Street = address.Street,
                    Number = address.Number,
                    Zipcode = address.Zipcode,
                    Geolocation = new GeolocationDto
                    {
                        Lat = address.Geolocation.lat,
                        Lon = address.Geolocation.lon
                    }
                }).ToList()
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Name)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.Geolocation)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                Phone = user.Phone,
                Name = new NameDto
                {
                    FirstName = user.Name.FirstName,
                    LastName = user.Name.LastName
                },
                Addresses = user.Addresses.Select(address => new AddressDto
                {
                    City = address.City,
                    Street = address.Street,
                    Number = address.Number,
                    Zipcode = address.Zipcode,
                    Geolocation = new GeolocationDto
                    {
                        Lat = address.Geolocation.lat,
                        Lon = address.Geolocation.lon
                    }
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Phone = userDto.Phone,
                Name = new Name
                {
                    FirstName = userDto.Name.FirstName,
                    LastName = userDto.Name.LastName
                },
                Addresses = userDto.Addresses.Select(addressDto => new Address
                {
                    City = addressDto.City,
                    Street = addressDto.Street,
                    Number = addressDto.Number,
                    Zipcode = addressDto.Zipcode,
                    Geolocation = new Geolocation
                    {
                        lat = addressDto.Geolocation.Lat,
                        lon = addressDto.Geolocation.Lon
                    }
                }).ToList()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {

            var existingUser = await _context.Users
                .Include(u => u.Name)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.Geolocation)
                .FirstOrDefaultAsync(u => u.UserId == userDto.UserId);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Email = userDto.Email;
            existingUser.UserName = userDto.UserName;
            existingUser.Password = userDto.Password;
            existingUser.Phone = userDto.Phone;

            if (existingUser.Name != null && userDto.Name != null)
            {
                existingUser.Name.FirstName = userDto.Name.FirstName;
                existingUser.Name.LastName = userDto.Name.LastName;
            }

            if (existingUser.Addresses != null && userDto.Addresses != null && userDto.Addresses.Count > 0)
            {

                existingUser.Addresses.Clear();

                foreach (var addressDto in userDto.Addresses)
                {
                    var address = new Address
                    {
                        City = addressDto.City,
                        Street = addressDto.Street,
                        Number = addressDto.Number,
                        Zipcode = addressDto.Zipcode,
                        Geolocation = new Geolocation
                        {
                            lat = addressDto.Geolocation.Lat,
                            lon = addressDto.Geolocation.Lon
                        }
                    };
                    existingUser.Addresses.Add(address);
                }
            }

            _context.Entry(existingUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userDto.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Name)
                .Include(u => u.Addresses)
                    .ThenInclude(a => a.Geolocation)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Name != null)
            {
                _context.Names.Remove(user.Name);
            }

            if (user.Addresses != null && user.Addresses.Count > 0)
            {
                _context.Addresses.RemoveRange(user.Addresses);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
