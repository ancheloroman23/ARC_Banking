using ARC_InternetBanking.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.Beneficiarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARC_InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class BeneficiarioController : Controller
    {
        private readonly IBeneficiarioService _service;
        private readonly IAccountService _accountService;
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly IUserService _userService;

        public BeneficiarioController(IAccountService accountService, 
                                      ICuentaAhorroService CuentaAhorroService, 
                                      IBeneficiarioService service, 
                                      IUserService userService)
        {
            _accountService = accountService;
            _CuentaAhorroService = CuentaAhorroService;
            _service = service;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetBeneficiryByUserId();
            return View(list);
        }

        public IActionResult AddBeneficiario()
        {
            var viewModel = new SaveBeneficiarioViewModel();
            return View(viewModel);
        }

            [HttpPost]
            public async Task<IActionResult> AddBeneficiario(string viewModel)
            {
                SaveBeneficiarioViewModel vm = new();
                vm.IdProducto = viewModel;

            if (!string.IsNullOrEmpty(viewModel) && viewModel.Length == 9)
                {
                    try
                    {
                        var addedBeneficiario = await _service.Add(vm);
                       if(addedBeneficiario.HasError == true)
                        {
                            var list = await _service.GetBeneficiryByUserId();
                        ViewBag.Message = vm.ErrorMessage;
                        return View("Index",list);
                        }
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Hubo un error al agregar el beneficiario: " + ex.Message);
                    }
                }

                ViewBag.Message = "No se pudo agregar al beneficiario";
                var listBeneficiario = await _service.GetBeneficiryByUserId();
                return View("Index", listBeneficiario);
            }

            public async Task<IActionResult> DeleteBeneficiario(int ID)
            {
                await _service.Delete(ID);
                return RedirectToAction("Index");
            }
        }
    } 

