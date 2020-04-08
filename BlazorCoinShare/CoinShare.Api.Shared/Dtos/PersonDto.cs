using System;
using System.ComponentModel.DataAnnotations;

namespace CoinShare.Api.Shared.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(140, ErrorMessage = "Gruppenname muss angegeben werden")]
        public string Name { get; set; }

        public Guid? GroupId { get; set; }
    }
}
