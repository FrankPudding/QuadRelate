using QuadRelate.Contracts;
using QuadRelate.Types;
using System;

namespace QuadRelate.Externals
{
    public class BoardDrawerConsole : IBoardDrawer
    {
        public void DrawBoard(Board board)
        {
            Console.Clear();

            for (var y = Board.Height - 1; y >= -1; y--)
            {
                if (y != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("|");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("-");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                for (var x = 0; x < Board.Width; x++)
                {
                    if (y != -1)
                    {
                        switch (board[x, y])
                        {
                            case Counter.Empty:
                                Console.Write(" ");
                                break;

                            case Counter.Red:
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write('\u20DD');
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;

                            case Counter.Yellow:
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write('\u20DD');
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("-");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    if (y != -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("-");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }

                Console.Write("\n");
            }
        }
    }
}
