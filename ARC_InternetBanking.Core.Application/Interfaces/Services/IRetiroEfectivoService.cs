

using ARC_InternetBanking.Core.Application.ViewModels.RetiroEfectivo;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface IRetiroEfectivoService
    {
        Task<SaveRetiroEfectivoViewModel> MakeAvance(SaveRetiroEfectivoViewModel model);
    }
}
