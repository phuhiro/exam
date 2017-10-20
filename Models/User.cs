using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.Models
{
    public class User
    {
        public int id { get; set; }
        [Column(TypeName = "TEXT")]
        [MaxLength(100)]
        public string name { get; set; }
        [Column(TypeName = "TEXT")]
        [MaxLength(100)]
        public string email { get; set; }
        [Column(TypeName = "TEXT")]
        [MaxLength(100)]
        public string username { get; set; }
        public string password { get; set; }
        public int role { get; set; } = 0;
    }
}
