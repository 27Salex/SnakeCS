using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Program
    {
        public static char[,] board;
        public static bool inGame;
        public static int[] snkHeadPos = new int[2];
        public static int[] snkTailPos = new int[2];
        public static int points;
        public static int rows;

        static void Main(string[] args)
        {
            ShowMenu();
            while (inGame)
            {
                Console.Clear();
                PrintBoard();
                ReadInput();
            }
        }
        private static void ShowMenu()
        {
            Console.SetWindowSize(100, 25);

            //Esto es el titulo
            Console.WriteLine("" +
               "     _______..__   __.      ___       __  ___  _______   \r\n" +
               "    /       ||  \\ |  |     /   \\     |  |/  / |   ____|  \r\n" +
               "   |   (----`|   \\|  |    /  ^  \\    |  '  /  |  |__   \r\n" +
               "    \\   \\    |  . `  |   /  /_\\  \\   |    <   |   __|\r\n" +
               ".----)   |   |  |\\   |  /  _____  \\  |  .  \\  |  |____\r\n" +
               "|_______/    |__| \\__| /__/     \\__\\ |__|\\__\\ |_______|");

            Console.WriteLine("Introduce el numero de lineas que quieras");
            int lines;
            while (!int.TryParse(Console.ReadLine(), out lines))
            {
                while (lines % 2 == 0 )
                    Console.WriteLine("Porfavor introduce un numero impar");
            }

                    
            
            board = new char[lines, lines];
            snkHeadPos[0] = lines/2;
            snkHeadPos[1] = lines/2;
            lines -= 1;
            inGame = true;
            points = 0;
            rows = lines;

            Console.WriteLine("Presiona cualquier tecla para comenzar...");
            Console.ReadKey();
        }

        private static void CheckWin()
        {
            throw new NotImplementedException();
        }

        private static void CellCheck()
        {
            throw new NotImplementedException();
        }

        private static void ReadInput()
        {
            bool teclaCorrecta = false;
            //Console.WriteLine("Recuerda, te mueves con las flechas ;)");

            while (!teclaCorrecta) 
            { 
        
                switch (Console.ReadKey().Key)
                {
                        case ConsoleKey.DownArrow:
                            snkHeadPos[0] += 1;
                            teclaCorrecta = true;
                            break;
                        case ConsoleKey.UpArrow:
                            snkHeadPos[0] -= 1;
                            teclaCorrecta = true;
                            break;
                        case ConsoleKey.LeftArrow:
                            snkHeadPos[1] -= 1;
                            teclaCorrecta = true;
                            break;
                        case ConsoleKey.RightArrow:
                            snkHeadPos[1] += 1;
                            teclaCorrecta = true;
                            break;
                        default:
                        Console.WriteLine("Te he dicho las flechas merluzo!! >:(");
                        break;
                }

            }
        }
 
        private static void PrintBoard()
        {
            Console.WriteLine("Points: " + points);
            board[snkHeadPos[0], snkHeadPos[1]] = '@';

            Console.Write("╔");
            for (int i = 0; i <= rows * 2 + 1; i++)
            {
                Console.Write('═');
            }

            Console.WriteLine("╗");

            for (int i = 0; i <= rows; i++)
            {
                Console.Write("║");
                for (int j = 0; j <= rows; j++)
                {
                    Console.Write(board[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine("║");

            }

            Console.Write("╚");
            for (int i = 0; i <= rows * 2 + 1; i++)
            {
                Console.Write('═');
            }

            Console.WriteLine('╝');
        }
    }
}
