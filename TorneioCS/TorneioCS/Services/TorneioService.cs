using System;
using System.Collections.Generic;
using System.Linq;
using TorneioCS.Models;
using TorneioCS.Repository;

namespace TorneioCS.Services
{
    public class TorneioService : ITorneioService
    {

        private readonly ICompetidorRepository _competidorRepository;

        public TorneioService(ICompetidorRepository competidorRepository)
        {
            _competidorRepository = competidorRepository;
        }

        //Organização por idade dos competidores
        public IList<Competidor> OrganizarPorIdade(IList<int> listaIDs)
        {
            try
            {
                //Busca a listta por Id e ordena por idade
                var competidoresOrganizados = _competidorRepository.GetListaCompetidoresPorID(listaIDs).OrderBy(com => com.Idade);
                //retorna a lista com os competidores em ordem
                return competidoresOrganizados.ToList();
            }
            catch (Exception erro)
            { 
                //caso haja algum erro na odenação
                throw new Exception($"Erro ao ordenar competidores por idade: " + erro.Message);
            }
        }

        //
        public List<Competidor> RodarFaseTorneio(IList<Competidor> competidores)
        {
            var integrantesProxFase = new List<Competidor>();
            try
            {
                for (int i = 0; i < competidores.Count; i += 2)
                {
                    //Método que busca vencedor da rodada
                    var vencedor = GetVencedorPartida(competidores[i], competidores[i + 1]);
                    integrantesProxFase.Add(vencedor);
                }

                //vencedor definido joga proxima fase
                return integrantesProxFase;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao simular rodada: " + erro.Message);
            }
        }


        //Determina a colocação no toneio e a colocação final dos competidores
        public ColocacaoTorneio GetColocacaoFinal(IList<Competidor> competidores)
        {
            var participantesRodada = competidores;
            ColocacaoTorneio colocacaoFinal = new ColocacaoTorneio();

            //Loop determina as rodas ate a semifinal que contém 4 competidores
            for (int i = 0; i < 4; i++)
            {
                participantesRodada = RodarFaseTorneio(participantesRodada);

                //Se tem somente um competidor
                if (participantesRodada.Count == 1)
                {
                    colocacaoFinal.Vencedor = participantesRodada[0];
                }
            }

            //Retorna a colocaão final dos competidores
            colocacaoFinal.Colocaçao = _competidorRepository.GetColocacao();

            return colocacaoFinal;
        }


        //Vencedor da partida, e determinado fase por fase
        public Competidor GetVencedorPartida(Competidor competidor1, Competidor competidor2)
        {
            try
            {
                //A porcentagem das vitorias do competidor e 100*vitoria/totalPartidas
                var porcentagemVitoriaCom1 = 100 * competidor1.Vitorias / competidor1.TotalPartidas;
                var porcentagemVitoriaCom2 = 100 * competidor2.Vitorias / competidor2.TotalPartidas;

                //Se a porcentagem do for identica passa para o desempate
                if (porcentagemVitoriaCom1 == porcentagemVitoriaCom2)
                {
                    return Desempate(competidor1, competidor2);
                }

                //Se a porcentagem do competidor 1 for maior e adicionada a vitoria para ele e a derrota para o competidor 2
                if (porcentagemVitoriaCom1 > porcentagemVitoriaCom2)
                {
                    _competidorRepository.AdicionarVitoria(competidor1);
                    _competidorRepository.AdicionarDerrota(competidor2);
                   
                    //Retorna para continuar na proxima rodada
                    return competidor1;
                }
                else
                {
                    //Caso a porcentagem do competidor 2 for maior adicionar vitoria para ele e a derrota para o competidor 1
                    _competidorRepository.AdicionarVitoria(competidor2);
                    _competidorRepository.AdicionarDerrota(competidor1);

                    //Retorna para continuar na proxima rodada
                    return competidor2;
                }
            }
            catch (Exception erro)
            {

                //caso acontenca algum erro ao simular o round entre os competidores
                throw new Exception($"Erro ao simular a partida entre {competidor1.Nome} e {competidor2.Nome}: " + erro.Message);
            }
        }


        //Deempate entre os competidores
        public Competidor Desempate(Competidor competidor1, Competidor competidor2)
        {
            try
            {

                //Desempate ccaso o total de kills sejam o mesmo
                if (competidor1.TotalKill == competidor2.TotalKill)
                {

                    //Caso o TotalPartidas do competidor 1 seja maior do que o competidor 2
                    if (competidor1.TotalPartidas > competidor2.TotalPartidas)
                    {
                        //Adiciona a derrota para o 2 e vitoria para o 1
                        _competidorRepository.AdicionarDerrota(competidor2);
                        _competidorRepository.AdicionarVitoria(competidor1);

                        //Retorna para continuar na proxima rodada
                        return competidor1;
                    }
                    else
                    {
                        //Adiciona a derrota para o 1 e vitoria para o 2
                        _competidorRepository.AdicionarDerrota(competidor1);
                        _competidorRepository.AdicionarVitoria(competidor2);

                        //Retorna para continuar na proxima rodada
                        return competidor2;
                    }
                }

                //Se a primeira condição não funcionar
                else
                {

                    //Caso o TotalKill do competidor 1 seja maior do que o competidor 2
                    if (competidor1.TotalKill > competidor2.TotalKill)
                    {

                        //Adiciona a derrota para o 2 e vitoria para o 1
                        _competidorRepository.AdicionarDerrota(competidor2);
                        _competidorRepository.AdicionarVitoria(competidor1);

                        //Retorna para continuar na proxima rodada
                        return competidor1;
                    }
                    else
                    {

                        //Adiciona a derrota para o 1 e vitoria para o 2
                        _competidorRepository.AdicionarDerrota(competidor1);
                        _competidorRepository.AdicionarVitoria(competidor2);

                        //Retorna para continuar na proxima rodada
                        return competidor2;
                    }
                }
            }
            catch (Exception erro)
            {

                //Caso acontenca algum erro ao simular o desempate entre os competidores
                throw new Exception($"Erro ao simular o desempate entre {competidor1.Nome} e {competidor2.Nome}: " + erro.Message);
            }
        }





    }
}
