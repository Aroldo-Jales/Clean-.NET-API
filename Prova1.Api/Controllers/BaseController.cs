using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : Controller
{
    public readonly Guid userid;

    public BaseController()
    {
        //userid = User.Identity
    }
}