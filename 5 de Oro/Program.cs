using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_de_Oro
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] Apuesta = new int[5];
            int[,] Jugadas = new int[10, 5];
            Menu(Jugadas, Apuesta);
            Console.ReadLine();
        }

        static public void Menu(int[,] Jugadas, int[] Apuesta)
            {
                
                bool Salir = true;
                string OpcionMenu = "";
                int[] contador = new int[49];

                while (Salir)
                {
                    Console.Clear();
                    Console.WriteLine("        ******************************************************************************");
                    Console.WriteLine("        **           Ejercicio Obligatorio *** 5 de Oro ***                         **");
                    Console.WriteLine("        **                                                                          **");
                    Console.WriteLine("        **        1- Generar las 10 jugadas.                                        **");
                    Console.WriteLine("        **        2- Apostar.                                                       **");
                    Console.WriteLine("        **        3- Mostrar la apuesta.                                            **");
                    Console.WriteLine("        **        4- Mostrar las 10 jugadas.                                        **");
                    Console.WriteLine("        **        5- Mostrar el número que más salió en las 10 jugadas.             **");
                    Console.WriteLine("        **        6- Salir.                                                         **");
                    Console.WriteLine("        **                                                                          **");
                    Console.WriteLine("        ****************************************************************************** \n");


                    OpcionMenu = Console.ReadLine();

                    switch (OpcionMenu)
                    {
                        case "1": 
                            {
                                Console.Clear();
                                GenerarJugada(Jugadas);
                                Console.WriteLine("Se an generado las jugadas");
                                Console.ReadLine();
                                break;
                            }
                        case "2": 
                            {
                                Console.Clear();

                                if (!JugadasCargadas(Jugadas))
                                {
                                    Console.WriteLine("Error debe ingresar las 10 ultimas jugadas antes de poder apostar (opcion [1])");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Apuesta = new int[5];
                                    HacerJugada(Apuesta, Jugadas);
                                    VerificacionSorteo(Jugadas, Apuesta);
                                }
                                break;
                            }
                        case "3": 
                            {
                                Console.Clear();
                                MostrarJugada(Apuesta);
                                break;
                            }
                        case "4": 
                            {
                                Console.Clear();
                                MuestroJugadasRandom(Jugadas);
                                break;
                            }
                        case "5": 
                            {
                                Console.Clear();
                                CargarRepetidos(Jugadas, contador);
                                NumeroRepetido(contador);
                                break;
                            }
                        case "6": 
                            {
                                Salir = false;
                                Console.Clear();
                                Console.WriteLine(" Gracias por usar el programa");
                                Console.ReadLine();
                                break;
                            }
                        default: 
                            {
                                Console.WriteLine("ERROR - Ingrese una opcion valida");
                                Console.ReadLine();
                                break;
                            }
                    }

                }
            }

        public static void GenerarJugada(int[,] Jugadas)
        {
            Random aleatorio = new Random();           
            for (int indiceC = 0; indiceC < Jugadas.GetLength(1); indiceC++)
            {         
                for (int indiceF = 0; indiceF < Jugadas.GetLength(0); indiceF++)
                {
                    bool flag = true;
                    while (flag)
                    {
                        int valor = aleatorio.Next(1, 49);
                        if (VerificoValorAleatorio(Jugadas, indiceF, valor))
                        {
                            Jugadas[indiceF, indiceC] = valor;
                            flag = false;
                        }
                    }
                }                
            }
        }

        public static void MuestroJugadasRandom(int[,] Jugadas)
        {
            for (int indiceC = 0; indiceC < Jugadas.GetLength(1); indiceC++)
            {
                for (int indiceF = 0; indiceF < Jugadas.GetLength(0); indiceF++)
                {

                    Console.Write("" + Jugadas[indiceF, indiceC] + " \t");
                }
                Console.ReadLine();
            }
        }

        public static void HacerJugada(int[] Apuesta, int[,] jugadas)
        {

            string leodato = "";
            bool flag = true;
            int numero = 0;

            for (int indice = 0; indice <= Apuesta.Length - 1; indice++)
            {
                flag = true;
                while (flag)
                {
                    Console.Clear();
                    Console.Write("ingrese el numero de la jugada " + indice + ": ");
                    leodato = Console.ReadLine();

                    try
                    {
                        numero = Convert.ToInt32(leodato);

                        if ((numero > 0) && (numero <= 48))
                        {
                            if (VerificoJIngresada(Apuesta, numero))
                            {
                                Apuesta[indice] = numero;
                                flag = false;
                            }
                            else
                            {
                                Console.WriteLine("Error - numero repetido");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Write("El numero ingresado no es correto");
                            Console.ReadLine();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error de ingreso");
                        Console.ReadLine();
                    }
                }

            }
        }

        public static void MostrarJugada(int[] Apuesta)
        {
            for (int indice = 0; indice <= Apuesta.Length - 1; indice++)
            {
                Console.WriteLine("Su apuesta " + indice + " es: " + Apuesta[indice]);
            }
            Console.ReadLine();
        }

        public static void CargarRepetidos(int[,] Jugadas, int[] contador)
        {


            for (int numeroP = 1; numeroP < contador.Length; numeroP++)
            {
                for (int indiceF = 0; indiceF < Jugadas.GetLength(0); indiceF++)
                {
                    for (int indiceC = 0; indiceC < Jugadas.GetLength(1); indiceC++)
                    {
                        if (numeroP == Jugadas[indiceF, indiceC])
                            contador[numeroP] = contador[numeroP] + 1;
                    }
                }
            }


        }

        public static void NumeroRepetido(int[] contador)
        {
            
            int cant = contador[0];
            int indice = 0;

            for (int i = 0; i < contador.Length; i++)
            {
                if (cant < contador[i])
                {
                    cant = contador[i];
                    indice = i;
                }
            }
            Console.WriteLine("El numero mas repetido es: " + indice + " se repitio " + cant + " veces.");
            Console.ReadLine();
        }

        public static bool VerificoJIngresada(int[] Apuesta, int numero)
        {
            for (int indice1 = 0; indice1 <= Apuesta.Length - 1; indice1++)
            {
                if (numero == Apuesta[indice1])
                {
                    return false;
                }
            }
            return true;
        }

        public static void VerificacionSorteo(int[,] jugadas, int[] Apuesta)
        {
            for (int indiceF = 0; indiceF < jugadas.GetLength(0); indiceF++)
            {
                int[] VectorX = new int[5];
                for (int indiceC = 0; indiceC < jugadas.GetLength(1); indiceC++)
                {
                    VectorX[indiceC] = jugadas[indiceF, indiceC];

                }
                if (ResultadoSorteo(Apuesta, VectorX))
                {
                    Console.WriteLine("Felicidades ha ganado!!");
                    Console.ReadLine();
                    return;
                }

            }
            Console.WriteLine("No ganaste");
            Console.ReadLine();

        }

        public static bool ResultadoSorteo(int[] Apuesta, int[] VectorJugadas)
        {
            int cont = 0;

            for (int indiceApuesta = 0; indiceApuesta < Apuesta.Length; indiceApuesta++)


                for (int indiceJugada = 0; indiceJugada < Apuesta.Length; indiceJugada++)
                {
                    if (Apuesta[indiceApuesta] == VectorJugadas[indiceJugada])
                    {
                        cont = cont + 1;
                        if (cont == 5)
                            return true;
                        break;
                    }


                }
            return false;
        }

        public static bool VerificoValorAleatorio(int[,] Verifico, int indice, int valor )
        {
          for(int indiceC = 0; indiceC < Verifico.GetLength(1); indiceC++)
          {
            if(valor == Verifico[indice, indiceC])
              {
                  return false;
              }
          }
          return true;
           
        }
        
        public static bool JugadasCargadas(int[,] jugadas)
        {
            for (int indiceF = 0; indiceF < jugadas.GetLength(0); indiceF++)
            {

                for (int indiceC = 0; indiceC < jugadas.GetLength(1); indiceC++)
                {
                    if (jugadas[indiceF, indiceC] == 0)
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
    }

