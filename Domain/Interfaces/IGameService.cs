using Domain.Entities;

namespace Domain.Interfaces;

public interface IGameService
{
    Grid GetGrid();
    bool Reveal(int row, int col);
    bool IsGameWon();
}