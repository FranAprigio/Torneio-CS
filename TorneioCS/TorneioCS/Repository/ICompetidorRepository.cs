using System.Collections.Generic;
using TorneioCS.Models;

namespace TorneioCS.Repository
{

    public interface ICompetidorRepository
    {
        List<Competidor> ListarCompetidores();
        public IEnumerable<Competidor> GetListaCompetidoresPorID(IEnumerable<int> listaIDs);
        List<Competidor> GetCompetidoresIniciais();
        void AdicionarDerrota(Competidor competidor);
        void AdicionarVitoria(Competidor competidor);
        public void AdicionarNaColocaçao(Competidor competidor);
        List<Competidor> GetColocacao();
    }

}
