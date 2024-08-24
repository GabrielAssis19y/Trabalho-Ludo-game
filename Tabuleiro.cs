using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Ludo
{
    internal class Tabuleiro
    {
        private int[] casas;
        private int[] casaSegura = {1, 9, 14, 22, 27, 35, 40, 48};
        private Jogador[] vetorJogadores;
        private string vencedor;

        public Tabuleiro(int quantJogadores)
        {
            vetorJogadores = new Jogador[quantJogadores];
            for (int i = 0; i < quantJogadores; i++)
            {
                VetorJogadores[i] = new Jogador();
                vetorJogadores [i].Id = i + 1;
            }
        }

        public int[] Casas
        {
            get { return casas; }
            set { casas = value; }
        }
        public int[] CasaSegura
        {

            get { return casaSegura; }
            set { casaSegura = value; }
        }
        public Jogador[] VetorJogadores
        {
            get { return vetorJogadores; }
            set { vetorJogadores = value; }
        }
        public string Vencedor
        {
            get { return vencedor; }
            set { vencedor = value; }
        }
        public void IniciarTabuleiro()
        {
            for (int i = 0; i < vetorJogadores.Length; i++)
            {
                for (int j = 0; j < vetorJogadores[i].VetorPeoes.Length; j++)
                {
                    VetorJogadores[i].VetorPeoes[j] = new Peao();
                    VetorJogadores[i].VetorPeoes[j].Id = j + 1;
                }
            }
            vencedor = "";
        }
        public string VerificarCasaSegura(int idJogador, int idPeao)
        {
            for(int i = 0; i < casaSegura.Length; i++)
            {
                if (VetorJogadores[idJogador - 1].VetorPeoes[idPeao - 1].Posicao == casaSegura[i])
                {
                    return "Casa segura";
                }
            }
            return "Casa nao segura";
            
        }
        public string VerificarCaptura(int idJogador, int idPeao)
        {
            string capturas = "";
            for (int i = 0; i < VetorJogadores.Length; i++)
            {
                for (int j = 0; j < VetorJogadores[i].VetorPeoes.Length; j++)
                {
                    if (i > idJogador - 1)
                    {
                        if (VetorJogadores[i].VetorPeoes[j].Posicao - (13 * (i - (idJogador - 1))) == VetorJogadores[idJogador - 1].VetorPeoes[idPeao - 1].Posicao)
                        {
                            capturas += $"O peao {idPeao} do jogador {idJogador} capturou o peao {j + 1} do jogador {i + 1}.\n";
                            VetorJogadores[i].VetorPeoes[j].RetornarCasa(j, VetorJogadores[i].Cor);
                        }
                    }
                    else if (i < idJogador - 1)
                    {
                        if (VetorJogadores[i].VetorPeoes[j].Posicao + (13 * (i - (idJogador - 1))) == VetorJogadores[idJogador - 1].VetorPeoes[idPeao - 1].Posicao)
                        {
                            capturas += $"O peao {idPeao} do jogador {idJogador} capturou o peao {j + 1} do jogador {i + 1}.\n";
                            VetorJogadores[i].VetorPeoes[j].RetornarCasa(j, VetorJogadores[i].Cor);
                        }
                    }
                }
            }
            if (capturas == "")
            {
                return null;
            }
            else
            {
                return capturas;
            }
        }
        public int Captura(int idJogador, int idPeao)
        {
            if (VerificarCasaSegura(idJogador, idPeao) == "Casa segura")
            {
                return 0;
            }
            else
            {
                Console.WriteLine(VerificarCaptura(idJogador, idPeao));
                if(VerificarCaptura(idJogador, idPeao) == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        public void VerificarVitoria(int idJogador)
        {
            if (vetorJogadores[idJogador - 1].PeoesVencedores == 4)
            {
                vencedor = $"Parabéns {vetorJogadores[idJogador - 1].Nome}, voce venceu!!!";
                Environment.Exit(0);
            }
        }
        public int VerificarChegada(int idJogador, int idPeao)
        {
            int resposta = 0;
            if (vetorJogadores[idJogador - 1].VetorPeoes[idPeao - 1].Posicao == 57)
            {
                Console.WriteLine($"Peao {idPeao} do jogador {vetorJogadores[idJogador - 1].Nome} chegou ao final!");
                vetorJogadores[idJogador - 1].PeoesVencedores++;
                
                VerificarVitoria(idJogador);
                resposta = 1;
            }
            Captura(idJogador, idPeao);
            return resposta;
        }
        public string MostrarPeoes(int idJogador)
        {
            string resposta = "";
            for (int i = 0; i < 4; i++)
            {
                if (vetorJogadores[idJogador - 1].VetorPeoes[i].Posicao == 0)
                {
                    resposta += $"O peao {i + 1} ainda nao foi liberado.\n";
                }
                else if (vetorJogadores[idJogador - 1].VetorPeoes[i].Posicao == 57)
                {
                    resposta += $"O peao {i + 1} já está na posiçao final.\n";
                }
                else
                {
                    resposta += $"O peao {i + 1} está na posicao {vetorJogadores[idJogador - 1].VetorPeoes[i].Posicao}.\n";
                }
            }
            return resposta;
        }
    }
}
