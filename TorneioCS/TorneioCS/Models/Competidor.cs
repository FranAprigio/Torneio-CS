using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioCS.Models
{
    public class Competidor
    {
        [Key]
        public int idCompetidor { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Nome { get; set; }
        [Column(TypeName = "int")]
        public int Idade { get; set; }
        [Column(TypeName = "int")]
        public int TotalKill { get; set; }
        [Column(TypeName = "int")]
        public int TotalPartidas { get; set; }
        [Column(TypeName = "int")]
        public int Vitorias { get; set; }

    }
}
