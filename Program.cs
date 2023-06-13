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
        public static int[] previousHeadPosition = new int[2];
        public static int points;
        public static int rows;
        public static int bodyCount = 0;
        public static int foodCount = 0;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ShowMenu();
            while (inGame)
            {
                Console.Clear();
                PrintBoard();
                ReadInput();
                GenerateFood();
                
            }
        }
        public static void ShowMenu()
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

            Console.WriteLine("Introduce el numero de lineas que quieras (Min: 11, impar)");
            int lines = 0;

            bool numValido = true;
            while (numValido) 
            {
                while (!int.TryParse(Console.ReadLine(), out lines)) 
                {
                    Console.WriteLine("Solo se permiten numeros");
                }
                if (lines % 2 == 0 || lines < 0 || lines == 0)
                {
                    Console.WriteLine("Porfavor introduce un numero impar positvo");
                }
                else if (lines < 11)
                { 
                    Console.WriteLine("El numero minimo es 11, porfavor prueba con uno más grande");
                }
                else
                {
                    numValido = false;
                }

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

        public static void GenerateFood()
        {
            if (foodCount == 0)
            {
                int[] FoodPos;
                FoodPos = new int[2];
                Random rand = new Random();
                while (board[FoodPos[0],FoodPos[1]] != '\0')
                {
                    FoodPos[0] = rand.Next(0, rows);
                    FoodPos[1] = rand.Next(0, rows);
                }

                board[FoodPos[0], FoodPos[1]] = '*';
                foodCount = 1;
            }
        }

        public static void CellCheck(int[] checkPos)
        {

            if (board[checkPos[0], checkPos[1]] == '*')
            {
                points++;
                foodCount = 0;
            }

            if (points >= 10)
            {
                Console.Clear();
                Console.WriteLine("FELICIDADES HAS GANADO!!");
                Thread.Sleep(3000);
                inGame = false;
            }
        }

        public static void HeadMovement (String teclaPulsada)
        {
            previousHeadPosition[0] = snkHeadPos[0];
            previousHeadPosition[1] = snkHeadPos[1];
            switch (teclaPulsada)
            {
                case "Abajo":
                    if (board[previousHeadPosition[0], previousHeadPosition[1]] == board[rows, previousHeadPosition[1]])
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[0] = 0;
                    }
                    else
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[0] += 1;                        
                    }
                    break;
                case "Arriba":
                    if (board[previousHeadPosition[0], previousHeadPosition[1]] == board[0, previousHeadPosition[1]])
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[0] = rows;
                    }
                    else
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[0] -= 1;
                    }
                    break;

                case "Derecha":
                    if (board[previousHeadPosition[0], previousHeadPosition[1]] == board[previousHeadPosition[0], rows])
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[1] = 0;
                    }
                    else
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[1] += 1;
                    }
                    break;
                case "Izquierda":
                    if (board[previousHeadPosition[0], previousHeadPosition[1]] == board[previousHeadPosition[0], 0])
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[1] = rows;
                    }
                    else
                    {
                        board[previousHeadPosition[0], previousHeadPosition[1]] = '■';
                        snkHeadPos[1] -= 1;
                    }
                    break;

            }
            CellCheck(snkHeadPos);
        }
        public static void TailMovement()
        {
            board[snkTailPos[0], snkTailPos[1]] = ' ';
            snkTailPos[0] = previousHeadPosition[0];
            snkTailPos[1] = previousHeadPosition[1];

        }

        public static void ReadInput()
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
            TailMovement();
        }

        
        public static void PrintBoard()
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
