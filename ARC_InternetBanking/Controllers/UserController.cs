using Microsoft.AspNetCore.Mvc;
using ARC_InternetBanking.ARC_InternetBanking.Middlewares;
using Microsoft.AspNetCore.Authorization;
using ARC_InternetBanking.Core.Application.ViewModels.User;
using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Helpers;


namespace WebApp.ARC_InternetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICuentaAhorroService _CuentaAhorroService;

        public UserController(IUserService userService, 
                              ICuentaAhorroService CuentaAhorroService)
        {
            _userService = userService;
            _CuentaAhorroService = CuentaAhorroService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                //Hay un ERROR
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                if (userVm.Roles.Contains("Cliente"))
                {
                    return RedirectToRoute(new { controller = "Cliente", action = "Index" });
                }
                return RedirectToRoute(new { controller = "Admin", action = "Dashboard" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            var origin = Request.Headers["origin"];

            RegisterResponse response = new();

            if (ModelState.IsValid)
            {
                response = await _userService.RegisterAsync(vm, origin);
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }

            return View(vm);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordViewModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            
            if (!String.IsNullOrEmpty(vm.Email) || !String.IsNullOrEmpty(vm.Password) || !String.IsNullOrEmpty(vm.ConfirmPassword))
            {
                return View(vm);
            }
            
            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

            public async Task<IActionResult> UpdateUser(string userId)
            {
                var user = await _userService.GetUserDtoAsync(userId);
                var editUser = new EditUserViewModel()
                {
                    Cedula = user.Cedula,
                    Email = user.Email,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Phone = user.Phone,
                    Username = user.UserName,
                };
            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel vm)
        {
            UserDto value = new();


            value.Cedula = vm.Cedula;
            value.Phone = vm.Phone;
            value.Email = vm.Email;
            value.Nombre = vm.Nombre;
            value.Apellido = vm.Apellido;
            value.UserName = vm.Username;
            value.UsuarioId = vm.UserId;

            await _userService.UpdateUserByUserId(value);
            return RedirectToRoute(new { controller = "Admin", action = "Dashboard" });
        }


        public async Task<IActionResult> UpdateClient(string userId)
            {
               var user = await _userService.GetUserDtoAsync(userId);
                var editUser = new EditUserViewModel()
                {
                    Cedula = user.Cedula,
                    Email = user.Email,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Phone = user.Phone,
                    Username = user.UserName,
                };
                return View(editUser);
            }

            [HttpPost]
            public async Task<IActionResult> UpdateClient(EditUserViewModel vm)
            {
                UserDto value = new();
            
                value.Cedula= vm.Cedula;
                value.Phone= vm.Phone;
                value.Email= vm.Email;
                value.Nombre = vm.Nombre;
                value.Apellido= vm.Apellido;
                value.UsuarioId = vm.Username;
                value.UsuarioId = vm.UserId;

            await _CuentaAhorroService.AddAmountToCuentaAhorro(value.UsuarioId, vm.MontoInicial);
              var user = await _userService.UpdateUserByUserId(value);

            return RedirectToRoute(new {controller = "Admin", action= "Dashboard" });
            }
    }
}

