using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MultiMap.Data.Models
{
    public class BygningType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter BygningType")]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("BygningType")]
        public string Navn { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public string UserID { get; set; }
    }
}
