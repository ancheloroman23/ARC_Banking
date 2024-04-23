using ARC_InternetBanking.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Application.ViewModels.User;
using AutoMapper;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ICuentaAhorroService _CuentaAhorroService;

        public UserService(IAccountService accountService, IMapper mapper, ICuentaAhorroService CuentaAhorroService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _CuentaAhorroService = CuentaAhorroService;
        }

        public async Task ChangeUserStatus(string userName)
        {
            await _accountService.ChangeUserStatus(userName);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task UpdateUser(SaveUserViewModel user)
        {
            await _accountService.UpdateUser(user);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {

            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            var response = await _accountService.RegisterBasicUserAsync(registerRequest, origin);

            if (!vm.IsAdmin && !response.HasError)
            {
                await _CuentaAhorroService.Add(new SaveCuentaAhorroViewModel
                {
                    Cantidad = vm.MontoInicial,
                    EsPrincipal = true,
                    IdUsuario = response.UserId,

                });
            }

            return response;

        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        public async Task<RegisterRequest> GetUserDtoAsync(string userId)
        {
            return await _accountService.GetUserById(userId);
        }

        public async Task<bool> IsaValidUser(string UserName)
        {
            return await _accountService.IsaValidUser(UserName);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
           return await _accountService.GetAllUsers();
        }

        public async Task<UserDto> UpdateUserByUserId(UserDto dto)
        {
            return await _accountService.UpdateUser(dto);
        }
    }
}
