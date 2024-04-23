
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARC_InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductoServices _prodcutServices;

        public ClienteController(IUserService userService, IProductoServices prodcutServices)
        {
            _userService = userService;
            _prodcutServices = prodcutServices;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _prodcutServices.GetAllProducts();
            return View(product);
        }


    }
}
