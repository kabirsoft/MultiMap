using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultiMap.Data.Models
{
    public class Bygningsregister
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter Lokasjon")]
        public string Lokasjon { get; set; }
        [Required(ErrorMessage = "Please enter AntallBygning")]
        public int AntallBygning { get; set; }
        //public string HF { get; set; }
        public double Areal { get; set; }
        public string Objektype { get; set; }
        public int Snittalder { get; set; }
        public string Beskrivelse { get; set; }
        public double Tomtareal { get; set; }
       // public string Lokasjonstype { get; set; }    
        public string Bilde { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserID { get; set; }
    }
}
