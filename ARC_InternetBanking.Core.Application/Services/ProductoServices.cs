using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.Productos;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class ProductoServices : IProductoServices
    {
        private readonly ITarjetaCreditoService _TarjetaCreditoService;
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly IPrestamoService _PrestamoService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransaccionRepository _TransaccionRepository;
        private readonly IUserService _userService;

        public ProductoServices(ITarjetaCreditoService TarjetaCreditoService, 
            ICuentaAhorroService CuentaAhorroService, 
            IPrestamoService PrestamoService, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor,
             ITransaccionRepository TransaccionRepository,
             IUserService userService)
        {
            _TransaccionRepository = TransaccionRepository;
            _TarjetaCreditoService = TarjetaCreditoService;
            _CuentaAhorroService = CuentaAhorroService;
            _PrestamoService = PrestamoService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        public async Task<EstadisticaDashBoard> GetDashBoard()
        {
            var credictCardList = await _TarjetaCreditoService.GetAllViewModel();
            var CuentaAhorroList = await _CuentaAhorroService.GetAllViewModel();
            var PrestamoList = await _PrestamoService.GetAllViewModel();
            var TransaccionsList = await _TransaccionRepository.GetAllAsync();
            var dayTransaccions = TransaccionsList.Where(x => x.Fecha == DateTime.Today).ToList();
            var paymentList = TransaccionsList.Where(x => x.IdTipoTransaccion == 1 || x.IdTipoTransaccion == 2 || x.IdTipoTransaccion == 3 || x.IdTipoTransaccion == 4).ToList();
            var dayPayment = paymentList.Where(x => x.Fecha == DateTime.Today).ToList();
            var userList = await _userService.GetAllUsers();
            var clientList = userList.Where(x => x.Roles.Contains("Cliente")).ToList();
            var activeClient = clientList.Where(x => x.IsActive == true).ToList();
            var inActiveClient = clientList.Where(x => x.IsActive == false).ToList();


            var dashBoardStatitics = new EstadisticaDashBoard
            {

                TotalTransacciones = TransaccionsList.Count,
                TransaccionDiarias = dayTransaccions.Count,
                TotalPagos = paymentList.Count,
                PagoDiarios = dayPayment.Count,
                TotalClientesActivos = activeClient.Count,
                TotalClientesInactivos = inActiveClient.Count,
                TarjetasCreditoTotal = credictCardList.Count,
                CuentasAhorroTotal = CuentaAhorroList.Count,
                PrestamoTotal = PrestamoList.Count,
                ProductosTotal = credictCardList.Count + CuentaAhorroList.Count + PrestamoList.Count
           };

            return dashBoardStatitics;

        }
      
        public async Task<ProductoViewModel> GetAllProducts()
        {
            var credictCardList = await _TarjetaCreditoService.GetAllVMbyUserId();
            var CuentaAhorroList = await _CuentaAhorroService.GetAllVMbyUserId();
            var PrestamoList = await _PrestamoService.GetAllVMbyUserId();

            
            var product = new ProductoViewModel
            {
                TarjetaCreditos = credictCardList,
                CuentaAhorros = CuentaAhorroList,
                Prestamos = PrestamoList
            };

            return product;
        }

    }
}
