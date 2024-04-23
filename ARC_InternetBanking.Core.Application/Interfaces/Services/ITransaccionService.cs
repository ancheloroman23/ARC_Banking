
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using ARC_InternetBanking.Core.Application.ViewModels.Transacciones;
using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITransaccionService : IGenericService<SaveTransaccionViewModel, TransaccionViewModel, Transaccion>
    {
        Task<SCPagoExpresoViewModel> AddExpressPayment(SaveTransaccionViewModel svm);
        Task ConfirmExpressPayment(SaveTransaccionViewModel svm);
        Task ConfirmBeneficiarioPayment(SaveTransaccionViewModel svm);
        Task<SavePrestamoViewModel> AddPrestamoPayment(SaveTransaccionViewModel svm);
        Task<SaveTransaccionViewModel> AddTransaccionBetween(SaveTransaccionViewModel svm);
        Task<SaveTarjetaCreditoViewModel> AddTarjetaCredito(SaveTransaccionViewModel svm);

        Task<SCPagoExpresoViewModel> PayToBeneficiaries(SaveTransaccionViewModel svm);
    }
}
