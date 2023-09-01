﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FunShareWebApi.Models;

public partial class ProductDetail
{
    public int ProductDetailId { get; set; }

    public int ProductId { get; set; }

    public DateTime? BeginTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? DistrictId { get; set; }

    public int StatusId { get; set; }

    public string Address { get; set; }

    public int? Stock { get; set; }

    public decimal? UnitPrice { get; set; }

    public DateTime? Dealine { get; set; }

    public int? ClassId { get; set; }

    public virtual ProductDetail Class { get; set; }

    public virtual District District { get; set; }

    public virtual ICollection<ProductDetail> InverseClass { get; set; } = new List<ProductDetail>();

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    public virtual Product Product { get; set; }
}