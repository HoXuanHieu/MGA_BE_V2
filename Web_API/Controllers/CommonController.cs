using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web_API;

public class CommonController : ControllerBase
{
    protected readonly IHttpContextAccessor _contextAccessor;
    protected readonly IUserService _userService;

    public CommonController(IUserService userService)
    {
        _userService = userService;
    }

}
