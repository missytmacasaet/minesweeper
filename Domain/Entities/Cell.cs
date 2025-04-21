namespace Domain.Entities;

public class Cell
{
    public bool IsRevealed { get; set; }
    public bool HasMine { get; set; }
    public int AdjacentMines { get; set; }
}