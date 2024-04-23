using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.ViewModels.Beneficiarios;
using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using ARC_InternetBanking.Core.Application.ViewModels.Transacciones;
using ARC_InternetBanking.Core.Application.ViewModels.User;
using ARC_InternetBanking.Core.Domain.Entities;
using AutoMapper;

namespace ARC_InternetBanking.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region UserProfile

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region Users

            #region Transaccion
            CreateMap<Transaccion, SaveTransaccionViewModel>()
                .ForMember(x => x.ErrorMessage, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion

            #region CuentaAhorro

            CreateMap<CuentaAhorro, SaveCuentaAhorroViewModel>()
                 .ForMember(x => x.Users, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<CuentaAhorroViewModel, SaveCuentaAhorroViewModel>()
                .ForMember(x => x.Users, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.UserName, opt => opt.Ignore());

            CreateMap<CuentaAhorro, CuentaAhorroViewModel>()
            .ReverseMap()
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());




            #endregion

            #region Prestamo

            CreateMap<Prestamo, PrestamoViewModel>()
            .ForMember(x => x.UserName, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Prestamo, SavePrestamoViewModel>()
            .ReverseMap()
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion

            #region TarjetaCredito 

            CreateMap<TarjetaCredito, TarjetaCreditoViewModel>()
            .ForMember(x => x.UserName, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TarjetaCredito, SaveTarjetaCreditoViewModel>()
              .ReverseMap()
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion

            #region Beneficiario
            CreateMap<Beneficiario, BeneficiarioViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.LastModified, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Beneficiario, SaveBeneficiarioViewModel>()
                        .ReverseMap()
                        .ForMember(x => x.LastModified, opt => opt.Ignore())
                        .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                        .ForMember(x => x.Created, opt => opt.Ignore())
                        .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion
            #endregion

            #region Cuenta de Ahorro

            CreateMap<SaveCuentaAhorroViewModel, CuentaAhorro>()
                .ReverseMap();

            #endregion
        }
    }
}
