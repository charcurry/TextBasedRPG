﻿using System;
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
        static char enemyCurrentTile;
        static char enemyNextTileUp;
        static char enemyNextTileDown;
        static char enemyNextTileLeft;
        static char enemyNextTileRight;
        static bool gameOver = false;
        static bool playerVictory = false;
        static int enemyHealth = 4;
        static int playerHealth = 4;
        static int mapX;
        static int offset = 1;
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

            Console.Write('+');
        for (int i = 0; i < mapX; i++)
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
                    mapX = mapRow.Length;
                char tile = mapRow[x];
                Console.Write(tile);
                    if (cursorx - offset < mapRow.Length && cursorx > 0 && cursory - offset < mapRows.Length && cursory > 0)
                    {
                        currentTile = mapRows[cursory - offset][cursorx - offset];
                    }
                    if (cursory - offset > 0)
                    {
                        nextTileUp = mapRows[cursory - 1 - offset][cursorx - offset];
                    }
                    if (mapRows.Length - 1 > cursory - offset)
                    {
                        nextTileDown = mapRows[cursory + 1 - offset][cursorx - offset];
                    }
                    if (cursorx - offset > 0)
                    {
                        nextTileLeft = mapRows[cursory - offset][cursorx - 1 - offset];
                    }
                    if (cursorx - offset < mapRow.Length - 1)
                    {
                        nextTileRight = mapRows[cursory - offset][cursorx + 1 - offset];
                    }
                    //if (enemyCursorx < mapRow.Length && enemyCursorx > 0 && enemyCursory < mapRows.Length && enemyCursory > 0)
                    //{
                    //    enemyCurrentTile = mapRows[enemyCursory - offset][enemyCursorx - offset];
                    //}
                    if (enemyCursory - offset > 0)
                    {
                        enemyNextTileUp = mapRows[enemyCursory - 1 - offset][enemyCursorx - offset];
                    }
                    if (mapRows.Length - 1 > enemyCursory - offset)
                    {
                        enemyNextTileDown = mapRows[enemyCursory + 1 - offset][enemyCursorx - offset];
                    }
                    if (enemyCursorx - offset > 0)
                    {
                        enemyNextTileLeft = mapRows[enemyCursory - offset][enemyCursorx - 1 - offset];
                    }
                    if (enemyCursorx - offset < mapRow.Length - 1)
                    {
                        enemyNextTileRight = mapRows[enemyCursory - offset][enemyCursorx + 1 - offset];
                    }
                }
                Console.Write('|');
                Console.WriteLine();
        }
            Console.Write('+');
            for (int i = 0; i < mapX; i++)
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
            Console.WriteLine("Enemy Cursor X " + enemyCursorx);
            Console.WriteLine("Enemy Cursor Y " + enemyCursory);
            //Console.WriteLine("Player Dead: " + playerDead);
            //Console.WriteLine("GameOver " + gameOver);
            //Console.WriteLine("Player Victory " + playerVictory);
            //Console.WriteLine("Current Tile Of The Enemy Position: " + enemyCurrentTile);
            Console.WriteLine("Next Tile Up From The Enemy Position: " + enemyNextTileUp);
            Console.WriteLine("Next Tile Down From The Enemy Position: " + enemyNextTileDown);
            Console.WriteLine("Next Tile Left From The Enemy Position: " + enemyNextTileLeft);
            Console.WriteLine("Next Tile Right From The Enemy Position: " + enemyNextTileRight);
        }

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
                else if (nextTileUp == '^') cursory++;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursory++;
                    enemyCursory--;
                    if (enemyNextTileUp == '^') enemyCursory++;
                }
            }
        else if (input.Key == ConsoleKey.A)
        {
            cursorx--;
                if (cursorx < 1) cursorx = 1;
                else if (nextTileLeft == '^') cursorx++;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursorx++;
                    enemyCursorx--;
                    if (enemyNextTileLeft == '^') enemyCursorx++;
                }
            }
        else if (input.Key == ConsoleKey.D)
        {
            cursorx++;
                if (cursorx > 34) cursorx = 34;
                else if (nextTileRight == '^') cursorx--;
                else if (enemyCursorx == cursorx && enemyCursory == cursory)
                {
                    EnemyTakeDamage(1);
                    cursorx--;
                    enemyCursorx++;
                    if (enemyNextTileRight == '^') enemyCursorx--;
                }
            }
        else if (input.Key == ConsoleKey.S)
        {
            cursory++;
                if (cursory > 16) cursory = 16;
                else if (nextTileDown == '^') cursory--;
                else if (enemyCursorx == cursorx && enemyCursory == cursory) 
                {
                    EnemyTakeDamage(1);
                    cursory--;
                    enemyCursory++;
                    if (enemyNextTileDown == '^') enemyCursory--;
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
                        else if (enemyNextTileLeft == '^') enemyCursorx++;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) 
                        { 
                            PlayerTakeDamage(1); 
                            enemyCursorx++;
                            cursorx--;
                            if (nextTileLeft == '^') 
                            {
                                cursorx++; 
                            }
                        }
                        break;
                    case 1:
                        enemyCursory++;
                        if (enemyCursory > 16) enemyCursory = 16;
                        else if (enemyNextTileDown == '^') enemyCursory--;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) 
                        { 
                            PlayerTakeDamage(1); 
                            enemyCursory--; 
                            cursory ++;
                            if (nextTileDown == '^')
                            {
                                cursory--;
                            }
                        }
                        break;
                    case 2:
                        enemyCursory--;
                        if (enemyCursory < 1) enemyCursory = 1;
                        else if (enemyNextTileUp == '^') enemyCursory++;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) 
                        { 
                            PlayerTakeDamage(1); 
                            enemyCursory++; 
                            cursory --;
                            if (nextTileUp == '^')
                            {
                                cursory++;
                            }
                        }
                        break;
                    case 3:
                        enemyCursorx++;
                        if (enemyCursorx > 34) enemyCursorx = 34;
                        else if (enemyNextTileRight == '^') enemyCursorx--;
                        else if (enemyCursorx == cursorx && enemyCursory == cursory) 
                        { 
                            PlayerTakeDamage(1); 
                            enemyCursorx--; 
                            cursorx ++;
                            if (nextTileRight == '^')
                            {
                                cursorx--;
                            }
                        }
                        break;
                }
            }
        }

        static void Main(string[] args)
    {
            //Console.WriteLine("MiniGame");
            //Console.WriteLine();
            Console.CursorVisible = false;
            RenderMap();
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
                EnemyUpdate();
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
