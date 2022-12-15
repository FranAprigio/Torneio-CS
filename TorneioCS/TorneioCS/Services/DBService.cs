using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneioCS.Models;
using TorneioCS.Repository;
using Microsoft.Extensions.DependencyInjection;
using TorneioCS.Context;
using Microsoft.EntityFrameworkCore;

namespace TorneioCS.Services
{
    public class DBService
    {

        public void StartDB(IServiceProvider service)
        {
            try
            {
                var context = service.GetService<DatabaseContext>();

                context.Database.Migrate();

                if (!context.Competidores.Any())
                {
                    var competidorRepository = service.GetService<ICompetidorRepository>();
                    List<Competidor> lista = competidorRepository.GetCompetidoresIniciais();

                }
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao iniciar o banco de dados: " + erro.Message);
            }
        }

    }
}
