using DAL.UserModels;
using Domains;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.DbContext_;

public partial  class ShippingContext : IdentityDbContext<ApplicationUser>
{
    public ShippingContext()
    {
    }

    public ShippingContext(DbContextOptions<ShippingContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<Log> Log { get; set; }

    public virtual DbSet<TbCarriers> TbCarriers { get; set; }

    public virtual DbSet<TbCities> TbCities { get; set; }

    public virtual DbSet<TbCountries> TbCountries { get; set; }

    public virtual DbSet<TbPaymentMethods> TbPaymentMethods { get; set; }

    public virtual DbSet<TbSetting> TbSetting { get; set; }

    public virtual DbSet<TbShippingTypes> TbShippingTypes { get; set; }

    public virtual DbSet<TbShippmentStatus> TbShippmentStatus { get; set; }

    public virtual DbSet<TbShippments> TbShippments { get; set; }

    public virtual DbSet<TbSubscriptionPackages> TbSubscriptionPackages { get; set; }

    public virtual DbSet<TbUserReceivers> TbUserReceivers { get; set; }

    public virtual DbSet<TbUserSenders> TbUserSenders { get; set; }

    public virtual DbSet<TbUserSubscriptions> TbUserSubscriptions { get; set; }
    public virtual DbSet<VwCities> VwCities { get; set; }
    public virtual DbSet<TbRefreshTokens> TbRefreshTokens { get; set; }
    public virtual DbSet<TbShipingPackging> TbShipingPackging { get; set; }


   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbCarriers>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CarrierName).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbCities>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CityAName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CityEName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.TbCities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCities_TbCountries");
        });

        modelBuilder.Entity<TbCountries>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CountryAName).HasMaxLength(200);
            entity.Property(e => e.CountryEName).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbPaymentMethods>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MethdAName).HasMaxLength(200);
            entity.Property(e => e.MethodEName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbSetting>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<TbShippingTypes>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingTypeAName).HasMaxLength(200);
            entity.Property(e => e.ShippingTypeEName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbShippmentStatus>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

           
            entity.HasOne(d => d.Shippment).WithMany(p => p.TbShippmentStatus)
                .HasForeignKey(d => d.ShippmentId)
                .HasConstraintName("FK_TbShippmentStatus_TbShippments");
        });

        modelBuilder.Entity<TbShippments>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PackageValue).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.ShipingDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingRate).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_TbShippments_TbPaymentMethods");

            entity.HasOne(d => d.Receiver).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbUserReceivers");

            entity.HasOne(d => d.Sender).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbUserSebders");
            entity.HasOne(d => d.Carrier).WithMany(p => p.TbShipments)
               .HasForeignKey(d => d.CarrierId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_TbShippmentStatus_TbCarriers");


            entity.HasOne(d => d.ShippingType).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.ShippingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbShippingTypes");
        });

        modelBuilder.Entity<TbSubscriptionPackages>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PackageName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbUserReceivers>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(200);
            entity.Property(e => e.ReceiverName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.TbUserReceivers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUserReceivers_TbCities");
        });

        modelBuilder.Entity<TbUserSenders>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(200);
            entity.Property(e => e.SenderName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.TbUserSenders)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUserSebders_TbCities");
        });

        modelBuilder.Entity<TbUserSubscriptions>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Package).WithMany(p => p.TbUserSubscriptions)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUserSubscriptions_TbSubscriptionPackages");
        });
        modelBuilder.Entity<VwCities>(entity =>
        {
       
            entity.ToView("VwCities");

        });

        modelBuilder.Entity<TbRefreshTokens>(entity =>
        {
            // Set Id as Guid and configure it as the primary key
            entity.HasKey(e => e.Id);

            // Set default value for Id as Guid
            entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");

            // Configure CurrentState as an integer (e.g., 0 = Active, 1 = Revoked)
            entity.Property(e => e.CurrentState)
                .HasDefaultValue(1) // Set default value to 0 (active)
                .IsRequired();

            // Configure CreatedBy, CreatedDate, UpdatedBy, and UpdatedDate
            entity.Property(e => e.CreatedBy).IsRequired();
            entity.Property(e => e.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("GETDATE()");
        });
        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
