using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Ludo
{
    class Peao
    {
        public int id;
        private int posicao;
        private int totalCasa = 52;
        private int casasColoridas = 5;
        public Peao()
        {
            posicao = 0;
            casasColoridas = 5;
        }
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Posicao
        {   
            get {return posicao;}
            set {posicao = value;}

        }
        public int TotalCasa
        {
            get { return totalCasa; }
            set { totalCasa = value; }

        }
        public bool Mover(int numDado)
        {
            if (Posicao == 0)
            {
                Posicao = 1;
                Console.WriteLine($"O peao {Id} se moveu para a casa inicial.");
                return true;
            }
            if (Posicao <= totalCasa)
            {
                Posicao += numDado;

                if (Posicao > totalCasa)
                {
                    casasColoridas = Posicao - totalCasa;
                }
                Console.WriteLine($"Peao {Id} moveu da casa {Posicao - numDado} para a casa {Posicao}.");
                return true;
            }
            else
            {
                if (numDado <= casasColoridas)
                {
                    Posicao += numDado;
                    Console.WriteLine($"Peao {Id} moveu da casa {Posicao - numDado} para a casa {Posicao}.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Dado inválido para este movimento. Necessário um dado {casasColoridas} ou menor.");
                    return false;
                }
            }
        }
        public void RetornarCasa(int idPeao, string cor)
        {
            posicao = 0;
            Console.WriteLine($"O peao {idPeao + 1} da cor {cor} retornou para a prisao.");
        }
    }
}