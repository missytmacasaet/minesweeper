using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Grid
{
    public int Size { get; }
    public Cell[,] Cells { get; }

    public Grid(int size)
    {
        Size = size;
        Cells = new Cell[size, size];
        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                Cells[row, col] = new Cell();
            }
        }
    }
}