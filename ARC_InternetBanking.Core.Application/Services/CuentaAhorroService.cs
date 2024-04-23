using ARC_InternetBanking.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Services;
using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class CuentaAhorroService : GenericService<SaveCuentaAhorroViewModel, CuentaAhorroViewModel, CuentaAhorro>, ICuentaAhorroService
    {
        private readonly ICuentaAhorroRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountServices;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CuentaAhorroService(IHttpContextAccessor httpContextAccessor,
            ICuentaAhorroRepository repository,
            IMapper mapper, IAccountService accountServices) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public Task AddAmountToPrincipalCuentaAhorro(SaveCuentaAhorroViewModel vm)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAmountToCuentaAhorro(string userName, decimal amount)
        {
            var CuentaAhorro = await _repository.GetCuentaAhorroByOwner(userName);

            if (CuentaAhorro != null)
            {
                
                CuentaAhorro.Cantidad += amount;
                CuentaAhorro.IdUsuario = userName;
                await _repository.UpdateAsync(CuentaAhorro, CuentaAhorro.Id);
                return true;
            }

            return false; 
        }

      
        public override async Task<List<CuentaAhorroViewModel>> GetAllViewModel()
        {
            var list = await _repository.GetAllAsync();

            List<CuentaAhorroViewModel> result = new List<CuentaAhorroViewModel>();

            foreach (var item in list)
            {

                CuentaAhorroViewModel vm = new CuentaAhorroViewModel
                {
                    Id = item.Id,
                    IdUsuario = item.IdUsuario,
                    Cantidad = item.Cantidad,
                    EsPrincipal = item.EsPrincipal,
                    NumeroProducto = item.NumeroProducto,
                };

                var a = await _accountServices.GetUserById(item.IdUsuario);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }

        public async Task<CuentaAhorroViewModel> GetByAccountINumber(string NumeroProducto)
        {
            var list = await GetAllViewModel();

            return list.FirstOrDefault(x => x.NumeroProducto == NumeroProducto);
        }

        public async Task<List<CuentaAhorroViewModel>> GetAllVMbyUserId()
        {
            var CuentaAhorroList = await _repository.GetAllAsync();
            CuentaAhorroList = CuentaAhorroList.Where(x => x.IdUsuario == userSession.Id).ToList();

            var CuentaAhorrosVM = _mapper.Map<List<CuentaAhorroViewModel>>(CuentaAhorroList);
           
            return CuentaAhorrosVM;
        }

        public async Task<SaveCuentaAhorroViewModel> GetVmByAccountNumber(string NumeroProducto)
        {
            var list = await GetAllViewModel();

             var accout = list.FirstOrDefault(x => x.NumeroProducto == NumeroProducto);

            var save = new SaveCuentaAhorroViewModel()
            {
                Id = accout.Id,
                Cantidad = accout.Cantidad,
                NumeroProducto = NumeroProducto,
                EsPrincipal = accout.EsPrincipal,
                //UserName = accout.UserName,
                IdUsuario = accout.IdUsuario,
            };
            return save;

            
        }
    }
}
