using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.ViewModels.User;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task UpdateUser(SaveUserViewModel user);
        Task<RegisterRequest> GetUserDtoAsync(string UserName);
        Task<bool> IsaValidUser(string UserName);
        Task<List<UserDto>> GetAllUsers();
        Task ChangeUserStatus(string userName);
        Task<UserDto> UpdateUserByUserId(UserDto dto);
    }
}