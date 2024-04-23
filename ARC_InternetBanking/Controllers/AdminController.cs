
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARC_InternetBanking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly ITarjetaCreditoService _TarjetaCreditoService;
        private readonly IPrestamoService _PrestamoService;
        private readonly IProductoServices _productServices;
        

        public AdminController(IProductoServices productServices,
                               IUserService userService, 
                               ICuentaAhorroService CuentaAhorroService, 
                               ITarjetaCreditoService TarjetaCreditoService, 
                               IPrestamoService PrestamoService)
        {
            _userService = userService;
            _CuentaAhorroService = CuentaAhorroService;
            _TarjetaCreditoService = TarjetaCreditoService;
            _PrestamoService = PrestamoService;
            _productServices = productServices;

        }

        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllUsers();
            return View(list);
        }

        public async Task<IActionResult> DashBoard()
        {
           var statistics = await _productServices.GetDashBoard();
            return View(statistics);
        }

        public async Task<IActionResult> ChangeUserStatus(string userName)
        {
            await _userService.ChangeUserStatus(userName);
            var list = await _userService.GetAllUsers();
            return View("Index",list);
        }

        public async Task<IActionResult> UpdateFilter(string newFilter)
        {
            string filter;
            if (newFilter == "Admin")
            {
                filter = "Cliente";
            }
            else
            {
                filter = "Admin";
            }
            ViewBag.Filter = filter;
            var list = await _userService.GetAllUsers();
            return View("Index",list);
        }


        public async Task<IActionResult> CuentaAhorros()
        {
            var list = await _CuentaAhorroService.GetAllViewModel();
            await _userService.GetAllUsers();
            return View(list);
        }

        public async Task<IActionResult> TarjetaCreditos()
        {
            var list = await  _TarjetaCreditoService.GetAllViewModel() ;
            await _userService.GetAllUsers();
            return View(list);
        }

        public async Task<IActionResult> Prestamos()
        {
            var list = await _PrestamoService.GetAllViewModel();
            await _userService.GetAllUsers();
            return View(list);

        }
        public async Task<IActionResult> CreateCuentaAhorros()
        {
            SaveCuentaAhorroViewModel svm = new SaveCuentaAhorroViewModel();

            svm.Users = await _userService.GetAllUsers();
         
            
            return View(svm);
        }

        public async Task<IActionResult> CreateTarjetaCreditos()
        {
            SaveTarjetaCreditoViewModel svm = new SaveTarjetaCreditoViewModel();

            svm.users = await _userService.GetAllUsers();


            return View(svm);
        }

        public async Task<IActionResult> CreatePrestamos()
        {
            SavePrestamoViewModel svm = new SavePrestamoViewModel();

            svm.users = await _userService.GetAllUsers();

            return View(svm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCuentaAhorros(SaveCuentaAhorroViewModel svm)
        {
            if (svm.IdUsuario == "0")
            {
                svm.IdUsuario = null!;
            }

            if(!ModelState.IsValid || svm.IdUsuario == null)
            {
                SaveCuentaAhorroViewModel sv = new SaveCuentaAhorroViewModel();

                sv.Users = await _userService.GetAllUsers();
                return View(sv);
            }

            
            await _CuentaAhorroService.Add(svm);
            var list = await _CuentaAhorroService.GetAllViewModel();

            return View("CuentaAhorros", list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarjetaCreditos(SaveTarjetaCreditoViewModel svm)
        {
            if (svm.Limite <= 0|| svm.IdUsuario == "0")
            {
                svm.users = await _userService.GetAllUsers();
                return View(svm);
            }

            svm.CantidadActual = svm.Limite;

            await _TarjetaCreditoService.Add(svm);
            var list = await _TarjetaCreditoService.GetAllViewModel();

            return View("TarjetaCreditos", list);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrestamos(SavePrestamoViewModel svm)
        {
            if (svm.CantidaPrestamo <= 0 || svm.IdUsuario == "0")
            {

                svm.users = await _userService.GetAllUsers();

                return View(svm);
            }
            
            svm.CantidadPagada = 0;

            await _PrestamoService.Add(svm);
            var list = await _PrestamoService.GetAllViewModel();

            return View("Prestamos", list);
        }



        public async Task<IActionResult> DeleteCuentaAhorros(int id)
        {
            await _CuentaAhorroService.Delete(id);
            var list = await _CuentaAhorroService.GetAllViewModel();
            return View("CuentaAhorros", list);

        }

        public async Task<IActionResult> DeleteTarjetaCreditos(int id)
        {
            await _TarjetaCreditoService.Delete(id);
            var list = await _TarjetaCreditoService.GetAllViewModel();
            return View("TarjetaCreditos", list);

        }

        public async Task<IActionResult> DeletePrestamos(int id)
        {
            await _PrestamoService.Delete(id);
            var list = await _PrestamoService.GetAllViewModel();
            return View("Prestamos", list);

        }


    }
}
