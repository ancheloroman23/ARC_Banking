

using ARC_InternetBanking.Core.Application.ViewModels.Productos;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface IProductoServices
    {
        Task<ProductoViewModel> GetAllProducts();
        Task<EstadisticaDashBoard> GetDashBoard();
    }
}