using ARC_InternetBanking.Core.Application.Enums;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using ARC_InternetBanking.Core.Application.ViewModels.Transacciones;
using ARC_InternetBanking.Core.Domain.Entities;
using AutoMapper;

namespace ARC_InternetBanking.Core.Application.Services
{

    public class TransaccionService : GenericService<SaveTransaccionViewModel, TransaccionViewModel, Transaccion>, ITransaccionService
    {
        private readonly ITransaccionRepository _TransaccionRepository;
        private readonly IMapper _mapper;
        private readonly ICuentaAhorroService _CuentaAhorroService;
        private readonly IUserService _userService;
        private readonly IPrestamoService _PrestamoService;
        private readonly ITarjetaCreditoService _TarjetaCreditoService;
        public TransaccionService(ITransaccionRepository TransaccionRepository, IMapper mapper, ICuentaAhorroService CuentaAhorroService, IUserService userService, IPrestamoService PrestamoService, ITarjetaCreditoService TarjetaCreditoService) : base(TransaccionRepository, mapper)
        {
            _TransaccionRepository = TransaccionRepository;
            _mapper = mapper;
            _CuentaAhorroService = CuentaAhorroService;
            _userService = userService;
            _PrestamoService = PrestamoService;
            _TarjetaCreditoService = TarjetaCreditoService;
        }

        public async Task ConfirmExpressPayment(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _CuentaAhorroService.GetByAccountINumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);

            destinationAccount.Cantidad += svm.Cantidad;
            var destinyAccount = _mapper.Map<SaveCuentaAhorroViewModel>(destinationAccount);
            await _CuentaAhorroService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Cantidad -= svm.Cantidad;
            var accountOrigin = _mapper.Map<SaveCuentaAhorroViewModel>(originAccount);
            await _CuentaAhorroService.Update(accountOrigin, accountOrigin.Id);

            var Transaccion = new SaveTransaccionViewModel
            {
                Cantidad = svm.Cantidad,
                DestinoNumeroCuenta = svm.DestinoNumeroCuenta,
                OriginNumeroCuenta = svm.OriginNumeroCuenta,
                Descriptcion = svm.Descriptcion,
                IdTipoTransaccion = svm.IdTipoTransaccion,
                IdUsuarioTitularCuenta = svm.IdUsuarioTitularCuenta
            };

            await Add(Transaccion);
        }


        public async Task<SCPagoExpresoViewModel> AddExpressPayment(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _CuentaAhorroService.GetByAccountINumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);
            if(destinationAccount.IdUsuario == originAccount.IdUsuario)
            {
                svm.HasError = true;
                svm.ErrorMessage = "No puede realizar un pago Express a su misma cuenta";

                SCPagoExpresoViewModel confirmPayment = new()
                {
                    SaveTransaccionViewModel = svm,

                };
                return confirmPayment;

            }
            if (destinationAccount != null)
            {
                if (originAccount.Cantidad >= svm.Cantidad)
                {
                    var user = await _userService.GetUserDtoAsync(destinationAccount.IdUsuario);
                    svm.IdUsuarioTitularCuenta = originAccount.IdUsuario;
                    SCPagoExpresoViewModel confirmPayment = new()
                    {
                        SaveTransaccionViewModel = svm,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                    };
                    return confirmPayment;

                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                    SCPagoExpresoViewModel confirmPayment = new()
                    {
                        SaveTransaccionViewModel = svm,

                    };
                    return confirmPayment;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "Este número de cuenta no Existe";

                SCPagoExpresoViewModel confirmPayment = new()
                {
                    SaveTransaccionViewModel = svm,

                };
                return confirmPayment;

            }
        }


        public async Task<SavePrestamoViewModel> AddPrestamoPayment(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _PrestamoService.GetByPrestamoINumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);
            SavePrestamoViewModel Prestamo = new();

            if (destinationAccount.CantidadPagada != destinationAccount.CantidaPrestamo)
            {
                if (originAccount.Cantidad >= svm.Cantidad)
                {
                    var pay = destinationAccount.CantidadPagada + svm.Cantidad;

                    if (pay != destinationAccount.CantidaPrestamo)
                    {
                        var Cantidad = 0m;
                        if (pay < destinationAccount.CantidaPrestamo)
                        {
                            pay = destinationAccount.CantidadPagada += svm.Cantidad;
                            originAccount.Cantidad -= svm.Cantidad;
                        }
                        else
                        {
                            Cantidad = pay - destinationAccount.CantidaPrestamo;
                            destinationAccount.CantidadPagada += svm.Cantidad - Cantidad;
                            originAccount.Cantidad -= svm.Cantidad - Cantidad;
                        }


                        var accountOrigin = new SaveCuentaAhorroViewModel()
                        {
                            NumeroProducto = originAccount.NumeroProducto,
                            Cantidad = originAccount.Cantidad,
                            EsPrincipal = originAccount.EsPrincipal,
                            IdUsuario = originAccount.IdUsuario,
                            Id = originAccount.Id
                        };
                        await _CuentaAhorroService.Update(accountOrigin, accountOrigin.Id);


                        var destinyAccount = new SavePrestamoViewModel()
                        {
                            NumeroProducto = destinationAccount.NumeroProducto,
                            IdUsuario = destinationAccount.IdUsuario,
                            CantidaPrestamo = destinationAccount.CantidaPrestamo,
                            CantidadPagada = destinationAccount.CantidadPagada,
                            Id = destinationAccount.Id

                        };
                        await _PrestamoService.Update(destinyAccount, destinyAccount.Id);

                        var Transaccion = new SaveTransaccionViewModel
                        {
                            Cantidad = destinationAccount.CantidadPagada,
                            DestinoNumeroCuenta = svm.DestinoNumeroCuenta,
                            OriginNumeroCuenta = svm.OriginNumeroCuenta,
                            Descriptcion = svm.Descriptcion ,
                            IdTipoTransaccion = (int)TipoTransacciones.PagoExpreso,
                            IdUsuarioTitularCuenta = originAccount.IdUsuario
                        };

                        SavePrestamoViewModel savePrestamoViewModel = new()
                        {
                            CantidadPagada = pay,
                            CantidaPrestamo = Cantidad
                        };

                        await Add(Transaccion);
                        savePrestamoViewModel.SaveTransaccionViewModel = new();
                        return savePrestamoViewModel;
                    }
                    else
                    {
                        svm.HasError = true;
                        svm.ErrorMessage = "El prestamo ya ha sido saldado";
                        Prestamo.SaveTransaccionViewModel = svm;
                        return Prestamo;
                    }


                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                    Prestamo.SaveTransaccionViewModel = svm;
                    return Prestamo;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "La deuda ha sido paga";
                Prestamo.SaveTransaccionViewModel = svm;
                return Prestamo;
            }
        }

        public async Task<SaveTarjetaCreditoViewModel> AddTarjetaCredito(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _TarjetaCreditoService.GetByCardIdentifyinNumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);
            var pay = 0.00m;
            var Cantidad = 0m;
            SaveTarjetaCreditoViewModel card = new();

            if (originAccount.Cantidad >= svm.Cantidad)
            {
                if (destinationAccount.Deuda > decimal.Zero)
                {
                    if (destinationAccount.Deuda >= svm.Cantidad)
                    {
                        destinationAccount.Deuda -= svm.Cantidad;
                        destinationAccount.CantidadActual += svm.Cantidad;
                        originAccount.Cantidad -= svm.Cantidad;
                    }
                    else
                    {
                        pay = destinationAccount.Deuda;
                        destinationAccount.Deuda = decimal.Zero;
                        destinationAccount.CantidadActual += pay;

                        if (pay != decimal.Zero)
                        {
                            originAccount.Cantidad -= pay;
                        }
                    }

                    var accountOrigin = new SaveCuentaAhorroViewModel()
                    {
                        NumeroProducto = originAccount.NumeroProducto,
                        Cantidad = originAccount.Cantidad,
                        EsPrincipal = originAccount.EsPrincipal,
                        IdUsuario = originAccount.IdUsuario,
                        Id = originAccount.Id,
                    };
                    await _CuentaAhorroService.Update(accountOrigin, accountOrigin.Id);

                    var destinyAccount = new SaveTarjetaCreditoViewModel()
                    {
                        NumeroProducto = destinationAccount.NumeroProducto,
                        IdUsuario = destinationAccount.IdUsario,
                        CantidadActual = destinationAccount.CantidadActual,
                        Limite = destinationAccount.Limite,
                        Id = destinationAccount.Id,
                        Deuda = destinationAccount.Deuda
                    };
                    await _TarjetaCreditoService.Update(destinyAccount, destinyAccount.Id);

                    var Transaccion = new SaveTransaccionViewModel
                    {
                        Cantidad = svm.Cantidad,
                        DestinoNumeroCuenta = svm.DestinoNumeroCuenta,
                        OriginNumeroCuenta = svm.OriginNumeroCuenta,
                        Descriptcion  = svm.Descriptcion ,
                        IdTipoTransaccion = (int)TipoTransacciones.PagoExpreso,
                        IdUsuarioTitularCuenta = originAccount.IdUsuario
                    };

                    SaveTarjetaCreditoViewModel savecardVM = new()
                    {
                        NumeroProducto = destinationAccount.NumeroProducto
                    };

                    await Add(Transaccion);
                    savecardVM.SaveTransaccionViewModel = new();
                    return savecardVM;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "La Deuda ha sido Saldada gracias por ser responsable";
                    card.SaveTransaccionViewModel = svm;
                    return card;
                }
            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "No tiene el monto Suficiente para Realizar el pago";
                card.SaveTransaccionViewModel = svm;
                return card;
            }
        }


        public async Task<SCPagoExpresoViewModel> PayToBeneficiaries(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _CuentaAhorroService.GetByAccountINumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);
      
                if (originAccount.Cantidad >= svm.Cantidad)
                {
                    var user = await _userService.GetUserDtoAsync(destinationAccount.IdUsuario);
                    svm.IdUsuarioTitularCuenta = originAccount.IdUsuario;
                    SCPagoExpresoViewModel confirmBeneficiarioPayment = new()
                    {
                        SaveTransaccionViewModel = svm,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                    };
                    return confirmBeneficiarioPayment;

                }

                svm.HasError = true;
                svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                SCPagoExpresoViewModel confirmPayment = new()
                {
                    SaveTransaccionViewModel = svm,

                };
                return confirmPayment;
            }

        public async Task ConfirmBeneficiarioPayment(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _CuentaAhorroService.GetByAccountINumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);

            destinationAccount.Cantidad += svm.Cantidad;
            var destinyAccount = _mapper.Map<SaveCuentaAhorroViewModel>(destinationAccount);
            await _CuentaAhorroService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Cantidad -= svm.Cantidad;
            var accountOrigin = _mapper.Map<SaveCuentaAhorroViewModel>(originAccount);
            await _CuentaAhorroService.Update(accountOrigin, accountOrigin.Id);

            var Transaccion = new SaveTransaccionViewModel
            {
                Cantidad = svm.Cantidad,
                DestinoNumeroCuenta = svm.DestinoNumeroCuenta,
                OriginNumeroCuenta = svm.OriginNumeroCuenta,
                Descriptcion  = svm.Descriptcion ,
                IdUsuarioTitularCuenta = svm.IdUsuarioTitularCuenta,
                IdTipoTransaccion = svm.IdTipoTransaccion
            };

            await Add(Transaccion);
        }

        public async Task<SaveTransaccionViewModel> AddTransaccionBetween(SaveTransaccionViewModel svm)
        {
            var destinationAccount = await _CuentaAhorroService.GetByAccountINumber(svm.DestinoNumeroCuenta);
            var originAccount = await _CuentaAhorroService.GetByAccountINumber(svm.OriginNumeroCuenta);

            if (originAccount.Id != destinationAccount.Id)
            {
                if (originAccount.Cantidad >= svm.Cantidad)
                {

                    destinationAccount.Cantidad += svm.Cantidad;

                    var destinyAccount = new SaveCuentaAhorroViewModel()
                    {
                        NumeroProducto = destinationAccount.NumeroProducto,
                        Cantidad = destinationAccount.Cantidad,
                        EsPrincipal = destinationAccount.EsPrincipal,
                        IdUsuario = destinationAccount.IdUsuario,
                        Id = destinationAccount.Id
                    };
                    await _CuentaAhorroService.Update(destinyAccount, destinyAccount.Id);

                    originAccount.Cantidad -= svm.Cantidad;


                    var accountOrigin = new SaveCuentaAhorroViewModel()
                    {
                        NumeroProducto = originAccount.NumeroProducto,
                        Cantidad = originAccount.Cantidad,
                        EsPrincipal = originAccount.EsPrincipal,
                        IdUsuario = originAccount.IdUsuario,
                        Id = originAccount.Id
                    };
                    await _CuentaAhorroService.Update(accountOrigin, accountOrigin.Id);

                    return svm;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";

                    return svm;
                }
            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "No puede realizar una transferencia a la misma cuenta";

                return svm;
            }

        }
    }
}
