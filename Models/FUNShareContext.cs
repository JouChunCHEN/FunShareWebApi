﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FunShareWebApi.Models;

public partial class FUNShareContext : DbContext
{
    public FUNShareContext(DbContextOptions<FUNShareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> City { get; set; }

    public virtual DbSet<CustomerInfomation> CustomerInfomation { get; set; }

    public virtual DbSet<District> District { get; set; }

    public virtual DbSet<ImageList> ImageList { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderDetail> OrderDetail { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<ProductDetail> ProductDetail { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnName("City_ID");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .HasColumnName("City_Name");
            entity.Property(e => e.RegionId).HasColumnName("Region_ID");
        });

        modelBuilder.Entity<CustomerInfomation>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.ToTable("Customer_Infomation");

            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.AllergyDescription).IsUnicode(false);
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("Birth_Date");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.Email)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(4);
            entity.Property(e => e.IsAllergy).HasColumnName("IsAllergy?");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Nickname).HasMaxLength(30);
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.RegistrationDay).HasColumnType("date");
            entity.Property(e => e.ResidentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Resident_ID");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.SuspensionReason).HasMaxLength(30);

            entity.HasOne(d => d.District).WithMany(p => p.CustomerInfomation)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Customer_Infomation_District");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Customer_Infomation_Customer_Infomation");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.CityId).HasColumnName("City_ID");
            entity.Property(e => e.DistrictName)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("District_Name");

            entity.HasOne(d => d.City).WithMany(p => p.District)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_City");
        });

        modelBuilder.Entity<ImageList>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Image_List");

            entity.Property(e => e.ImageId).HasColumnName("Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ImageList)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Image_List_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.BillingAmount)
                .HasColumnType("money")
                .HasColumnName("Billing_Amount");
            entity.Property(e => e.CouponId).HasColumnName("Coupon_ID");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.OrderTime)
                .HasColumnType("datetime")
                .HasColumnName("Order_Time");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.TaxId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Tax_ID");

            entity.HasOne(d => d.Member).WithMany(p => p.Order)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer_Infomation");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("Order_Detail");

            entity.Property(e => e.OrderDetailId).HasColumnName("Order_Detail_ID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.IsAttend).HasColumnName("isAttend?");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductDetailId).HasColumnName("Product_Detail_ID");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");

            entity.HasOne(d => d.Member).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Detail_Customer_Infomation");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Detail_Order");

            entity.HasOne(d => d.ProductDetail).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.ProductDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Detail_Product_Detail");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AgeId).HasColumnName("Age_ID");
            entity.Property(e => e.IsClass).HasColumnName("IsClass?");
            entity.Property(e => e.Note).HasColumnType("ntext");
            entity.Property(e => e.ProductIntro)
                .HasColumnType("ntext")
                .HasColumnName("Product_Intro");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("Product_Name");
            entity.Property(e => e.ReleasedTime).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Product)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Supplier");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.ToTable("Product_Detail");

            entity.Property(e => e.ProductDetailId).HasColumnName("Product_Detail_ID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.BeginTime)
                .HasColumnType("datetime")
                .HasColumnName("Begin_Time");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Dealine).HasColumnType("date");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("End_Time");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Class).WithMany(p => p.InverseClass)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Product_Detail_Product_Detail");

            entity.HasOne(d => d.District).WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Product_Detail_District");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Detail_Product");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CityId).HasColumnName("City_ID");
            entity.Property(e => e.ContactPerson)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("Contact_Person");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Email)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LogoImage).HasColumnName("Logo_Image");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.SupplierName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("Supplier_Name");
            entity.Property(e => e.SupplierPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Supplier_Phone");
            entity.Property(e => e.SupplierPhoto).HasColumnName("Supplier_Photo");
            entity.Property(e => e.TaxId)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Tax_ID");

            entity.HasOne(d => d.City).WithMany(p => p.Supplier)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Supplier_City");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}