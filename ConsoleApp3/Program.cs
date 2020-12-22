using System;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 21;
            int width = 41;
            int[] xSnake = new int[800];
            xSnake[0] = 3;
            int[] ySnake = new int[800];
            ySnake[0] = 19;
            int xApple;
            int yApple;
            bool alive = true;
            bool appleEaten = false;
            int numOfApples = 0;
            int speed = 300;
            int gameSpeed = 0;

            Random rand = new Random();

            int line = 5;
            ConsoleKey alsoKey;

            Console.SetCursorPosition(5, 3);
            Console.WriteLine("CHOOSE THE LEVEL:\n");
            Console.SetCursorPosition(5, 5);
            Console.Write("DEFAULT");
            Console.SetCursorPosition(5, 6);
            Console.Write("HOLE");
            Console.SetCursorPosition(5, 7);
            Console.Write("ZIG-SAW");
            Console.SetCursorPosition(5, 8);
            Console.Write("CROSSBAR");
            Console.SetCursorPosition(4, 5);
            do
            {
                ConsoleKey key1 = Console.ReadKey(true).Key;
                switch (key1)
                {
                    case ConsoleKey.UpArrow:
                        line--;
                        if (line < 5) { line = 8; }
                        Console.SetCursorPosition(4, line);
                        break;
                    case ConsoleKey.DownArrow:
                        line++;
                        if (line > 8) { line = 5; }
                        Console.SetCursorPosition(4, line);
                        break;
                }

                alsoKey = key1;

            } while (alsoKey != ConsoleKey.Enter);
            Console.Clear();
            Console.CursorVisible = false;
            //build a board
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 1; i < width; i++)
            {
                Drawings(i, 1, "#");
                Drawings(i, (height - 1), "#");
            }
            for (int i = 1; i < height; i++)
            {
                Drawings(1, i, "#");
                Drawings((width - 1), i, "#");
            }

            if (line == 6 || line == 8)//hole
            {
                for (int i = width / 3; i < (width / 3) * 2; i++)
                {
                    Drawings(i, height / 3, "#");
                    Drawings(i, (height / 3) * 2, "#");
                }
                if (line == 8)
                {
                    for (int i = ((height / 4) * 2) + 2; i < ((height / 4) * 3) + 2; i++)
                    {
                        Drawings((width / 3) - 3, i, "#");
                        Drawings(((width / 3) * 2) + 3, i, "#");
                    }
                    for (int i = ((height / 4) * 2) - 5; i < ((height / 4) * 3) - 5; i++)
                    {
                        Drawings((width / 3) - 3, i, "#");
                        Drawings(((width / 3) * 2) + 3, i, "#");
                    }
                }
            }
            if (line == 7)//zig-saw
            {
                for (int i = 1; i < (width / 2) - 1; i++)
                {
                    Drawings(i, height / 3, "#");
                    Drawings(width - i, (height / 3) * 2, "#");
                }
                for (int i = 1; i < (height / 2) - 2; i++)
                {
                    Drawings((width / 3) * 2, i, "#");
                    Drawings((width / 3), (height - i), "#");
                }
            }


            //making a snake
            Console.ForegroundColor = ConsoleColor.Green;
            Drawings(xSnake[0], ySnake[0], "O");

            //making an apple 
            bool vso1;
            do
            {
                xApple = rand.Next(2, (width - 2));
                yApple = rand.Next(2, (height - 2));
                CheckingBoundaries(line, xApple, yApple, out vso1);
            } while (!vso1);
            Console.ForegroundColor = ConsoleColor.Red;
            Drawings(xApple, yApple, "o");


            ConsoleKey key = Console.ReadKey(true).Key;
            do
            {
                Console.SetCursorPosition(width + 5, 1);
                Console.Write("Score: " + numOfApples * 10);
                Console.SetCursorPosition(width + 5, 2);
                Console.Write("Speed: " + gameSpeed);

                //movings
                Console.SetCursorPosition(xSnake[0], ySnake[0]);
                Console.Write(" ");
                switch (key)
                {
                    case ConsoleKey.W:
                        ySnake[0]--;
                        break;
                    case ConsoleKey.S:
                        ySnake[0]++;
                        break;
                    case ConsoleKey.A:
                        xSnake[0]--;
                        break;
                    case ConsoleKey.D:
                        xSnake[0]++;
                        break;

                }

                // bite
                for (int i = 1; i <= numOfApples; i++)
                {
                    if (xSnake[0] == xSnake[i] && ySnake[0] == ySnake[i])
                    {
                        alive = false;
                        Console.SetCursorPosition(10, (height / 2));
                        Console.WriteLine("The snake bit itself.");
                        Console.SetCursorPosition(0, (height + 2));
                    }
                }

                //snake
                if (alive)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Drawings(xSnake[0], ySnake[0], "O");

                    for (int i = 1; i < numOfApples + 1; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Drawings(xSnake[i], ySnake[i], "O");
                    }
                    Console.SetCursorPosition(xSnake[numOfApples + 1], ySnake[numOfApples + 1]);
                    Console.WriteLine(" ");
                    for (int i = numOfApples + 1; i > 0; i--)
                    {
                        xSnake[i] = xSnake[i - 1];
                        ySnake[i] = ySnake[i - 1];
                    }

                    //boundaries and apple
                    CheckingBoundaries(line, xSnake[0], ySnake[0], out alive);
                    if (!alive)
                    {
                        Console.SetCursorPosition(6, (height / 2));
                        Console.WriteLine("The snake reached a boundary");
                        Console.SetCursorPosition(0, (height + 3));
                    }


                    if (xSnake[0] == xApple && ySnake[0] == yApple)
                    {
                        appleEaten = true;
                        speed = speed - 10;
                        gameSpeed += 1;

                    }
                    //apple apearing
                    if (appleEaten)
                    {
                        bool vso = false;
                        bool vso2;
                        numOfApples++;
                        do
                        {
                            xApple = rand.Next(2, (width - 2));
                            yApple = rand.Next(2, (height - 2));
                            CheckingBoundaries(line, xApple, yApple, out vso2);
                            for (int i = 0; i < numOfApples + 1; i++)
                            {
                                if (xApple != xSnake[i] && yApple != ySnake[i]) { vso = true; }
                            }

                        } while (!vso || !vso2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Drawings(xApple, yApple, "o");
                        appleEaten = false;
                    }

                    Thread.Sleep(speed);
                    if (Console.KeyAvailable) { key = Console.ReadKey(true).Key; }
                }

            } while (alive);

            Thread.Sleep(500);
            Environment.Exit(0);
        }
        static void Drawings(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
        static void CheckingBoundaries(int line, int xPossition, int yPossition, out bool possition, int height = 21, int width = 41)
        {
            if (line == 5 || line == 6 || line == 7 || line == 8)
            {
                if (xPossition == 1 || xPossition == (width - 1) || yPossition == 1 || yPossition == (height - 1)) { possition = false; }
                else possition = true;
                if (line == 6 || line == 8)
                {
                    if (((yPossition == height / 3) && (xPossition >= width / 3 && xPossition < (width / 3) * 2)) || ((yPossition == (height / 3) * 2)) && (xPossition >= width / 3 && xPossition < (width / 3) * 2)) { possition = false; }

                    if (line == 8)
                    {
                        if ((xPossition == (width / 3) - 3 || xPossition == ((width / 3) * 2) + 3) && (((yPossition >= ((height / 4) * 2) - 5) && (yPossition < ((height / 4) * 3) - 5)) || ((yPossition >= ((height / 4) * 2) + 2) && yPossition < ((height / 4) * 3) + 2)))
                        {
                            possition = false;
                        }
                    }

                }
                if (line == 7)
                {
                    if ((xPossition < ((width / 2) - 1) && yPossition == height / 3) || (xPossition > ((width - (width / 2)) + 3) && yPossition == (height / 3) * 2) || (xPossition == ((width / 3) * 2) && yPossition < ((height / 2) - 2)) || (xPossition == (width / 3) && yPossition > (height - (height / 2)) + 3))
                    {
                        possition = false;
                    }
                }
            }
            else possition = true;
        }
    }
}