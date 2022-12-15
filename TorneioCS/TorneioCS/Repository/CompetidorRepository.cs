using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TorneioCS.Context;
using TorneioCS.Models;

namespace TorneioCS.Repository
{
    public class CompetidorRepository : ICompetidorRepository
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<Competidor> dbSet;

        List<Competidor> colocacaoParticipantes = new List<Competidor>();

        public CompetidorRepository(DatabaseContext context)
        {
            _context = context;
            dbSet = _context.Set<Competidor>();
        }

        //Lista os competidores
        public List<Competidor> ListarCompetidores()
        {
            try
            {
                //Consulta o banco de dados
                var competidores = dbSet.ToList();
                return competidores;
            }

            
            catch (Exception erro)
            {
                //Caso haja algum erro consultando o banco de dados
                throw new Exception("Erro ao retornar competidores:" + erro.Message);
            }
        }

        //Consulta ao banco dos competidores por ID
        public IEnumerable<Competidor> GetListaCompetidoresPorID(IEnumerable<int> listaIDs)
        {
            try
            { 
                //Consulta só pra pegar todos os competidores
                return dbSet.Where(x => listaIDs.Contains(x.idCompetidor));
            }
            catch (Exception erro)
            {
                //Caso retorne algum erro em retornar os competidores selecionados 
                throw new Exception("Erro ao retornar competidores selecionados: " + erro.Message);
            }
        }


        public List<Competidor> GetCompetidoresIniciais()
        {
            try
            {
                //Recupera os jogadores da lista competidores.json
                var stringJson = File.ReadAllText("competidores.json");

                //Lê todo o texto no arquivo com a codificação especificada e fecha o arquivo
                return JsonConvert.DeserializeObject<List<Competidor>>(stringJson);
            }
            catch (Exception erro)
            {
                //Caso retorne algum erro em recupera os competidores iniciais 
                throw new Exception("Erro ao recuperar competidores iniciais: " + erro.Message); ;
            }
        }

        //Adiciona a derrota acrecentando na quantidade de partidas jogadas
        public void AdicionarDerrota(Competidor competidor)
        {
            try
            {
                //Quando o jogador não ganha e adicionada nas partidas jogadas
                competidor.TotalPartidas++;
                dbSet.Update(competidor);
                _context.SaveChanges();
                AdicionarNaColocaçao(competidor);
            }
            catch (Exception erro)
            {
                //Caso retorne algum erro adicionando a derrota 
                throw new Exception($"Erro ao adicionar derrota para {competidor.Nome}: " + erro.Message);
            }
        }


        //Adição da vitorio no Banco
        public void AdicionarVitoria(Competidor competidor)
        {
            try
            {
                //E dicionada vitorias e o total de partidas jogada
                competidor.TotalPartidas++;
                competidor.Vitorias++;
                dbSet.Update(competidor);
                _context.SaveChanges();
            }
            catch (Exception erro)
            {
                //Caso retorne algum erro adicionando a derrota 
                throw new Exception($"Erro ao adicionar vitória para {competidor.Nome}: " + erro.Message);
            }
        }


        //E adicionada a colocação do competidor em vitorias
        public void AdicionarNaColocaçao(Competidor competidor)
        {
            try
            {
                colocacaoParticipantes.Insert(0, competidor);
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao inserir o competidor {competidor.Nome} na colocação final: " + erro.Message);
            }
        }

        //Retorna a colocação do competidor
        public List<Competidor> GetColocacao()
        {
            try
            {
                return colocacaoParticipantes;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao retornar colocação final: " + erro.Message);
            }
        }



    }
}
