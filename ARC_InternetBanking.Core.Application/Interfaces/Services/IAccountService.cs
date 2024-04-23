using ARC_InternetBanking.Core.Application.ViewModels.User;
using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.Dtos.Account;

namespace ARC_InternetBanking.Application.Interfaces.Services
{

    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task UpdateUser(SaveUserViewModel user);
        Task<UserDto> GetUserByUserName(string UserName);
        Task<bool> IsaValidUser(string UserName);
        Task<List<UserDto>> GetAllUsers();
        Task ChangeUserStatus(string userName);
        Task<UserDto> UpdateUser(UserDto dto);
        Task UpdateUserByUserName(EditUserViewModel value);
        Task<UserDto> GetUserByUserEmail(string Email);

        Task<RegisterRequest> GetUserById(string UserId);


    }
}