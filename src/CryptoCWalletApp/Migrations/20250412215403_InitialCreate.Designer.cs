﻿// <auto-generated />
using CryptoCWalletApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CryptoCWalletApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250412215403_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CryptoCWalletApp.Models.CryptoCurrencyEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal?>("LastUpdatedAt")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<decimal?>("Usd")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Usd24HChange")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Usd24HVol")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("UsdMarketCap")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("CryptoCurrency");
                });
#pragma warning restore 612, 618
        }
    }
}
