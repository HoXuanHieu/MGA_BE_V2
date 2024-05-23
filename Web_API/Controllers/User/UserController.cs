using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web_API;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var result = await _userService.GetAllUserAsync();
        return Ok(result);
    }


    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request);
        if (result)
        {
            return Ok("User Create Successful");
        }
        else
        {
            return StatusCode(500, "error");
        }
    }
}
