using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.SymbolStore;

namespace TextBasedRPG
{
    internal class Program
    {
    static bool enemyDead = false;
        static bool playerDead = false;
        static char nextTileUp;
    static char nextTileDown;
    static char nextTileLeft;
    static char nextTileRight;
    static char currentTile;
    static int cursory = 10;
    static int cursorx = 10;
    static int enemyCursory = 10;
    static int enemyCursorx = 16;
    static bool gameOver = false;
        static bool playerVictory = false;

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
                    if (cursorx < 33 && cursorx > 0 && cursory < 16 && cursory > 0)
                    {
                        currentTile = mapRows[cursory][cursorx];
                    }
                    //if (cursory <= 0) return;
                    if (cursory > 0)
                    {
                        nextTileUp = mapRows[cursory - 1][cursorx];
                    }
                    if (mapRows.Length - 1 > cursory)
                    {
                        nextTileDown = mapRows[cursory + 1][cursorx];
                    }
                    if (cursorx > 0)
                    {
                        nextTileLeft = mapRows[cursory][cursorx - 1];
                    }
                    if (cursorx < mapRow.Length - 1)
                    {
                        nextTileRight = mapRows[cursory][cursorx + 1];
                    }
                    //Console.WriteLine("MapRow Length" + mapRow.Length);
                }
            Console.WriteLine();
        }   
            Console.WriteLine("Current Tile Of The Player Position: " + currentTile);
            Console.WriteLine("Next Tile Up From The Player Position: " + nextTileUp);
            Console.WriteLine("Next Tile Down From The Player Position: " + nextTileDown);
            Console.WriteLine("Next Tile Left From The Player Position: " + nextTileLeft);
            Console.WriteLine("Next Tile Right From The Player Position: " + nextTileRight);
            Console.WriteLine("Cursor X " + cursorx);
            Console.WriteLine("Cursor Y " + cursory);
            Console.WriteLine("Enemy Cursor X " + enemyCursorx);
            Console.WriteLine("Enemy Cursor Y " + enemyCursory);
            Console.WriteLine("Player Dead: " + playerDead);
            Console.WriteLine("GameOver " + gameOver);
            Console.WriteLine("Player Victory " + playerVictory);

        }


    static void PlayerUpdate()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);

        if (input.Key == ConsoleKey.W)
        {
            cursory--;
                if (cursory < 0) cursory = 0;
                else if (nextTileUp == '^') cursory++;
                else if (enemyCursorx == cursorx && enemyCursory == cursory - 1) { enemyDead = true; playerVictory = true; }
        }
        else if (input.Key == ConsoleKey.A)
        {
            cursorx--;
                if (cursorx < 0) cursorx = 0;
                else if (nextTileLeft == '^') cursorx++;
                else if (enemyCursorx == cursorx - 1 && enemyCursory == cursory) { enemyDead = true; playerVictory = true; }
            }
        else if (input.Key == ConsoleKey.D)
        {
            cursorx++;
                if (cursorx > 33) cursorx = 33;
                else if (nextTileRight == '^') cursorx--;
                else if (enemyCursorx == cursorx + 1 && enemyCursory == cursory) { enemyDead = true; playerVictory = true; }
            }
        else if (input.Key == ConsoleKey.S)
        {
            cursory++;
                if (cursory > 15) cursory = 15;
                else if (nextTileDown == '^') cursory--;
                else if (enemyCursorx == cursorx && enemyCursory == cursory + 1) { enemyDead = true; playerVictory = true; }
            }
        else if (input.Key == ConsoleKey.Escape)
        {
            gameOver = true;
        }

        if (playerDead)
            {
                gameOver = true;
            }
    }


    static void EnemyDraw(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine("G");
    }

    static void EnemyUpdate()
        {
            Random random = new Random();
            int direction = random.Next(0, 4);
            if (playerDead)
            {
                gameOver = true;
            }
            else if (enemyCursorx - 1 == cursorx)
            {
                direction = 0;
            }
            else if (enemyCursorx + 1 == cursorx)
            {
                direction = 3;
            }
            else if (enemyCursory - 1 == cursory)
            {
                direction = 2;
            }
            else if (enemyCursory + 1 == cursory)
            {
                direction = 1;
            }
            if (!enemyDead)
            {
                switch (direction)
                {
                    case 0:
                        enemyCursorx--;
                        if (enemyCursorx < 0) enemyCursorx = 0;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) { playerDead = true; }
                        break;
                    case 1:
                        enemyCursory++;
                        if (enemyCursory > 15) enemyCursory = 15;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) { playerDead = true; }
                        break;
                    case 2:
                        enemyCursory--;
                        if (enemyCursory < 0) enemyCursory = 0;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) { playerDead = true; }
                        break;
                    case 3:
                        enemyCursorx++;
                        if (enemyCursorx > 33) enemyCursorx = 33;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) { playerDead = true; }
                        break;
                }
            }
        }

    static void Main(string[] args)
    {
        Console.WriteLine("MiniGame");
        Console.WriteLine();
        RenderMap();
        while (!!gameOver || !playerVictory)
        {
            if (!playerDead)
                {
                    PlayerDraw(cursorx, cursory);
                }
            if(!enemyDead)
            {
                EnemyDraw(enemyCursorx, enemyCursory);
            }
            if (!playerDead)
                {
                    PlayerUpdate();
                }
            if (!enemyDead)
            {
                EnemyUpdate();
            }
            RenderMap();
        }
        if (playerVictory)
            {
                Console.Clear();
                Console.WriteLine("Victory");
            }
        if (gameOver)
            {
                Console.Clear();
                Console.WriteLine("Game Over");
            }
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}
}
