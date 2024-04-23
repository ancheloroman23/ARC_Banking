using ARC_InternetBanking.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Dtos.Email;
using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.Enums;
using ARC_InternetBanking.Core.Application.ViewModels.User;
using ARC_InternetBanking.Infrastructure.Identity.Entities;
using ARC_InternetBanking.Infrastructure.Shared.Service;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ARC_InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {

        #region Dependencias

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IEmailService emailService,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        #endregion

        

        public async Task<RegisterRequest> GetUserById(string UserId)
        {

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return null;
            }
            RegisterRequest UserDto = new();

            UserDto.Nombre = user.Nombre;
            UserDto.Apellido = user.Apellido;            
            UserDto.Email = user.Email;
            UserDto.UserName = user.UserName;            
            UserDto.Phone = user.PhoneNumber;
            UserDto.Cedula = user.Cedula;

            return UserDto;

        }


        public async Task UpdateUserByUserName(EditUserViewModel vm)
        {
            ApplicationUser value = await _userManager.FindByIdAsync(vm.UserId);

            value.Nombre = vm.Nombre;
            value.Apellido = vm.Apellido;
            value.Email = vm.Email;
            value.UserName = vm.Username;
            value.Cedula = vm.Cedula;            

            await _userManager.UpdateAsync(value);
        }


        public async Task<UserDto> UpdateUser(UserDto dto)
        {
            ApplicationUser value = await _userManager.FindByIdAsync(dto.UsuarioId);

            value.Nombre = dto.Nombre;
            value.Apellido = dto.Apellido;
            value.UserName = dto.UserName;
            value.Email = dto.Email;            
            value.Cedula = dto.Cedula;
            value.UserName = dto.UserName;
            value.PhoneNumber = dto.Phone;
            
            dto.UsuarioId = value.Id;

            await _userManager.UpdateAsync(value);

            return dto;
        }


        public async Task UpdateUser(SaveUserViewModel user)
        {
            var oldUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.UpdateAsync(oldUser);
        }


        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"Esta cuenta no esta registrada {request.Email}";
                return response;
            }

            request.Token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Error al restablecer clave";
                return response;
            }

            return response;
        }


        public async Task UpdatePassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            user.PasswordHash = request.Password;

        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Restablezca su cuenta visitando esta url {verificationUri}",
                Subject = "restablecer la Clave"
            });


            return response;
        }



        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No hay Cuenta con este Usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Cuenta registrada con {user.Email} puedes acceder";
            }
            else
            {
                return $"Error confirmando a {user.Email}.";
            }
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de usuario '{request.UserName}' esta en uso";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' esta en uso";
                return response;
            }

            var user = new ApplicationUser
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,                
                EmailConfirmed = true,
                Cedula = request.Cedula,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (request.IsAdmin)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
            }
            var userForId = await _userManager.FindByEmailAsync(request.Email);

            response.UserId = userForId.Id;
            return response;
        }


        public async Task<UserDto> GetUserByUserEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return null;
            }

            UserDto UserDto = new();

            UserDto.Nombre = user.Nombre;
            UserDto.Apellido = user.Apellido;            
            UserDto.Email = user.Email;
            UserDto.UserName = user.UserName;            
            UserDto.Phone = user.PhoneNumber;
            UserDto.Cedula = user.Cedula;

            return UserDto;
        }

        public async Task<UserDto> GetUserByUserName(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return null;
            }
            UserDto UserDto = new();

            UserDto.UsuarioId = user.Id;
            UserDto.Nombre = user.Nombre;
            UserDto.Apellido = user.Apellido;            
            UserDto.UserName = user.UserName;            
            UserDto.Phone = user.PhoneNumber;
            
            return UserDto;
        }

        public async Task<bool> IsaValidUser(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return false;
            }
            return true;
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuenta con {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"No es valido {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Cuenta no confirmada con {request.Email}";
                return response;
            }
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Cuenta inavilitada por {request.Email}. Hablar con el Admin ancheloroman23@email.com";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }


        public async Task ChangeUserStatus(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user.IsActive == true)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<List<UserDto>> GetAllUsers()
        {

            var userList = await _userManager.Users.ToListAsync();
            List<UserDto> UserDtoList = new();
            foreach (var user in userList)
            {
                var UserDto = new UserDto();

                UserDto.Nombre = user.Nombre;
                UserDto.Apellido = user.Apellido;
                UserDto.Email = user.Email;
                UserDto.UserName = user.UserName;                
                UserDto.Cedula = user.Cedula;
                UserDto.Phone = user.PhoneNumber;                                              
                UserDto.UsuarioId = user.Id;
                UserDto.IsActive = user.IsActive;
                UserDto.Roles = _userManager.GetRolesAsync(user).Result.ToList();
                UserDtoList.Add(UserDto);
            }
            return UserDtoList;
        }


        #region Private Methods

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }


        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        #endregion

    }
}
