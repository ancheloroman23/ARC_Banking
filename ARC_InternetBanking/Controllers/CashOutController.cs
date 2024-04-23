using Microsoft.AspNetCore.Mvc;
using ARC_InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using ARC_InternetBanking.Core.Application.ViewModels.RetiroEfectivo;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;

namespace ARC_InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class CashOutController : Controller
    {
        private readonly IRetiroEfectivoService _avancedeEfectivo;
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly ITarjetaCreditoService _TarjetaCreditoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? user;

        public CashOutController(IRetiroEfectivoService avancedeEfectivo, 
                                 ICuentaAhorroService CuentaAhorroService, 
                                 ITarjetaCreditoService TarjetaCreditoService, 
                                 IHttpContextAccessor httpContextAccessor)
        {
            _avancedeEfectivo = avancedeEfectivo;
            _CuentaAhorroService = CuentaAhorroService;
            _TarjetaCreditoService = TarjetaCreditoService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task< IActionResult> Index()
        {
            ViewBag.TarjetaCredito = await _TarjetaCreditoService.GetAllVMbyUserId();
            ViewBag.Account = await _CuentaAhorroService.GetAllVMbyUserId();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SaveRetiroEfectivoViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.TarjetaCredito = await _TarjetaCreditoService.GetAllVMbyUserId();
                ViewBag.Account = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(model);
            }
            var avance = await _avancedeEfectivo.MakeAvance(model);
            if (avance.HasError)
            {
                ViewBag.TarjetaCredito = await _TarjetaCreditoService.GetAllVMbyUserId();
                ViewBag.Account = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(model);
            }
            return RedirectToRoute(new { controller = "Cliente", action = "Index" });
        }
    }
}
