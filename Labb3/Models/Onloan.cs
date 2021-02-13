using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3.Models
{
    [DisplayName("Låna")]
    public class Onloan
    {
        //PK
        public int OnloanId { get; set; }

        [DisplayName("Namn")]
        [Required]
        [StringLength(30, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string FriendName { get; set; }

        [DisplayName("Datum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        //FK for Record
        [DisplayName("Album")]
        public int RecordId { get; set; }
        public Record Record { get; set; }

        public Onloan()
        {
        }
    }
}
