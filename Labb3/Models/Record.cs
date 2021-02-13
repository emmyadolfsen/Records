using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace Labb3.Models
{
    [DisplayName("Album")]
    public class Record
    {
        //PK
        [DisplayName("Album")]
        public int RecordId { get; set; }

        [DisplayName("Album")]
        [Required]
        [StringLength(30, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string RecordName { get; set; }

        [DisplayName("Utlånad")]
        public bool Onloan { get; set; } = false;

        //FK for Artist
        [DisplayName("Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }


        //FK for onloan
        public ICollection<Onloan> Onloans { get; set; }


        public Record()
        {
        }
    }
}
