using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_Ludo;

namespace Trabalho_Ludo
{

    class Jogo
    {  
        static void Main(string[] args)
        {
            string logPath = "D:\\PUC\\Algoritmos e tecnicas de programacao\\trabalho_ludo\\arquivo.txt";
            try
            {
                using (StreamWriter log = new StreamWriter(logPath, false, Encoding.UTF8))
                {
                    string input;
                    int quantJogadores = 0;
                    int repeticao = 1;

                    do
                    {
                        Console.WriteLine("Digite o número de jogadores (2, 3 ou 4): ");
                        input = Console.ReadLine().Trim();

                        try
                        {
                            quantJogadores = int.Parse(input);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Por favor, digite um número válido.");
                            continue;
                        }
                    } while (quantJogadores < 2 || quantJogadores > 4);
                    log.WriteLine("Número de Jogadores escolhidos.");
                    log.Flush();

                    Tabuleiro tabuleiro = new Tabuleiro(quantJogadores);

                    for (int i = 0; i < quantJogadores; i++)
                    {
                        Console.WriteLine($"Digite o nome do jogador {i + 1}: ");
                        tabuleiro.VetorJogadores[i].Nome = Console.ReadLine();

                        Console.WriteLine($"Digite a cor do jogador {i + 1}: ");
                        tabuleiro.VetorJogadores[i].Cor = Console.ReadLine();
                    }
                    log.WriteLine("Jogadores setados.");
                    log.Flush();

                    tabuleiro.IniciarTabuleiro();
                    log.WriteLine("Tabuleiro iniciado.");
                    log.Flush();

                    while (tabuleiro.Vencedor == "")
                    {
                        log.WriteLine("Jogo iniciado.");
                        log.Flush();

                        if (repeticao > 0)
                        {
                            repeticao -= 1;
                        }
                        int cont = 0;
                        int dado1, dado2, dado3;
                        int dadoSelecionado;
                        int peaoDesejado = 0;
                        bool resposta;
                        string enter;

                        while (cont <= quantJogadores)
                        {Console.WriteLine($"Jogador {cont + 1}, tecle Enter para rolar o dado.");
                            Console.ReadLine();
                            dado1 = tabuleiro.VetorJogadores[cont].LancarDado();
                            Console.WriteLine($"Valor do dado: {dado1}.");
                            log.WriteLine($"Jogador {cont + 1} obteve o primeiro valor");
                            log.Flush();

                            if (tabuleiro.VetorJogadores[cont].RolagemInicial() != 1)
                            {
                                peaoDesejado = 0;
                                if (tabuleiro.VetorJogadores[cont].RolagemInicial() == 0)
                                {
                                    do
                                    {
                                        Console.WriteLine("Selecione o peao que deseja colocar na casa inicial: ");
                                        input = Console.ReadLine();

                                        try
                                        {
                                            peaoDesejado = int.Parse(input);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Por favor, digite um número válido.");
                                            continue;
                                        }

                                    } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dado1) == false);
                                    resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dado1);

                                    Console.WriteLine("Role mais um dado.");
                                    enter = Console.ReadLine();
                                    dado1 = tabuleiro.VetorJogadores[cont].LancarDado();
                                    Console.WriteLine($"Valor do dado: {dado1}.");
                                }

                                peaoDesejado = 0;
                                dadoSelecionado = dado1;

                                if (dado1 == 6)
                                {
                                    Console.WriteLine("Role mais um dado.");
                                    enter = Console.ReadLine();
                                    dado2 = tabuleiro.VetorJogadores[cont].LancarDado();
                                    Console.WriteLine($"Valor do dado: {dado2}.");
                                    log.WriteLine($"Jogador {cont + 1} obteve o segundo valor, precisou jogar novamente");
                                    log.Flush();

                                    if (dado2 == 6)
                                    {
                                        Console.WriteLine("Role mais um dado.");
                                        enter = Console.ReadLine();
                                        dado3 = tabuleiro.VetorJogadores[cont].LancarDado();
                                        Console.WriteLine($"Valor do dado: {dado3}.");
                                        log.WriteLine($"Jogador {cont + 1} obteve o terceiro valor.");
                                        log.Flush();
                                        if (dado3 == 6)
                                        {
                                            Console.WriteLine($"Jogador {cont + 1} perdeu a vez!");
                                            log.WriteLine("jogada perdida");
                                            log.Flush();
                                        }
                                        else
                                        {
                                            do
                                            {
                                                do
                                                {
                                                    do
                                                    {
                                                        Console.WriteLine($"Seus dados sao {dado1}, {dado2} e {dado3}, qual dado deseja utilizar?");
                                                        dadoSelecionado = int.Parse(Console.ReadLine());
                                                    } while (dadoSelecionado != dado1 && dadoSelecionado != dado2 && dadoSelecionado != dado3);

                                                    log.WriteLine($"Jogador {cont + 1} escolheu o dado {dadoSelecionado}");
                                                    log.Flush();
                                                    Console.WriteLine("Digite o número do peao que deseja mover: ");
                                                    Console.WriteLine(tabuleiro.MostrarPeoes(cont + 1));
                                                    input = Console.ReadLine();

                                                    try
                                                    {
                                                        peaoDesejado = int.Parse(input);
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Por favor, digite um número válido.");
                                                        continue;
                                                    }

                                                } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dadoSelecionado) == false);
                                                resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dadoSelecionado);
                                                repeticao += tabuleiro.Captura(cont + 1, peaoDesejado);
                                                repeticao += tabuleiro.VerificarChegada(cont + 1, peaoDesejado);
                                                log.WriteLine("verificando.");
                                                log.Flush();

                                                if (dadoSelecionado == dado1)
                                                {
                                                    do
                                                    {
                                                        do
                                                        {
                                                            Console.WriteLine($"Seus dados sao {dado2} e {dado3}, qual dado deseja utilizar?");
                                                            dadoSelecionado = int.Parse(Console.ReadLine());
                                                        } while (dadoSelecionado != dado2 && dadoSelecionado != dado3);

                                                        log.WriteLine($"Jogador {cont + 1} escolheu o dado {dadoSelecionado}");
                                                        log.Flush();
                                                        Console.WriteLine("Digite o número do peao que deseja mover: ");
                                                        Console.WriteLine(tabuleiro.MostrarPeoes(cont + 1));
                                                        input = Console.ReadLine();

                                                        try
                                                        {
                                                            peaoDesejado = int.Parse(input);
                                                        }
                                                        catch (FormatException)
                                                        {
                                                            Console.WriteLine("Por favor, digite um número válido.");
                                                            continue;
                                                        }
                                                    } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dadoSelecionado) == false);
                                                    resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dadoSelecionado);
                                                    repeticao += tabuleiro.Captura(cont + 1, peaoDesejado);
                                                    repeticao += tabuleiro.VerificarChegada(cont + 1, peaoDesejado);
                                                    log.WriteLine("verificando.");
                                                    log.Flush();

                                                    if (dadoSelecionado == dado2)
                                                    {
                                                        dadoSelecionado = dado3;

                                                    }
                                                    else
                                                    {
                                                        dadoSelecionado = dado2;

                                                    }
                                                }
                                                else if (dadoSelecionado == dado2)
                                                {
                                                    do
                                                    {
                                                        do
                                                        {
                                                            Console.WriteLine($"Seus dados sao {dado1} e {dado3}, qual dado deseja utilizar?");
                                                            dadoSelecionado = int.Parse(Console.ReadLine());
                                                        } while (dadoSelecionado != dado1 && dadoSelecionado != dado3);

                                                        log.WriteLine($"Jogador {cont + 1} escolheu o dado {dadoSelecionado}");
                                                        log.Flush();
                                                        Console.WriteLine("Digite o número do peao que deseja mover:");
                                                        Console.WriteLine(tabuleiro.MostrarPeoes(cont + 1));
                                                        input = Console.ReadLine();

                                                        try
                                                        {
                                                            peaoDesejado = int.Parse(input);
                                                        }
                                                        catch (FormatException)
                                                        {
                                                            Console.WriteLine("Por favor, digite um número válido.");
                                                            continue;
                                                        }
                                                    } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dadoSelecionado) == false);
                                                    resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dadoSelecionado);
                                                    repeticao += tabuleiro.Captura(cont + 1, peaoDesejado);
                                                    repeticao += tabuleiro.VerificarChegada(cont + 1, peaoDesejado);
                                                    log.WriteLine("verificando.");
                                                    log.Flush();


                                                    if (dadoSelecionado == dado1)
                                                    {
                                                        dadoSelecionado = dado3;
                                                    }
                                                    else
                                                    {
                                                        dadoSelecionado = dado1;
                                                    }
                                                }
                                                else
                                                {
                                                    do
                                                    {
                                                        do
                                                        {
                                                            Console.WriteLine($"Seus dados sao {dado1} e {dado2}, qual dado deseja utilizar?");
                                                            dadoSelecionado = int.Parse(Console.ReadLine());
                                                        } while (dadoSelecionado != dado1 && dadoSelecionado != dado2);

                                                        log.WriteLine($"Jogador {cont + 1} escolheu o dado {dadoSelecionado}");
                                                        log.Flush();
                                                        Console.WriteLine("Digite o número do peao que deseja mover:");
                                                        Console.WriteLine(tabuleiro.MostrarPeoes(cont + 1));
                                                        input = Console.ReadLine();

                                                        try
                                                        {
                                                            peaoDesejado = int.Parse(input);
                                                        }
                                                        catch (FormatException)
                                                        {
                                                            Console.WriteLine("Por favor, digite um número válido.");
                                                            continue;
                                                        }
                                                    } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dadoSelecionado) == false);
                                                    resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dadoSelecionado);
                                                    repeticao += tabuleiro.Captura(cont + 1, peaoDesejado);
                                                    repeticao += tabuleiro.VerificarChegada(cont + 1, peaoDesejado);
                                                    log.WriteLine("Verificando.");
                                                    log.Flush();


                                                    if (dadoSelecionado == dado1)
                                                    {
                                                        dadoSelecionado = dado2;
                                                    }
                                                    else
                                                    {
                                                        dadoSelecionado = dado1;
                                                    }
                                                }
                                            } while (resposta == false);
                                        }
                                    }
                                    else
                                    {
                                        do
                                        {
                                            do
                                            {
                                                do
                                                {
                                                    Console.WriteLine($"Seus dados sao {dado1} e {dado2}, qual dado deseja utilizar?");
                                                    dadoSelecionado = int.Parse(Console.ReadLine());
                                                } while (dadoSelecionado != dado1 && dadoSelecionado != dado2);

                                                log.WriteLine($"Jogador {cont + 1} escolheu o dado {dadoSelecionado}");
                                                log.Flush();
                                                Console.WriteLine("Digite o número do peao que deseja mover: ");
                                                Console.WriteLine(tabuleiro.MostrarPeoes(cont + 1));
                                                input = Console.ReadLine();

                                                try
                                                {
                                                    peaoDesejado = int.Parse(input);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Por favor, digite um número válido.");
                                                    continue;
                                                }
                                            } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dadoSelecionado) == false);
                                            resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dadoSelecionado);
                                            repeticao += tabuleiro.Captura(cont + 1, peaoDesejado);
                                            repeticao += tabuleiro.VerificarChegada(cont + 1, peaoDesejado);
                                            log.WriteLine("Verificando.");
                                            log.Flush();


                                            if (dadoSelecionado == dado1)
                                            {
                                                dadoSelecionado = dado2;
                                            }
                                            else
                                            {
                                                dadoSelecionado = dado1;
                                            }
                                        } while (resposta == false);
                                    }
                                }
                                if (tabuleiro.VetorJogadores[cont].PeaoValido(cont, dadoSelecionado) == true)
                                {
                                    do
                                    {
                                        do
                                        {
                                            Console.WriteLine($"Digite o número do peao que deseja mover com o dado {dadoSelecionado}: ");
                                            Console.WriteLine(tabuleiro.MostrarPeoes(cont + 1));
                                            input = Console.ReadLine();
                                            log.WriteLine($"Jogador {cont + 1} escolheu o dado {input}");
                                            log.Flush();

                                            try
                                            {
                                                peaoDesejado = int.Parse(input);
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Por favor, digite um número válido.");
                                                continue;
                                            }
                                        } while (tabuleiro.VetorJogadores[cont].SelecionarPeao(peaoDesejado, dadoSelecionado) == false);
                                        resposta = tabuleiro.VetorJogadores[cont].VetorPeoes[peaoDesejado - 1].Mover(dadoSelecionado);
                                        repeticao += tabuleiro.Captura(cont + 1, peaoDesejado);
                                        repeticao += tabuleiro.VerificarChegada(cont + 1, peaoDesejado);
                                        log.WriteLine("Verificando.");
                                        log.Flush();

                                    } while (resposta == false);
                                }
                                else
                                {
                                    Console.WriteLine("Nao há peoes disponíveis");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Necessário um 6 para colocar peoes na casa inicial.");
                                log.WriteLine($"Jogador {cont + 1} nao tirou o dado necessário para liberar nenhum peao.");
                                log.Flush();
                            }
                            if (repeticao > 0)
                            {
                                Console.WriteLine("Role o dado novamente.");
                                log.WriteLine($"Jogador {cont + 1} rola o dado novamente.");
                                log.Flush();
                            }
                            else
                            {
                                Console.WriteLine($"Fim das rolagens de {tabuleiro.VetorJogadores[cont].Nome}.\n");
                                cont++;
                                log.WriteLine("Rolagens finalizadas.");
                                log.Flush();
                            }
                            if (cont == quantJogadores)
                            {
                                cont = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro!!!" + e.Message);
            }
        }
    }
}