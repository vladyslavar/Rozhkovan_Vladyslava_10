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
           // int level;
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
            
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("#");
                Console.SetCursorPosition(i, (height - 1));
                Console.Write("#");
            }
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("#");
                Console.SetCursorPosition((width - 1), i);
                Console.Write("#");
            }

            if (line == 6||line==8)//hole
            {
                for (int i = width / 3; i < (width / 3) * 2; i++)
                {
                    Console.SetCursorPosition(i, height / 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("#");
                    Console.SetCursorPosition(i, (height / 3) * 2);
                    Console.Write("#");
                }
                if (line == 8)
                {
                    for (int i = ((height / 4) * 2) + 2; i < ((height / 4) * 3) + 2; i++)
                    {
                        Console.SetCursorPosition((width / 3) - 3, i);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("#");
                        Console.SetCursorPosition(((width / 3) * 2) + 3, i);
                        Console.Write("#");
                    }
                    for (int i = ((height / 4) * 2) - 5; i < ((height / 4) * 3) - 5; i++)
                    {
                        Console.SetCursorPosition((width / 3) - 3, i);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("#");
                        Console.SetCursorPosition(((width / 3) * 2) + 3, i);
                        Console.Write("#");
                    }
                }
            }
            if (line == 7)//zig-saw
            {
                for (int i = 1; i < (width / 2) - 1; i++)
                {
                    Console.SetCursorPosition(i, height / 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("#");
                    Console.SetCursorPosition(width - i, (height / 3) * 2);
                    Console.Write("#");
                }
                for (int i = 1; i < (height / 2) - 2; i++)
                {
                    Console.SetCursorPosition((width / 3) * 2, i);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("#");
                    Console.SetCursorPosition((width / 3), (height - i));
                    Console.Write("#");
                }
            }
            

            //making a snake
            Console.SetCursorPosition(xSnake[0], ySnake[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("O");

            //making an apple 
            bool vso1;
            do
            {
                xApple = rand.Next(2, (width - 2));
                yApple = rand.Next(2, (height - 2));
                CheckingBoundaries(line, xApple, yApple, out vso1);
                Console.SetCursorPosition(xApple, yApple);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("o");
            } while (!vso1);


            ConsoleKey key = Console.ReadKey(true).Key;
            
            do
            {
                Console.SetCursorPosition(width + 5, 1);
                Console.Write("Score: " + numOfApples * 10);
                Console.SetCursorPosition(width + 5, 2);
                Console.Write("Speed: " + gameSpeed);

                //movings
                switch (key)
                {
                    case ConsoleKey.W:
                        Console.SetCursorPosition(xSnake[0], ySnake[0]);
                        Console.Write(" ");
                        ySnake[0]--;
                        break;
                    case ConsoleKey.S:
                        Console.SetCursorPosition(xSnake[0], ySnake[0]);
                        Console.Write(" ");
                        ySnake[0]++;
                        break;
                    case ConsoleKey.A:
                        Console.SetCursorPosition(xSnake[0], ySnake[0]);
                        Console.Write(" ");
                        xSnake[0]--;
                        break;
                    case ConsoleKey.D:
                        Console.SetCursorPosition(xSnake[0], ySnake[0]);
                        Console.Write(" ");
                        xSnake[0]++;
                        break;
                    
                }

                // кусь
                for (int i = 1; i <= numOfApples; i++)
                {
                    if (xSnake[0] == xSnake[i] && ySnake[0] == ySnake[i])
                    {
                        alive = false;
                        Console.SetCursorPosition(3, (height / 2));
                        Console.WriteLine("Змейка себя кусьнула. Змейке больно.");
                        Console.SetCursorPosition(0, (height + 2));
                             
                    }
                }

                //snake
                if (alive)
                {
                    Console.SetCursorPosition(xSnake[0], ySnake[0]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("O");

                    for (int i = 1; i < numOfApples + 1; i++)
                    {
                        Console.SetCursorPosition(xSnake[i], ySnake[i]);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("O");
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

                        } while (!vso && !vso2);
                        Console.SetCursorPosition(xApple, yApple);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("o");
                        appleEaten = false;
                    }

                    Thread.Sleep(speed);
                    if (Console.KeyAvailable) { key = Console.ReadKey(true).Key; }
                }

            } while (alive);

            Thread.Sleep(500);
            Environment.Exit(0);
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
                    if ((xPossition < ((width / 2) - 1) && yPossition == height / 3) || (xPossition > ((width - (width / 2)) + 3) && yPossition == (height / 3) * 2) || (xPossition == ((width / 3) * 2) && yPossition < ((height / 2) - 2)) || (xPossition == (width / 3) && yPossition > (height - (height / 2)) + 3 ))
                    {
                        possition = false;
                    }
                }
            }
            else possition = true;
        }
    }

}

