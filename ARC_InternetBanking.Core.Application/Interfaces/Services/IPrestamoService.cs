
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPrestamoService : IGenericService<SavePrestamoViewModel, PrestamoViewModel, Prestamo>
    {
        Task<List<PrestamoViewModel>> GetAllVMbyUserId();
        Task<PrestamoViewModel> GetByPrestamoINumber(string NumeroProducto);
    }
}
