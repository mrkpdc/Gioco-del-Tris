using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gioco_del_Tris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * scrivere un programma che simuli il gioco del tris:
             * - 2 giocatori
             * - box di 3x3, simboli O, X
             * - Vince il giocatore che fa tris
             */

            /*
             * altre funzionalità implementabili:
             * - punteggio con vittoria al meglio delle 3
             * - vuoi giocare ancora? dopo aver concluso un set
             */

            int choice = 0, turn = 1, turnCount = 0;
            bool winFlag = false, correctInput = false, drawFlag = false, freePosition = false;

            // Game intro
            Console.WriteLine("Welcome to TIC TAC TOE!");
            Console.WriteLine();
            Console.WriteLine("Player 1, insert your name:");
            string player1 = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Player 2, insert your name:");
            string player2 = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"Okay, {player1} is \"X\" and {player2} is \"O\".");
            Console.WriteLine();
            Console.WriteLine($"{player1} will go first, press any key to continue.");
            Console.ReadKey();
            Console.Clear();

            // Game loop
            bool playing = true;
            while (playing == true)
            {
                while (winFlag == false && drawFlag == false) // Execute until no one wins or draw occurs
                {
                    turnCount++;

                    UpdateBoardAndClearConsole();

                    if (turn == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"It's {player1}'s turn (X).");
                    }
                    if (turn == 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"It's {player2}'s turn (O).");
                    }

                    // Check user's input
                    while (correctInput == false || freePosition == false)
                    {
                        // Check user's input (must be position 1 - 9)
                        Console.WriteLine();
                        Console.WriteLine("Which position would you like to take (1-9)?");
                        bool successfulParse = int.TryParse(Console.ReadLine(), out choice);
                        if ((choice > 0 && choice < 10) && successfulParse == true)
                        {
                            correctInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Positions must be between 1 and 9, press any key to try again.");
                            Console.ReadKey();
                            UpdateBoardAndClearConsole();
                            continue;
                        }

                        // Check user's input (must be a free position)
                        if (pos[choice] == choice.ToString())
                        {
                            if (turn == 1)
                            {
                                pos[choice] = "X";
                            }
                            else if (turn == 2)
                            {
                                pos[choice] = "O";
                            }
                            UpdateBoardAndClearConsole();
                            Console.WriteLine();
                            //Console.WriteLine($"You chose position {choice}! Press any key to continue with the next player.");
                            //Console.ReadKey();
                            freePosition = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("The selected position is already taken! Press any key to try again.");
                            Console.ReadKey();
                            UpdateBoardAndClearConsole();
                            continue;
                        }
                    }
                    // Reset of correctInput and freePosition flags
                    correctInput = false;
                    freePosition = false;

                    winFlag = CheckWin();

                    // if draw occurs, drawFlag is true
                    if (winFlag == false && turnCount == 9)
                    {
                        drawFlag = true;
                    }

                    // if no one wins, switch turn
                    if (winFlag == false)
                    {
                        if (turn == 1)
                        {
                            turn = 2;
                        }
                        else if (turn == 2)
                        {
                            turn = 1;
                        }
                    }
                }

                ClearBoard();

                UpdateBoardAndClearConsole();

                if (drawFlag == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Draw!");
                    playing = false;
                }
                else
                {
                    if (turn == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{player1} won!");
                        playing = false;
                    }
                    if (turn == 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{player2} won!");
                        playing = false;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Thanks for playing and see you soon!");

            Console.ReadKey();
        }


        // Positions array, position 0 is not used
        static string[] pos = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        // Board drawing method
        static void DrawBoard()
        {
            Console.WriteLine(" {0} | {1} | {2} ", pos[1], pos[2], pos[3]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2} ", pos[4], pos[5], pos[6]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2} ", pos[7], pos[8], pos[9]);
        }

        // Board cleaning method
        static void ClearBoard()
        {
            for (int i = 1; i < 10; i++)
                pos[i] = " ";
        }

        // Win checking method
        static bool CheckWin()
        {
            for (int i = 1; i < 10; i = i + 3) // Horizontal check
            {
                if ((pos[i] == "X" && pos[i + 1] == "X" && pos[i + 2] == "X") ||
                    (pos[i] == "O" && pos[i + 1] == "O" && pos[i + 2] == "O"))
                {
                    return true;
                }
            }

            for (int i = 1; i < 4; i++) // Vertical check
            {
                if ((pos[i] == "X" && pos[i + 3] == "X" && pos[i + 6] == "X") ||
                    (pos[i] == "O" && pos[i + 3] == "O" && pos[i + 6] == "O"))
                {
                    return true;
                }
            }

            if ((pos[1] == "X" && pos[5] == "X" && pos[9] == "X") || // Checks 1, 5, 9 diagonal win
                (pos[1] == "O" && pos[5] == "O" && pos[9] == "O") ||

                (pos[3] == "X" && pos[5] == "X" && pos[7] == "X") || // Checks 3, 5, 7 diagonal win
                (pos[3] == "O" && pos[5] == "O" && pos[7] == "O")
                )
            {
                return true;
            }

            return false;
        }

        static void UpdateBoardAndClearConsole()
        {
            Console.Clear();
            DrawBoard();
        }
    }
}
