using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.RetiroEfectivo;
using AutoMapper;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class RetiroEfectivoService : IRetiroEfectivoService
    {
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly ITarjetaCreditoService _TarjetaCreditoService;
        private readonly IMapper _mapper;

        public RetiroEfectivoService(ICuentaAhorroService CuentaAhorroService, ITarjetaCreditoService TarjetaCreditoService, IMapper mapper)
        {
            _CuentaAhorroService = CuentaAhorroService;
            _TarjetaCreditoService = TarjetaCreditoService;
            _mapper = mapper;
        }

        public async Task<SaveRetiroEfectivoViewModel> MakeAvance(SaveRetiroEfectivoViewModel model)
        {
            var credicard = await _TarjetaCreditoService.GetByCardNumber(model.NumeroTarjeta);
            var CuentaAhorro = await _CuentaAhorroService.GetVmByAccountNumber(model.DestinoCuenta);

            if(model.Cantidad > credicard.CantidadActual)
            {
                model.HasError = true;
                model.ErrorMessage = "ESTE MONTO PASA DEL MONTO DISPONIBLE";
                return model;
            }
            decimal cobrar = model.Cantidad + (model.Cantidad * 0.0625m);
            credicard.Deuda += cobrar;
            credicard.CantidadActual = credicard.CantidadActual - cobrar;
            CuentaAhorro.Cantidad += model.Cantidad;

            await _TarjetaCreditoService.Update(credicard, credicard.Id);
            await _CuentaAhorroService.Update(CuentaAhorro, CuentaAhorro.Id);

            return model;

            
        }
    }
}
