using System;
using System.ComponentModel.DataAnnotations;

namespace exam.Models
{
    public class Category
    {
        public int id { get; set; }
        [MaxLength(200)]
        public string name { get; set; }
    }
}