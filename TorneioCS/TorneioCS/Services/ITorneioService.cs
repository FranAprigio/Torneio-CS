using System.Collections.Generic;
using TorneioCS.Models;

namespace TorneioCS.Services
{
    public interface ITorneioService
    {

        IList<Competidor> OrganizarPorIdade(IList<int> listaIDs);
        public ColocacaoTorneio GetColocacaoFinal(IList<Competidor> competidores);
        public List<Competidor> RodarFaseTorneio(IList<Competidor> competidores);
        Competidor GetVencedorPartida(Competidor competidor1, Competidor competidor2);

        Competidor Desempate(Competidor competidor1, Competidor competidor2);

    }
}
