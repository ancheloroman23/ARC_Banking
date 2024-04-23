using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.Transacciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class PaymentController : Controller
    {
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly ITransaccionService _TransaccionService;
        private readonly IPrestamoService _PrestamoService;
        private readonly ITarjetaCreditoService _TarjetaCreditoService;
        private readonly IBeneficiarioService _BeneficiarioService;

        public PaymentController(ICuentaAhorroService CuentaAhorroService, 
                                 ITransaccionService TransaccionService, 
                                 IPrestamoService PrestamoService, 
                                 ITarjetaCreditoService TarjetaCreditoService, 
                                 IBeneficiarioService BeneficiarioService)
        {
            _CuentaAhorroService = CuentaAhorroService;
            _TransaccionService = TransaccionService;
            _PrestamoService = PrestamoService;
            _TarjetaCreditoService = TarjetaCreditoService;
            _BeneficiarioService = BeneficiarioService;
        }

        #region Express
        public async Task<IActionResult> PaymentExpress()
        {
            ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
            return View(new SaveTransaccionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentExpress(SaveTransaccionViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(new SaveTransaccionViewModel());
            }

            var paymentExpress = await _TransaccionService.AddExpressPayment(svm);
            if (paymentExpress.SaveTransaccionViewModel.HasError)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(paymentExpress.SaveTransaccionViewModel);
            }

            return View("ConfirmPaymentExpress", paymentExpress);
        }

        public IActionResult ConfirmPaymentExpress(SCPagoExpresoViewModel ConfirmVM)
        {
            if(!ModelState.IsValid)
            {
                return View(new SCPagoExpresoViewModel());
            }

            return View(ConfirmVM);

        }

       

        public async Task<IActionResult> MakePaymentExpress(string AccountHolderId, 
            string OriginAccount,
            string DestinationAccount,
            decimal Amount, string Description, 
            int TransaccionTypeId)
        {
            var saveTansactionVM = new SaveTransaccionViewModel
            {
                OriginNumeroCuenta = OriginAccount,
                DestinoNumeroCuenta = DestinationAccount,
                Cantidad = Amount,
               Descriptcion = Description,
                IdTipoTransaccion = TransaccionTypeId,
                IdUsuarioTitularCuenta = AccountHolderId
            };

            await _TransaccionService.ConfirmExpressPayment(saveTansactionVM);
            return RedirectToRoute(new { controller = "Cliente", action = "Index" });
        }
        #endregion

        public async Task<IActionResult> PaymentBeneficiario()
        {
            ViewBag.Beneficiaries = await _BeneficiarioService.GetBeneficiryByUserId();
            ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
            return View(new SaveTransaccionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentBeneficiario(SaveTransaccionViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                ViewBag.Beneficiaries = await _BeneficiarioService.GetBeneficiryByUserId();
                return View(new SaveTransaccionViewModel());
            }
          
            var paymentExpress = await _TransaccionService.PayToBeneficiaries(svm);
            if (paymentExpress.SaveTransaccionViewModel.HasError)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                ViewBag.Beneficiaries = await _BeneficiarioService.GetBeneficiryByUserId();
                return View(paymentExpress);
            }

            return View("ConfirmBeneficiarioExpress", paymentExpress);
        }

        public IActionResult ConfirmBeneficiarioExpress(SCPagoExpresoViewModel ConfirmVM)
        {
            if (!ModelState.IsValid)
            {
                return View(ConfirmVM);
            }

            return View(ConfirmVM);
        }

        public async Task<IActionResult> MakeBeneficiarioExpress(string AccountHolderId,
            string OriginAccount,
            string DestinationAccount,
            decimal Amount, string Description,
            int TransaccionTypeId)
        {
            var saveTansactionVM = new SaveTransaccionViewModel
            {
                OriginNumeroCuenta = OriginAccount,
                DestinoNumeroCuenta = DestinationAccount,
                Cantidad = Amount,
                Descriptcion = Description,
                IdTipoTransaccion = TransaccionTypeId,
                IdUsuarioTitularCuenta = AccountHolderId
            };
          
            await _TransaccionService.ConfirmBeneficiarioPayment(saveTansactionVM);
            return RedirectToRoute(new { controller = "Cliente", action = "Index" });
        }


        #region TarjetaCredito

        public async Task<IActionResult> PaymentTarjetaCredito()
        {
            ViewBag.TarjetaCreditos = await _TarjetaCreditoService.GetAllVMbyUserId();
            ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
            return View(new SaveTransaccionViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> PaymentTarjetaCredito(SaveTransaccionViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TarjetaCreditos = await _TarjetaCreditoService.GetAllVMbyUserId();
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(new SaveTransaccionViewModel());
            }

            var paymentCard = await _TransaccionService.AddTarjetaCredito(svm);
            if (paymentCard.SaveTransaccionViewModel.HasError)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                ViewBag.TarjetaCreditos = await _TarjetaCreditoService.GetAllVMbyUserId();
                return View(paymentCard.SaveTransaccionViewModel);
            }

            return RedirectToRoute(new { controller = "Cliente", action = "Index" });
        }

        #endregion

        public async Task<IActionResult> PaymentPrestamo()
        {
            ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
            ViewBag.Prestamos = await _PrestamoService.GetAllVMbyUserId();
            return View(new SaveTransaccionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentPrestamo(SaveTransaccionViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                ViewBag.Prestamos = await _PrestamoService.GetAllVMbyUserId();
                return View(new SaveTransaccionViewModel());
            }

            var paymentPrestamo = await _TransaccionService.AddPrestamoPayment(svm);
            if (paymentPrestamo.SaveTransaccionViewModel.HasError )
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                ViewBag.Prestamos = await _PrestamoService.GetAllVMbyUserId();
                return View(paymentPrestamo.SaveTransaccionViewModel);
            }

            return RedirectToRoute(new { controller = "Cliente", action = "Index" });
        }

        public async Task<IActionResult> Transaccion()
        {
            ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
            return View(new SaveTransaccionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Transaccion(SaveTransaccionViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(new SaveTransaccionViewModel());
            }

            var Transaccion = await _TransaccionService.AddTransaccionBetween(svm);
            if (Transaccion.HasError)
            {
                ViewBag.CuentaAhorros = await _CuentaAhorroService.GetAllVMbyUserId();
                return View(svm);
            }

            return RedirectToRoute(new { controller = "Cliente", action = "Index" });
        }
    }
}
