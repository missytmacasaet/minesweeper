using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GameService : IGameService
{
    private readonly Grid _grid;
    private readonly int _mineCount;
    private readonly IMinePlacer _minePlacer;

    public GameService(int size, int mineCount, IMinePlacer minePlacer)
    {
        _grid = new Grid(size);
        _mineCount = mineCount;
        _minePlacer = minePlacer;
        _minePlacer.PlaceMines(_grid, mineCount);
        CalculateAdjacentMines();
    }

    public Grid GetGrid() => _grid;

    public bool Reveal(int row, int col)
    {
        var cell = _grid.Cells[row, col];
        if (cell.HasMine)
            return false;

        FloodFill(row, col);
        return true;
    }

    public bool IsGameWon()
    {
        foreach (var cell in _grid.Cells)
        {
            if (!cell.HasMine && !cell.IsRevealed)
                return false;
        }
        return true;
    }

    private void CalculateAdjacentMines()
    {
        int[] dr = { -1, 0, 1 };
        int[] dc = { -1, 0, 1 };

        for (int r = 0; r < _grid.Size; r++)
        {
            for (int c = 0; c < _grid.Size; c++)
            {
                if (_grid.Cells[r, c].HasMine)
                    continue;

                int count = 0;
                foreach (int dx in dr)
                {
                    foreach (int dy in dc)
                    {
                        int nr = r + dx;
                        int nc = c + dy;

                        if ((dx != 0 || dy != 0) && IsValid(nr, nc) && _grid.Cells[nr, nc].HasMine)
                            count++;
                    }
                }

                _grid.Cells[r, c].AdjacentMines = count;
            }
        }
    }

    private void FloodFill(int r, int c)
    {
        if (!IsValid(r, c) || _grid.Cells[r, c].IsRevealed)
            return;

        var cell = _grid.Cells[r, c];
        cell.IsRevealed = true;

        if (cell.AdjacentMines == 0)
        {
            int[] dr = { -1, 0, 1 };
            int[] dc = { -1, 0, 1 };

            foreach (int dx in dr)
            {
                foreach (int dy in dc)
                {
                    if (dx != 0 || dy != 0)
                        FloodFill(r + dx, c + dy);
                }
            }
        }
    }

    private bool IsValid(int r, int c)
    {
        return r >= 0 && r < _grid.Size && c >= 0 && c < _grid.Size;
    }
}