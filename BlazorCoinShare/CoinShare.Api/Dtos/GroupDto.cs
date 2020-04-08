using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoinShare.Api.Shared.Dtos;

namespace CoinShare.Api.Dtos
{
    public class GroupDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(140, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime ExpiresOn { get; set; }

        [Required]
        public IList<PersonDto> Persons { get; set; }
    }
}
