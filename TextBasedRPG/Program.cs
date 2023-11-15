using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextBasedRPG
{
    internal class Program
    {

    static int y = 10;
    static int x = 10;
    static bool gameOver = false;

    static void PlayerDraw(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine("@");
    }

    static void RenderMap()
    {
        Console.SetCursorPosition(0, 0);
        string path = @"map.txt";

        string[] mapRows;

        mapRows = File.ReadAllLines(path);

        for (int y = 0; y < mapRows.Length; y++)
        {
            string mapRow = mapRows[y];
            for (int x = 0; x < mapRow.Length; x++)
            {
                char tile = mapRow[x];
                Console.Write(tile);
            }
            Console.WriteLine();
        }
    }


    static void PlayerUpdate()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);

        if (input.Key == ConsoleKey.W)
        {
            y--;
            if (y < 0) y = 0;
        }
        else if (input.Key == ConsoleKey.A)
        {
            x--;
            if (x < 0) x = 0;
        }
        else if (input.Key == ConsoleKey.D)
        {
            x++;
            if (x > 50) x = 50;
        }
        else if (input.Key == ConsoleKey.S)
        {
            y++;
            if (y > 50) y = 50;
        }
        else if (input.Key == ConsoleKey.Escape)
        {
            gameOver = true;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("MiniGame");
        Console.WriteLine();
        RenderMap();
        while (!gameOver)
        {
            PlayerDraw(x, y);
            PlayerUpdate();
            RenderMap();
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}
}
