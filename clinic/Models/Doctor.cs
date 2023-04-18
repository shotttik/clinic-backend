﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;

namespace clinic.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Pid), IsUnique = true)]
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Views { get; set; } = 0;
        public string Pid { get; set; }
        public string? Image { get; set; } = null;
        public string? Document { get; set; } = null;
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
