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
        public static int bodyCount = 0;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
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
            int lines = 0;
            while (lines % 2 == 0 || lines < 0 ) 
            {
                while (!int.TryParse(Console.ReadLine(), out lines)) 
                {
                    Console.WriteLine("introduce numero");
                }
                if(lines % 2 == 0 || lines < 0)
                    Console.WriteLine("Porfavor introduce un numero impar positvo");
            }

                    
            
            board = new char[lines, lines];
            snkHeadPos[0] = lines/2;
            snkHeadPos[1] = lines/2;
            snkTailPos[0] = lines/2;
            snkTailPos[1] = lines/2;

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
            
        }

        private static void HeadMovement (String teclaPulsada)
        {
            int[] previousPos = snkHeadPos;
            switch (teclaPulsada)
            {
                case "Abajo":
                    if (board[previousPos[0], previousPos[1]] == board[rows, previousPos[1]])
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[0] = 0;
                    }
                    else
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[0] += 1;                        
                    }
                    break;
                case "Arriba":
                    if (board[previousPos[0], previousPos[1]] == board[0, previousPos[1]])
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[0] = rows;
                    }
                    else
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[0] -= 1;
                    }
                    break;

                case "Derecha":
                    if (board[previousPos[0], previousPos[1]] == board[previousPos[0], rows])
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[1] = 0;
                    }
                    else
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[1] += 1;
                    }
                    break;
                case "Izquierda":
                    if (board[previousPos[0], previousPos[1]] == board[previousPos[0], 0])
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[1] = rows;
                    }
                    else
                    {
                        board[previousPos[0], previousPos[1]] = '■';
                        snkHeadPos[1] -= 1;
                    }
                    break;
            }
        }
        private static void TailMovement(String teclaPulsada)
        {
            int[] previousPos = snkTailPos;
            switch (teclaPulsada)
            {
                case "Abajo":
                    board[previousPos[0], previousPos[1]] = ' ';
                    snkTailPos[0] += 1;
                    break;

                case "Arriba":
                    board[previousPos[0], previousPos[1]] = ' ';
                    snkTailPos[0] -= 1;
                    break;

                case "Derecha":
                    board[previousPos[0], previousPos[1]] = ' ';
                    snkTailPos[1] += 1;
                    break;

                case "Izquierda":
                    board[previousPos[0], previousPos[1]] = ' ';
                    snkTailPos[1] -= 1;
                    break;
            }
        }

        private static void ReadInput()
        {
            Console.WriteLine("Recuerda, te mueves con las flechas ;)");
            string tecla = "";
           
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    tecla = "Abajo";
                    break;
                case ConsoleKey.UpArrow:
                    tecla = "Arriba";
                    break;
                case ConsoleKey.LeftArrow:
                    tecla = "Izquierda";
                    break;
                case ConsoleKey.RightArrow:
                    tecla = "Derecha";
                    break;                   
            }
            HeadMovement(tecla);

            if (bodyCount > 3)
                TailMovement(tecla);
            else
                bodyCount++;
        }

        
        private static void PrintBoard()
        {
            Console.WriteLine("Points: " + points);
            board[snkHeadPos[0], snkHeadPos[1]] = '■';
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
