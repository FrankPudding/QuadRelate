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
                    Console.Write("|");
                }
                else
                {
                    Console.Write("-");
                }

                for (var x = 0; x < Board.Width; x++)
                {
                    if (y != -1)
                    {
                        switch (board.Position[x, y])
                        {
                            case Cell.Empty:
                                Console.Write(" ");
                                break;

                            case Cell.Red:
                                Console.Write("R");
                                break;

                            case Cell.Yellow:
                                Console.Write("Y");
                                break;
                        }
                    }
                    else
                    {
                        Console.Write("-");
                    }

                    if (y != -1)
                    {
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }

                Console.Write("\n");
            }
        }
    }
}
