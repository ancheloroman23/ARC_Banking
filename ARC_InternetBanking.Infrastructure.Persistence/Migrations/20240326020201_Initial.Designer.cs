﻿// <auto-generated />
using System;
using ARC_InternetBanking.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ARC_InternetBanking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240326020201_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.Beneficiario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BeneficiarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beneficiarios", (string)null);
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.CuentaAhorro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsPrincipal")
                        .HasColumnType("bit");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CuentaAhorros", (string)null);
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CantidaPrestamo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CantidadPagada")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prestamos", (string)null);
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.TarjetaCredito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CantidadActual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Deuda")
                        .HasPrecision(16, 2)
                        .HasColumnType("Decimal");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Limite")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TarjetaCreditos", (string)null);
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.TipoTransaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreTipoTransaccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoTransacciones", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 3, 25, 22, 2, 0, 987, DateTimeKind.Local).AddTicks(8652),
                            CreatedBy = "Admin",
                            NombreTipoTransaccion = "Pago Expreso"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 3, 25, 22, 2, 0, 987, DateTimeKind.Local).AddTicks(8671),
                            CreatedBy = "Admin",
                            NombreTipoTransaccion = "Pago de Tarjeta Credito"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2024, 3, 25, 22, 2, 0, 987, DateTimeKind.Local).AddTicks(8676),
                            CreatedBy = "Admin",
                            NombreTipoTransaccion = "Pago de Prestamo"
                        },
                        new
                        {
                            Id = 4,
                            Created = new DateTime(2024, 3, 25, 22, 2, 0, 987, DateTimeKind.Local).AddTicks(8680),
                            CreatedBy = "Admin",
                            NombreTipoTransaccion = "Pago a Beneficiario"
                        },
                        new
                        {
                            Id = 5,
                            Created = new DateTime(2024, 3, 25, 22, 2, 0, 987, DateTimeKind.Local).AddTicks(8684),
                            CreatedBy = "Admin",
                            NombreTipoTransaccion = "Avance efectivo"
                        },
                        new
                        {
                            Id = 6,
                            Created = new DateTime(2024, 3, 25, 22, 2, 0, 987, DateTimeKind.Local).AddTicks(8689),
                            CreatedBy = "Admin",
                            NombreTipoTransaccion = "Transferecia entre Cuentas"
                        });
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinoNumeroCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTipoTransaccion")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioTitularCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginNumeroCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoTransaccion");

                    b.ToTable("Transacciones", (string)null);
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.Transaccion", b =>
                {
                    b.HasOne("ARC_InternetBanking.Core.Domain.Entities.TipoTransaccion", "TipoTransaccion")
                        .WithMany("Transacciones")
                        .HasForeignKey("IdTipoTransaccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_TipoTransaccion_Transaccion");

                    b.Navigation("TipoTransaccion");
                });

            modelBuilder.Entity("ARC_InternetBanking.Core.Domain.Entities.TipoTransaccion", b =>
                {
                    b.Navigation("Transacciones");
                });
#pragma warning restore 612, 618
        }
    }
}
