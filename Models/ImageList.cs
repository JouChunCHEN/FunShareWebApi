﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FunShareWebApi.Models;

public partial class ImageList
{
    public int ImageId { get; set; }

    public int ProductId { get; set; }

    public byte[] Images { get; set; }

    public string ImagePath { get; set; }

    public bool? IsMain { get; set; }

    public virtual Product Product { get; set; }
}