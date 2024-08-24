using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Ludo
{
    class Jogador
    {
        private Peao[] vetorPeoes;
        private string nome;
        private string cor;
        private int id;
        private int peoesVencedores;

        public Jogador()
        {
            vetorPeoes = new Peao[4];
            peoesVencedores = 0;
        }
        public Peao[] VetorPeoes
        {
            get { return vetorPeoes; }
            set { vetorPeoes = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Cor
        {
            get { return cor; }
            set { cor = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int PeoesVencedores
        {
            get { return peoesVencedores; }
            set { peoesVencedores = value; }
        }
        public int LancarDado()
        {
            Random random = new Random();
            int dado = random.Next(1, 7);
            return dado;
        }
        public int RolagemInicial()
        {
            if (vetorPeoes[0].Posicao == 0 && vetorPeoes[1].Posicao == 0 && vetorPeoes[2].Posicao == 0 && vetorPeoes[3].Posicao == 0)
            {
                if (LancarDado() == 6)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 2;
            }
        }
        public bool SelecionarPeao(int idPeao, int dado)
        {

            if (idPeao < 1 || idPeao > 4)
            {
                Console.WriteLine("Id inválido");
                return false;
            }

            else if (VetorPeoes[idPeao - 1].Posicao == 57)
            {
                Console.WriteLine("Peao já alcançou a última casa.");
                return false;
            }

            else if (VetorPeoes[idPeao - 1].Posicao == 0 && dado != 6)
            {
                Console.WriteLine("Para colocar este peao no início, utilize um dado 6.");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool PeaoValido(int idPeao, int dado)
        {
            bool resposta = false;
            int cont = 0;

            while (resposta == false)
            {
                if(vetorPeoes[cont].Posicao != 0 && 57 - vetorPeoes[cont].Posicao > dado)
                {
                    resposta = true;
                }
                cont++;
                if(cont == 4)
                {
                    break;
                }
            }
            return resposta;
        }
    }
}
