using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using api_sale_planning.Models;

namespace api_sale_planning.Contexts
{
    public partial class DBSCM : DbContext
    {
        public DBSCM()
        {
        }

        public DBSCM(DbContextOptions<DBSCM> options)
            : base(options)
        {
        }

        public virtual DbSet<AlCustomer> AlCustomers { get; set; } = null!;
        public virtual DbSet<AlPalletTypeMapping> AlPalletTypeMappings { get; set; } = null!;
        public virtual DbSet<AlSaleForecaseMonth> AlSaleForecaseMonths { get; set; } = null!;
        public virtual DbSet<PnCompressor> PnCompressors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.226.86;Database=dbSCM;TrustServerCertificate=True;uid=sa;password=decjapan");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_CI_AS");

            modelBuilder.Entity<AlCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerCode);

                entity.ToTable("AL_Customer");

                entity.Property(e => e.CustomerCode).HasMaxLength(50);

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.Address3).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(100);

                entity.Property(e => e.CustomerNameShort).HasMaxLength(100);

                entity.Property(e => e.ShipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<AlPalletTypeMapping>(entity =>
            {
                entity.HasKey(e => new { e.Plgrp, e.Pltype });

                entity.ToTable("AL_PalletTypeMapping");

                entity.Property(e => e.Plgrp)
                    .HasMaxLength(10)
                    .HasColumnName("PLGRP");

                entity.Property(e => e.Pltype)
                    .HasMaxLength(20)
                    .HasColumnName("PLTYPE");

                entity.Property(e => e.Plcode)
                    .HasMaxLength(20)
                    .HasColumnName("PLCode");

                entity.Property(e => e.Pllevel)
                    .HasMaxLength(20)
                    .HasColumnName("PLLevel");

                entity.Property(e => e.Plqty)
                    .HasMaxLength(20)
                    .HasColumnName("PLQty");

                entity.Property(e => e.RackControl).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(20);
            });

            modelBuilder.Entity<AlSaleForecaseMonth>(entity =>
            {
                entity.ToTable("AL_SaleForecaseMonth");

                entity.HasIndex(e => new { e.Ym, e.ModelName, e.Lrev }, "IX_AL_SaleForecaseMonth");

                entity.HasIndex(e => new { e.Ym, e.ModelName, e.Pltype, e.Lrev }, "IX_AL_SaleForecaseMonth_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Customer).HasMaxLength(15);

                entity.Property(e => e.D01).HasDefaultValueSql("((0))");

                entity.Property(e => e.D02).HasDefaultValueSql("((0))");

                entity.Property(e => e.D03).HasDefaultValueSql("((0))");

                entity.Property(e => e.D04).HasDefaultValueSql("((0))");

                entity.Property(e => e.D05).HasDefaultValueSql("((0))");

                entity.Property(e => e.D06).HasDefaultValueSql("((0))");

                entity.Property(e => e.D07).HasDefaultValueSql("((0))");

                entity.Property(e => e.D08).HasDefaultValueSql("((0))");

                entity.Property(e => e.D09).HasDefaultValueSql("((0))");

                entity.Property(e => e.D10).HasDefaultValueSql("((0))");

                entity.Property(e => e.D11).HasDefaultValueSql("((0))");

                entity.Property(e => e.D12).HasDefaultValueSql("((0))");

                entity.Property(e => e.D13).HasDefaultValueSql("((0))");

                entity.Property(e => e.D14).HasDefaultValueSql("((0))");

                entity.Property(e => e.D15).HasDefaultValueSql("((0))");

                entity.Property(e => e.D16).HasDefaultValueSql("((0))");

                entity.Property(e => e.D17).HasDefaultValueSql("((0))");

                entity.Property(e => e.D18).HasDefaultValueSql("((0))");

                entity.Property(e => e.D19).HasDefaultValueSql("((0))");

                entity.Property(e => e.D20).HasDefaultValueSql("((0))");

                entity.Property(e => e.D21).HasDefaultValueSql("((0))");

                entity.Property(e => e.D22).HasDefaultValueSql("((0))");

                entity.Property(e => e.D23).HasDefaultValueSql("((0))");

                entity.Property(e => e.D24).HasDefaultValueSql("((0))");

                entity.Property(e => e.D25).HasDefaultValueSql("((0))");

                entity.Property(e => e.D26).HasDefaultValueSql("((0))");

                entity.Property(e => e.D27).HasDefaultValueSql("((0))");

                entity.Property(e => e.D28).HasDefaultValueSql("((0))");

                entity.Property(e => e.D29).HasDefaultValueSql("((0))");

                entity.Property(e => e.D30).HasDefaultValueSql("((0))");

                entity.Property(e => e.D31).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lrev)
                    .HasMaxLength(3)
                    .HasColumnName("LREV");

                entity.Property(e => e.ModelCode).HasMaxLength(50);

                entity.Property(e => e.ModelName).HasMaxLength(50);

                entity.Property(e => e.Pltype)
                    .HasMaxLength(10)
                    .HasColumnName("PLTYPE");

                entity.Property(e => e.Rev)
                    .HasMaxLength(3)
                    .HasColumnName("REV");

                entity.Property(e => e.Sebango).HasMaxLength(10);

                entity.Property(e => e.Ym)
                    .HasMaxLength(6)
                    .HasColumnName("YM");
            });

            modelBuilder.Entity<PnCompressor>(entity =>
            {
                entity.HasKey(e => new { e.ModelCode, e.Model, e.Line });

                entity.ToTable("PN_Compressor");

                entity.Property(e => e.ModelCode).HasMaxLength(20);

                entity.Property(e => e.Model).HasMaxLength(250);

                entity.Property(e => e.CreateBy).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModelGroup).HasMaxLength(50);

                entity.Property(e => e.ModelType)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'1YC')");

                entity.Property(e => e.Rmk1)
                    .HasMaxLength(250)
                    .HasColumnName("rmk1")
                    .HasComment("Model customer");

                entity.Property(e => e.Rmk10)
                    .HasMaxLength(250)
                    .HasColumnName("rmk10")
                    .HasComment("data for check original line (backflush)");

                entity.Property(e => e.Rmk2)
                    .HasMaxLength(250)
                    .HasColumnName("rmk2")
                    .HasComment("Pallate");

                entity.Property(e => e.Rmk3)
                    .HasMaxLength(250)
                    .HasColumnName("rmk3")
                    .HasComment("Magnet");

                entity.Property(e => e.Rmk4)
                    .HasMaxLength(250)
                    .HasColumnName("rmk4");

                entity.Property(e => e.Rmk5)
                    .HasMaxLength(250)
                    .HasColumnName("rmk5")
                    .HasComment("bit for check terminal cover");

                entity.Property(e => e.Rmk6)
                    .HasMaxLength(250)
                    .HasColumnName("rmk6");

                entity.Property(e => e.Rmk7)
                    .HasMaxLength(250)
                    .HasColumnName("rmk7");

                entity.Property(e => e.Rmk8)
                    .HasMaxLength(250)
                    .HasColumnName("rmk8");

                entity.Property(e => e.Rmk9)
                    .HasMaxLength(250)
                    .HasColumnName("rmk9");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'active')");

                entity.Property(e => e.UpdateBy).HasMaxLength(20);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
