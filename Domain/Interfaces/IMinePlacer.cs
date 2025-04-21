using Domain.Entities;

namespace Domain.Interfaces;

public interface IMinePlacer
{
    void PlaceMines(Grid grid, int mineCount);
}