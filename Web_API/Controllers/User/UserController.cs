using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Response;
using Service;
using Web_API.ResponseModel;

namespace Web_API;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var result = await _userService.GetAllUserAsync();
        return Ok(result);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _userService.CreateUserAsync(request);
        return StatusCode(response.Status, response);
    }

    [HttpGet]
    [Route("getbyid/{userId}")]
    public async Task<IActionResult> GetUserByIdAsync(String userId)
    {
        var response = await _userService.GetUserById(userId);
        return StatusCode(response.Status, response);
    }

    [HttpDelete]
    [Route("delete/{userId}")]
    public async Task<IActionResult> DeleteUserAsync(String userId)
    {
        var response = await _userService.DeleteUserAsync(userId);
        return StatusCode(response.Status, response);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserRequest request)
    {
        var response = await _userService.UpdateUserAsync(request);
        return StatusCode(response.Status, response);
    }

    [HttpPost]
    [Route("verify")]
    public async Task<IActionResult> VerifyUserAsync()
    {
        throw new NotImplementedException();
    }
}
