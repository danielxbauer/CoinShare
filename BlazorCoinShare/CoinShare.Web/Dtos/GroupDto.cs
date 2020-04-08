using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoinShare.Api.Shared.Dtos;

namespace CoinShare.Web.Dtos
{
    public class GroupDto
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(140, MinimumLength = 3)]
        public string Name { get; set; }

        [ValidateComplexType]
        [Required]
        // https://github.com/dotnet/aspnetcore/issues/17316
        public IList<PersonDto> Persons { get; set; }
    }
}
