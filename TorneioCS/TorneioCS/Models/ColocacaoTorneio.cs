using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioCS.Models
{
    public class ColocacaoTorneio
    {
        public Competidor Vencedor { get; set; }
        public List<Competidor> Colocaçao { get; set; }
    }
}
