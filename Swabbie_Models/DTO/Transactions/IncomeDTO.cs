﻿using Swabbie_DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace Swabbie_Models.DTO.Transactions
{
    public class IncomeDTO
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TransactionDate { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public IncomeCat Category { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionCat TransactionType { get; set; }
        [Required]
        public CurrencyCat CurrencyType { get; set; }
    }
}
