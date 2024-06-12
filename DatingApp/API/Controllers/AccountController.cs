using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class AccountController : BaseApiController
{
    private DataContext _context;
    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")]  // /api/account/register'
    public async Task<ActionResult<AppUser>> Register(string userName , string password)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = userName,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PassworSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

}
