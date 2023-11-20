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

    static char currentTile;
    static int cursory = 10;
    static int cursorx = 10;
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
                currentTile = mapRows[cursory][cursorx];
            }
            Console.WriteLine();
        }
            Console.Write("Current Tile Of The Player Position: " + currentTile);
    }


    static void PlayerUpdate()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);

        if (input.Key == ConsoleKey.W)
        {
            cursory--;
            if (cursory < 0) cursory = 0;
        }
        else if (input.Key == ConsoleKey.A)
        {
            cursorx--;
            if (cursorx < 0) cursorx = 0;
        }
        else if (input.Key == ConsoleKey.D)
        {
            cursorx++;
            if (cursorx > 50) cursorx = 50;
        }
        else if (input.Key == ConsoleKey.S)
        {
            cursory++;
            if (cursory > 50) cursory = 50;
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
            PlayerDraw(cursorx, cursory);
            PlayerUpdate();
            RenderMap();
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}
}
