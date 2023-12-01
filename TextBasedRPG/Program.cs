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
        #region Player Death
        static bool playerDead = false;
        #endregion


        #region Enemy Death
        static bool enemyDead = false;
        #endregion

        #region Enemy Cursor Position
        static int enemyCursory = 10;
        static int enemyCursorx = 16;
        #endregion

        #region Player Surrounding Tiles Check
        static char nextTileUp;
        static char nextTileDown;
        static char nextTileLeft;
        static char nextTileRight;
        #endregion

        #region Player Cursor Positions
        static int cursory = 10;
        static int cursorx = 10;
        #endregion

        #region Enemy Surrounding Tiles Check
        static char enemyNextTileUp;
        static char enemyNextTileDown;
        static char enemyNextTileLeft;
        static char enemyNextTileRight;
        #endregion

        #region GameOver States
        static bool gameOver = false;
        static bool playerVictory = false;
        #endregion

        #region Health
        static int enemyHealth = 4;
        static int playerHealth = 4;
        #endregion

        #region Enemy was Attacked Check
        static bool enemyWasAttacked;
        #endregion


        #region Map Organization

        #region Border Offset
        static int borderOffset = 1;
        #endregion

        #region Wall Tile Char
        static char wallTile = '^';
        #endregion

        #region Map
        static string path = @"map.txt";
        static string[] mapRows = File.ReadAllLines(path);
        #endregion

        #region MapX Axis Length
        static int mapXLength = mapRows[0].Length;
        #endregion

        static void RenderMap()
        {
            Console.SetCursorPosition(0, 0);
        

            Console.Write('+');
            for (int i = 0; i < mapXLength; i++)
            {
                Console.Write('-');
            }
            Console.Write('+');
            Console.WriteLine();
            for (int y = 0; y < mapRows.Length; y++)
            {
            Console.Write('|');
            string mapRow = mapRows[y];
            for (int x = 0; x < mapRow.Length; x++)
            {
                char tile = mapRow[x];
                Console.Write(tile);
                    if (cursory - borderOffset > 0)
                    {
                        nextTileUp = mapRows[cursory - 1 - borderOffset][cursorx - borderOffset];
                    }
                    if (mapRows.Length - 1 > cursory - borderOffset)
                    {
                        nextTileDown = mapRows[cursory + 1 - borderOffset][cursorx - borderOffset];
                    }
                    if (cursorx - borderOffset > 0)
                    {
                        nextTileLeft = mapRows[cursory - borderOffset][cursorx - 1 - borderOffset];
                    }
                    if (cursorx - borderOffset < mapRow.Length - 1)
                    {
                        nextTileRight = mapRows[cursory - borderOffset][cursorx + 1 - borderOffset];
                    }
                    if (enemyCursory - borderOffset > 0)
                    {
                        enemyNextTileUp = mapRows[enemyCursory - 1 - borderOffset][enemyCursorx - borderOffset];
                    }
                    if (mapRows.Length - 1 > enemyCursory - borderOffset)
                    {
                        enemyNextTileDown = mapRows[enemyCursory + 1 - borderOffset][enemyCursorx - borderOffset];
                    }
                    if (enemyCursorx - borderOffset > 0)
                    {
                        enemyNextTileLeft = mapRows[enemyCursory - borderOffset][enemyCursorx - 1 - borderOffset];
                    }
                    if (enemyCursorx - borderOffset < mapRow.Length - 1)
                    {
                        enemyNextTileRight = mapRows[enemyCursory - borderOffset][enemyCursorx + 1 - borderOffset];
                    }
                }
                Console.Write('|');
                Console.WriteLine();
            }
            Console.Write('+');
            for (int i = 0; i < mapXLength; i++)
            {
                Console.Write('-');
            }
            Console.Write('+');
            Console.WriteLine();
            //Console.WriteLine("Current Tile Of The Player Position: " + currentTile);
            //Console.WriteLine("Next Tile Up From The Player Position: " + nextTileUp);
            //Console.WriteLine("Next Tile Down From The Player Position: " + nextTileDown);
            //Console.WriteLine("Next Tile Left From The Player Position: " + nextTileLeft);
            //Console.WriteLine("Next Tile Right From The Player Position: " + nextTileRight);
            //Console.WriteLine("Cursor X " + cursorx);
            //Console.WriteLine("Cursor Y " + cursory);
            //Console.WriteLine("Enemy Cursor X " + enemyCursorx);
            //Console.WriteLine("Enemy Cursor Y " + enemyCursory);
            //Console.WriteLine("Player Dead: " + playerDead);
            //Console.WriteLine("GameOver " + gameOver);
            //Console.WriteLine("Player Victory " + playerVictory);
            //Console.WriteLine("Current Tile Of The Enemy Position: " + enemyCurrentTile);
            //Console.WriteLine("Next Tile Up From The Enemy Position: " + enemyNextTileUp);
            //Console.WriteLine("Next Tile Down From The Enemy Position: " + enemyNextTileDown);
            //Console.WriteLine("Next Tile Left From The Enemy Position: " + enemyNextTileLeft);
            //Console.WriteLine("Next Tile Right From The Enemy Position: " + enemyNextTileRight);
        }

        static bool CheckForWall(char tile, char wallTile)
        {
            if (tile == wallTile)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        static void EnemyTakeDamage(int damage)
        {
            enemyHealth -= damage;
            if (enemyHealth <= 0)
            {
                enemyDead = true;
                gameOver = true;
                playerVictory = true;
            }
        }

        static void PlayerTakeDamage(int damage)
        {
            playerHealth -= damage;
            if (playerHealth <= 0)
            {
                playerDead = true;
                gameOver = true;
            }
        }

        static void PlayerUpdate()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.W)
            {
                cursory--;
                if (cursory < 1) cursory = 1;
                else if (CheckForWall(nextTileUp, wallTile)) cursory++;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursory++;
                    enemyCursory--;
                    if (CheckForWall(enemyNextTileUp, wallTile) || enemyCursory < 1) enemyCursory++;
                    enemyWasAttacked = true;
                }
            }
            else if (input.Key == ConsoleKey.A)
            {
                cursorx--;
                if (cursorx < 1) cursorx = 1;
                else if (CheckForWall(nextTileLeft, wallTile)) cursorx++;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursorx++;
                    enemyCursorx--;
                    if (CheckForWall(enemyNextTileLeft, wallTile) || enemyCursorx < 1) enemyCursorx++;
                    enemyWasAttacked = true;
                }
            }
            else if (input.Key == ConsoleKey.D)
            {
                cursorx++;
                if (cursorx > 34) cursorx = 34;
                else if (CheckForWall(nextTileRight, wallTile)) cursorx--;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursorx--;
                    enemyCursorx++;
                    if (CheckForWall(enemyNextTileRight, wallTile) || enemyCursorx > 34) enemyCursorx--;
                    enemyWasAttacked = true;
                }
            }
            else if (input.Key == ConsoleKey.S)
            {
                cursory++;
                if (cursory > 16) cursory = 16;
                else if (CheckForWall(nextTileDown, wallTile)) cursory--;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursory--;
                    enemyCursory++;
                    if (CheckForWall(enemyNextTileDown, wallTile) || enemyCursory > 16) enemyCursory--;
                    enemyWasAttacked = true;
                }
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
                        if (enemyCursorx < 1) enemyCursorx = 1;
                        else if (CheckForWall(enemyNextTileLeft, wallTile)) enemyCursorx++;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory)
                        {
                            PlayerTakeDamage(1);
                            enemyCursorx++;
                            cursorx--;
                            if (CheckForWall(nextTileLeft, wallTile) || cursorx < 1)
                            {
                                cursorx++;
                            }
                        }
                        break;
                    case 1:
                        enemyCursory++;
                        if (enemyCursory > 16) enemyCursory = 16;
                        else if (CheckForWall(enemyNextTileDown, wallTile)) enemyCursory--;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory)
                        {
                            PlayerTakeDamage(1);
                            enemyCursory--;
                            cursory++;
                            if (CheckForWall(nextTileDown, wallTile) || cursory > 16)
                            {
                                cursory--;
                            }
                        }
                        break;
                    case 2:
                        enemyCursory--;
                        if (enemyCursory < 1) enemyCursory = 1;
                        else if (CheckForWall(enemyNextTileUp, wallTile)) enemyCursory++;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory)
                        {
                            PlayerTakeDamage(1);
                            enemyCursory++;
                            cursory--;
                            if (CheckForWall(nextTileUp, wallTile) || cursory < 1)
                            {
                                cursory++;
                            }
                        }
                        break;
                    case 3:
                        enemyCursorx++;
                        if (enemyCursorx > 34) enemyCursorx = 34;
                        else if (CheckForWall(enemyNextTileRight, wallTile)) enemyCursorx--;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory)
                        {
                            PlayerTakeDamage(1);
                            enemyCursorx--;
                            cursorx++;
                            if (CheckForWall(nextTileRight, wallTile) || cursorx > 34)
                            {
                                cursorx--;
                            }
                        }
                        break;
                }
            }
        }
        static void PlayerDraw(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine("@");
    }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            RenderMap();
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("Enemy Health: " + enemyHealth);
            while (!gameOver)
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
                    if (!enemyWasAttacked)
                    {
                        EnemyUpdate();
                    }
                    enemyWasAttacked = false;
                }
            RenderMap();
                Console.WriteLine("Player Health: " + playerHealth);
                Console.WriteLine("Enemy Health: " + enemyHealth);
            }
            if (playerVictory)
            {
                Console.Clear();
                Console.WriteLine("Victory");
            }
            if (playerDead)
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
