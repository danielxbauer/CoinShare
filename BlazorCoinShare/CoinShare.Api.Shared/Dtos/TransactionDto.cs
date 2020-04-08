using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoinShare.Api.Shared.Enums;

namespace CoinShare.Api.Shared.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(140)]
        public string Text { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaidOn { get; set; }

        [Required]
        public int PaidBy { get; set; }

        [Required]
        public IList<int> PaidFor { get; set; }

        public TransactionType TransactionType { get; set; }

        public Guid? GroupId { get; set; }
    }
}
