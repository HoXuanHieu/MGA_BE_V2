using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace Web_API;

[Route("api/[controller]")]
[ApiController]
public class SecureController(SignInManager<UserEntity> sa, UserManager<UserEntity> um) : ControllerBase
{
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserManager<UserEntity> userManager;

    //public async Task<ActionResult> RegisterUser()
    //{
    //    string message = "";
    //    IdentityResult result = new();
    //    try
    //    {
    //        UserEntity user = new UserEntity()
    //        {

    //        };
    //    }

    //    catch (Exception ex)
    //    {

    //    }
    //}
}
