using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MultiMap.Data.Models
{
    public class Lokasjon
    {
        public Lokasjon()
        {
            Byggs = new List<Bygg>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Lokasjon")]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Lokasjon")]
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public string Selskap { get; set; }
        public int AntallBygg { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public List<Bygg> Byggs { get; set; }
        public string UserID { get; set; }
    }
}
