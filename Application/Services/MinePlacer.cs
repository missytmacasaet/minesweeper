using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class MinePlacer : IMinePlacer
{
    private readonly Random _random = new();

    public void PlaceMines(Grid grid, int mineCount)
    {
        int size = grid.Size;
        int placed = 0;

        while (placed < mineCount)
        {
            int row = _random.Next(size);
            int col = _random.Next(size);

            var cell = grid.Cells[row, col];
            if (!cell.HasMine)
            {
                cell.HasMine = true;
                placed++;
            }
        }
    }
}