using ARC_InternetBanking.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Services;
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class PrestamoService : GenericService<SavePrestamoViewModel, PrestamoViewModel, Prestamo>, IPrestamoService
    {
        private readonly IPrestamoRepository _PrestamoRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountServices;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICuentaAhorroService _CuentaAhorroService;

        public PrestamoService(ICuentaAhorroService CuentaAhorroService,IHttpContextAccessor httpContextAccessor,IPrestamoRepository PrestamoRepository, IMapper mapper, IAccountService accountServices) : base(PrestamoRepository, mapper)
        {
            _PrestamoRepository = PrestamoRepository;
            _mapper = mapper;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _CuentaAhorroService = CuentaAhorroService;
        }

        public override async Task<List<PrestamoViewModel>> GetAllViewModel()
        {
            var list = await _PrestamoRepository.GetAllAsync();

            List<PrestamoViewModel> result = new List<PrestamoViewModel>();

            foreach (var item in list)
            {

                PrestamoViewModel vm = new PrestamoViewModel
                {
                    Id = item.Id,
                    UserName = item.IdUsuario,
                    CantidaPrestamo = item.CantidaPrestamo,
                    CantidadPagada = item.CantidadPagada,
                    NumeroProducto = item.NumeroProducto,
                    IdUsuario = item.IdUsuario
                };

                var a = await _accountServices.GetUserById(item.IdUsuario);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }

        public async Task<List<PrestamoViewModel>> GetAllVMbyUserId()
        {
            var PrestamoList = await _PrestamoRepository.GetAllAsync();

             PrestamoList = PrestamoList.Where(x => x.IdUsuario == userSession.Id).ToList();


            var PrestamoViewModels = _mapper.Map<List<PrestamoViewModel>>(PrestamoList);
            return PrestamoViewModels;
        }


        public async Task<PrestamoViewModel> GetByPrestamoINumber(string NumeroProducto)
        {
            var list = await GetAllViewModel();

            return list.FirstOrDefault(x => x.NumeroProducto == NumeroProducto);
        }

    }
}
