using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MultiMap.Data.Models
{
    public class Etasje
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Etasje")]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Etasje")]
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        [ForeignKey("Bygg")]
        [Required(ErrorMessage = "The Bygg field is required.")]
        public int ByggId { get; set; }
        public Bygg Bygg { get; set; }
        public string UserID { get; set; }

    }
}
