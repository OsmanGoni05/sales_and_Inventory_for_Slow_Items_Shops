using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sales_and_Inventory_for_Slow_Items_Shops.Constants;
using sales_and_Inventory_for_Slow_Items_Shops.data;
using sales_and_Inventory_for_Slow_Items_Shops.models;

namespace sales_and_Inventory_for_Slow_Items_Shops.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly ApplicationDbContext _context;
    public AuthenticationController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet("Profile")]
    public IActionResult Profile(int id)
    {
        dynamic? user = _context.User
             .Select(element => new
             {
                 element.Id,
                 element.FirstName,
                 element.LastName,
                 element.Role,
                 element.UserType,
                 element.MobileNumber,
                 element.IsLogedIn,
                 element.CreatedAt,
                 element.UpdatedAt
             }).FirstOrDefault();
        if (user is null) return BadRequest("User not exist.");
        return Ok(user);
    }
    [HttpGet("Option")]
    public IActionResult Option()
    {
        List<dynamic> elements = _context.Brands
            .Select(element => new
            {
                element.Id,
                Name = element.Name
            }).ToList<dynamic>();
        return Ok(elements);
    }//func


    [HttpPost("Registration")]
    public IActionResult Registration(RegistrationRequest request)
    {
        var oldUser = _context.User.FirstOrDefault(element => element.MobileNumber == request.MobileNumber);
        if (oldUser != null)
        {
            return Ok("Mobile number is already used. Try new one.");
        }//if
        User registration = _mapper.Map<User>(request);
        registration.IsLogedIn = true;
        _context.User.Add(registration);
        var result = _context.SaveChanges();
        return Ok(new
        {
            Status = ResponseMessage.SUCCESS_MESSAGE,
            id = registration.Id
        });
    }//func

    [HttpPost("LogIn")]
    public IActionResult LogIn(RegistrationRequest request)
    {
        var oldUser = _context.User.FirstOrDefault(element => element.MobileNumber == request.MobileNumber);
        if (oldUser == null)
        {
            return Ok("User not exist.");
        }
        else
        {
            if (oldUser.Password != request.Password)
            {
                return Ok("Username or password is wrong.");
            }
            else
            {

                User logIn = _mapper.Map<User>(request);
                logIn.IsLogedIn = true;
                _context.User.Update(logIn);
                var result = _context.SaveChanges();
                return Ok(new
                {
                    status = ResponseMessage.SUCCESS_MESSAGE,
                    id = logIn.Id
                });
            }
        }

    }

    [HttpPost("LogOut")]
    public IActionResult LogOut(int id)
    {
        var oldUser = _context.User.FirstOrDefault(element => element.Id == id);
        if (oldUser == null)
        {
            return BadRequest("User not exist.");
        }
        else
        {
            oldUser.IsLogedIn = false;
            _context.User.Update(oldUser);
            var result = _context.SaveChanges();
            return Ok(ResponseMessage.SUCCESS_MESSAGE);
        }

    }

    [HttpPost("ResetPassword")]
    public IActionResult ResetPassword(string newpassword, RegistrationRequest request)
    {
        var oldUser = _context.User.FirstOrDefault(element => element.MobileNumber == request.MobileNumber);
        if (oldUser != null)
        {
            if (request.Password == oldUser.Password)
            {
                oldUser.Password = newpassword;
                _context.User.Update(oldUser);
                var result = _context.SaveChanges();
                return Ok(ResponseMessage.SUCCESS_MESSAGE);
            }
            return BadRequest("Old password wrong.");
        }
        return BadRequest("User not found. ");

    }
}