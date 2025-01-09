using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        //static void Main(string[] args)
        //{

            class TicTacToe
        {
            private char[,] board = new char[3, 3];
            private char currentPlayer = 'X';

            public TicTacToe()
            {
                InitializeBoard();
            }

            private void InitializeBoard()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board[i, j] = ' ';
                    }
                }
            }

            public void Play()
            {
                bool gameOver = false;
                while (!gameOver)
                {
                    PrintBoard();
                    int[] move = GetPlayerMove();
                    if (IsValidMove(move[0], move[1]))
                    {
                        MakeMove(move[0], move[1]);
                        if (CheckWin())
                        {
                            PrintBoard();
                            Console.WriteLine($"Player {currentPlayer} wins!");
                            gameOver = true;
                        }
                        else if (CheckDraw())
                        {
                            PrintBoard();
                            Console.WriteLine("It's a draw!");
                            gameOver = true;
                        }
                        else
                        {
                            SwitchPlayer();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Try again.");
                    }
                }
            }

            private int[] GetPlayerMove()
            {
                Console.WriteLine($"Player {currentPlayer}, enter your move (row [0 - 2] and column [0 - 2] separated by a space):");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');
                int row = int.Parse(parts[0]);
                int col = int.Parse(parts[1]);
                return new int[] { row, col };
            }

            private bool IsValidMove(int row, int col)
            {
                return row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ';
            }

            private void MakeMove(int row, int col)
            {
                board[row, col] = currentPlayer;
            }

            private void SwitchPlayer()
            {
                currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
            }

            private void PrintBoard()
            {
                Console.WriteLine("  0 1 2");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"{i} ");
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write($"{board[i, j]} ");
                    }
                    Console.WriteLine();
                }
            }

            private bool CheckWin()
            {
                // Check rows
                for (int i = 0; i < 3; i++)
                {
                    if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    {
                        return true;
                    }
                }

                // Check columns
                for (int j = 0; j < 3; j++)
                {
                    if (board[0, j] == currentPlayer && board[1, j] == currentPlayer && board[2, j] == currentPlayer)
                    {
                        return true;
                    }
                }

                // Check diagonals
                if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                {
                    return true;
                }
                if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                {
                    return true;
                }

                return false;
            }

            private bool CheckDraw()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
            class Progra
            {
                static void Main()
                {
                    TicTacToe game = new TicTacToe();
                    game.Play();
                }

            }

         //}

    
    }
}

