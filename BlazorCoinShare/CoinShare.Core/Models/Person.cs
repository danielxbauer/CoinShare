using System;
using System.ComponentModel.DataAnnotations;

namespace CoinShare.Core.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(140)]
        public string Name { get; set; }

        public Guid GroupId { get; set; }
    }
}
