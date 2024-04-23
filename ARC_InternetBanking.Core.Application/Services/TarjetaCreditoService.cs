using ARC_InternetBanking.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Services;
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using ARC_InternetBanking.Core.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class TarjetaCreditoService : GenericService<SaveTarjetaCreditoViewModel, TarjetaCreditoViewModel, TarjetaCredito>, ITarjetaCreditoService
    {
        private readonly ITarjetaCreditoRepository _TarjetaCreditoRepository;
        private readonly IAccountService _accountServices;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TarjetaCreditoService(IHttpContextAccessor httpContextAccessor,
            ITarjetaCreditoRepository TarjetaCreditoRepository, 
            IMapper mapper, IAccountService accountServices) : base(TarjetaCreditoRepository, mapper)
        {
            _TarjetaCreditoRepository = TarjetaCreditoRepository;
            _mapper = mapper;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<List<TarjetaCreditoViewModel>> GetAllVMbyUserId()
        {
            var credictCardList = await _TarjetaCreditoRepository.GetAllAsync();
          
            credictCardList = credictCardList.Where(x => x.IdUsuario == userSession.Id).ToList();

            var TarjetaCreditosVMList = _mapper.Map<List<TarjetaCreditoViewModel>>(credictCardList);
            
            return TarjetaCreditosVMList;
        }

        public override async Task<List<TarjetaCreditoViewModel>> GetAllViewModel()
        {
            var list = await _TarjetaCreditoRepository.GetAllAsync();

            List<TarjetaCreditoViewModel> result = new List<TarjetaCreditoViewModel>();

            foreach (var item in list)
            {

                TarjetaCreditoViewModel vm = new TarjetaCreditoViewModel
                {
                    Id = item.Id,
                    IdUsario = item.IdUsuario,
                    Limite = item.Limite,
                    CantidadActual = item.CantidadActual,
                    NumeroProducto = item.NumeroProducto,
                    Deuda = item.Deuda
                    
                };

                var a = await _accountServices.GetUserById(item.IdUsuario);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }

        public async Task<SaveTarjetaCreditoViewModel> GetByCardNumber(string cardNumber)
        {
            return _mapper.Map<SaveTarjetaCreditoViewModel>(await _TarjetaCreditoRepository.GetByCardNumber(cardNumber));
        }


        public async Task<TarjetaCreditoViewModel> GetByCardIdentifyinNumber(string NumeroProducto)
        {
            var list = await GetAllViewModel();

            return list.FirstOrDefault(x => x.NumeroProducto == NumeroProducto);
        }
    }
}
