using ARC_InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ARC_InternetBanking.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<CuentaAhorro> CuentaAhorros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<TarjetaCredito> TarjetaCreditos { get; set; }
        public DbSet<TipoTransaccion> TipoTransacciones { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables

            modelBuilder.Entity<Beneficiario>().ToTable("Beneficiarios");
            modelBuilder.Entity<CuentaAhorro>().ToTable("CuentaAhorros");
            modelBuilder.Entity<Prestamo>().ToTable("Prestamos");
            modelBuilder.Entity<TarjetaCredito>().ToTable("TarjetaCreditos");
            modelBuilder.Entity<TipoTransaccion>().ToTable("TipoTransacciones");
            modelBuilder.Entity<Transaccion>().ToTable("Transacciones");            

            #endregion

            #region Keys

            modelBuilder.Entity<Beneficiario>().HasKey(x => x.Id);
            modelBuilder.Entity<CuentaAhorro>().HasKey(x => x.Id);
            modelBuilder.Entity<Prestamo>().HasKey(x => x.Id);
            modelBuilder.Entity<TarjetaCredito>().HasKey(x => x.Id);
            modelBuilder.Entity<TipoTransaccion>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaccion>().HasKey(x => x.Id);

            #endregion

            #region Relationships

            #region TipoTransacciones

            modelBuilder.Entity<TipoTransaccion>()
                .HasMany(x => x.Transacciones)
                .WithOne(t => t.TipoTransaccion)
                .HasForeignKey(x => x.IdTipoTransaccion)
                .HasConstraintName("fk_TipoTransaccion_Transaccion");
            #endregion

            #endregion

            #region Properties Configuration

            modelBuilder.Entity<TarjetaCredito>()
                        .Property(x => x.Deuda)
                        .HasColumnType("Decimal")
                        .HasPrecision(16, 2);

            #region Tipo Transaccion

            #region Inserta datos en tipo de transaccion

            modelBuilder.Entity<TipoTransaccion>()
                .HasData
                (
                new TipoTransaccion
                {
                    Id = 1,
                    NombreTipoTransaccion = "Pago Expreso",
                    Created = DateTime.Now,
                    CreatedBy = "Admin",
                },
                new TipoTransaccion
                {
                    Id = 2,
                    NombreTipoTransaccion = "Pago de Tarjeta Credito",
                    Created = DateTime.Now,
                    CreatedBy = "Admin",
                },
                new TipoTransaccion
                {
                    Id = 3,
                    NombreTipoTransaccion = "Pago de Prestamo",
                    Created = DateTime.Now,
                    CreatedBy = "Admin",
                },
                 new TipoTransaccion
                 {
                     Id = 4,
                     NombreTipoTransaccion = "Pago a Beneficiario",
                     Created = DateTime.Now,
                     CreatedBy = "Admin",
                 },
                  new TipoTransaccion
                  {
                      Id = 5,
                      NombreTipoTransaccion = "Avance efectivo",
                      Created = DateTime.Now,
                      CreatedBy = "Admin",
                  },
                   new TipoTransaccion
                   {
                       Id = 6,
                       NombreTipoTransaccion = "Transferecia entre Cuentas",
                       Created = DateTime.Now,
                       CreatedBy = "Admin",
                   }
                );

            #endregion

            #endregion

            #endregion

        }
    }
}
