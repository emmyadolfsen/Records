using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3.Models
{
    public class Artist
    {
        // PK
        [DisplayName("Artist")]
        public int ArtistId { get; set; }

        [DisplayName("Artist")]
        [Required]
        [StringLength(30, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string ArtistName { get; set; }

        //FK for record
        public ICollection<Record> Records { get; set; }

        public Artist()
        {
        }
    }
}
