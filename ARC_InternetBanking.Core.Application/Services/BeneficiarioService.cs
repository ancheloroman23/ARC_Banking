using ARC_InternetBanking.Core.Application.Dtos.Account;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Services;
using ARC_InternetBanking.Core.Application.ViewModels.Beneficiarios;
using ARC_InternetBanking.Core.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ARC_InternetBanking.Core.Application.Services
{
    public class BeneficiarioService : GenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario>, IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _BeneficiarioRepository;
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BeneficiarioService(IUserService userService,IBeneficiarioRepository BeneficiarioRepository, IMapper mapper, ICuentaAhorroService CuentaAhorroService, IHttpContextAccessor httpContextAccessor) : base(BeneficiarioRepository, mapper)
        {
            _BeneficiarioRepository = BeneficiarioRepository;
            _mapper = mapper;
            _CuentaAhorroService = CuentaAhorroService;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        public async Task<bool> AddBeneficiario(string NumeroProducto)
        { 
            var addedSuccessfully = await _BeneficiarioRepository.AddBeneficiario(NumeroProducto);
            return addedSuccessfully;
        }

       

        public override async Task<SaveBeneficiarioViewModel> Add(SaveBeneficiarioViewModel vm)
        {
            try 
            {
                var Beneficiario = new Beneficiario();

                var destinationAccount = await _CuentaAhorroService.GetByAccountINumber(vm.IdProducto);

                var beneficiaries = await GetBeneficiryByUserId();
                var BeneficiarioalreadyExits = beneficiaries.Exists(x => x.IdProducto == vm.IdProducto);

                var CuentaAhorros = await _CuentaAhorroService.GetAllViewModel();


                if (userSession.Id != destinationAccount.IdUsuario && BeneficiarioalreadyExits == false)
                {
                    var matchingCuentaAhorro = CuentaAhorros.FirstOrDefault(x => x.NumeroProducto == vm.IdProducto);

                    if (matchingCuentaAhorro != null)
                    {
                        var userId = userSession?.Id;

                        if (userId != null)
                        {
                            Beneficiario.IdProducto = matchingCuentaAhorro.NumeroProducto;
                            Beneficiario.UsuarioId = userSession.Id;

                            Beneficiario.IdProducto = vm.IdProducto;
                            Beneficiario.BeneficiarioId = matchingCuentaAhorro.IdUsuario;

                            var addedBeneficiario = await _BeneficiarioRepository.AddAsync(Beneficiario);

                            return _mapper.Map<SaveBeneficiarioViewModel>(addedBeneficiario);
                        }
                        else
                        {
                            throw new InvalidOperationException("No se pudo obtener el ID del usuario en sesión");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("El número de cuenta de ahorro no existe");
                    }
                }else if (userSession.Id == destinationAccount.IdUsuario)
                {
                    vm.HasError = true;
                    vm.ErrorMessage = "No se puede agregar a usted mismo como Beneficiario";
                    return vm;
                }

                vm.HasError = true;
                vm.ErrorMessage = "Ya tiene este Beneficiario Agregado";
                return vm;

            }
            catch (Exception ex) 
            {
                throw new Exception();
            }
           
        }

        public async Task<List<BeneficiarioViewModel>> GetBeneficiryByUserId()
        {
            var BeneficiarioList = await _BeneficiarioRepository.GetAllAsync();
            List<BeneficiarioViewModel> listBeneficiarioViewModel = new List<BeneficiarioViewModel>();
            foreach (var list in BeneficiarioList)
            {
                var userBeneficiario = await _userService.GetUserDtoAsync(list.BeneficiarioId);
                var Beneficiario = new BeneficiarioViewModel
                {
                    Id = list.Id,
                    Nombre = userBeneficiario.Nombre,
                    IdProducto = list.IdProducto,
                    UserName = list.UsuarioId,
                    UserNameBeneficiario = userBeneficiario.UserName,
                    Apellido = userBeneficiario.Apellido,
                };

                listBeneficiarioViewModel.Add(Beneficiario);
            }

            return _mapper.Map<List<BeneficiarioViewModel>>(listBeneficiarioViewModel);
        }
    }
}
