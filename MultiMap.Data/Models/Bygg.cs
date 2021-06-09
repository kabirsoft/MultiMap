using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MultiMap.Data.Models
{
    public class Bygg
    {
        public Bygg()
        {
            Etasjes = new List<Etasje>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Bygg")]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Bygg")]
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int AntallEtasje { get; set; }
        public int Byggeår { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        [ForeignKey("Lokasjon")]
        [Required(ErrorMessage = "The Lokasjon field is required.")]
        public int LokasjonId { get; set; }
        public Lokasjon Lokasjon { get; set; }
        public List<Etasje> Etasjes { get; set; }
        public string UserID { get; set; }

    }
}
