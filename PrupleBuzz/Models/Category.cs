﻿using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PrupleBuzz.Models
{
    public class Category
    {
        public Category()
        {
            Service = new List<Service>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Service> Service { get; set; }
        public bool IsDeleted { get; set; }


    }
}
