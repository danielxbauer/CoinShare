using System;
using System.ComponentModel.DataAnnotations;

namespace CoinShare.Core.Models
{
    public class Group
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(140)]
        public string Name { get; set; }
    }
}
