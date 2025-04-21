using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Domain.Entities;
using System.Drawing;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IMinePlacer, MinePlacer>();
        services.AddSingleton<IGameServiceFactory, GameServiceFactory>();

        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IGameServiceFactory>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Minesweeper!\n");

            int size = ReadInt("Enter the size of the grid (e.g. 4 for a 4x4 grid):\n", 2, 26);
            int maxMines = (int)(size * size * 0.35);
            int mines = ReadInt($"Enter the number of mines to place on the grid (maximum is 35% of the total squares):\n", 1, maxMines);
            
            var game = factory.Create(size, mines);
            bool gameOver = false;

            while (!gameOver)
            {
                ShowBoard(game.GetGrid());

                Console.Write("\nSelect a square to reveal (e.g. A1): ");
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (!TryParseInput(input, out int row, out int col, size))
                {
                    Console.WriteLine("\nInvalid input. Try again.");
                    Console.ReadKey();
                    continue;
                }

                if (!game.Reveal(row, col))
                {
                    Console.Clear();
                    ShowBoard(game.GetGrid());
                    Console.WriteLine("\nOh no, you detonated a mine! Game over.");
                    gameOver = true;
                }
                else if (game.IsGameWon())
                {
                    Console.Clear();
                    ShowBoard(game.GetGrid());
                    Console.WriteLine("\nCongratulations, you have won the game!");
                    gameOver = true;
                }
            }

            Console.WriteLine("Press any key to play again...");
            Console.ReadKey();
        }
    }

    static void ShowBoard(Grid grid)
    {
        Console.Write("\nHere is your updated minefield:\n");
        for (int i = 1; i <= grid.Size; i++)
            Console.Write($" {i}");

        Console.WriteLine();

        for (int r = 0; r < grid.Size; r++)
        {
            Console.Write((char)('A' + r) + " ");
            for (int c = 0; c < grid.Size; c++)
            {
                var cell = grid.Cells[r, c];
                if (!cell.IsRevealed)
                    Console.Write(" _");
                else if (cell.HasMine)
                    Console.Write(" *");
                else
                    Console.Write($" {cell.AdjacentMines}");
            }
            Console.WriteLine();
        }
    }

    static int ReadInt(string prompt, int min, int max)
    {
        int value;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);
        return value;
    }

    static bool TryParseInput(string input, out int row, out int col, int size)
    {
        row = col = -1;
        if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
            return false;

        char rowChar = input[0];
        if (rowChar < 'A' || rowChar >= 'A' + size)
            return false;

        if (!int.TryParse(input[1..], out col))
            return false;

        row = rowChar - 'A';
        col -= 1;

        return col >= 0 && col < size;
    }
}