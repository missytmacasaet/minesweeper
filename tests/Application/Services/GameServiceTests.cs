using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using NSubstitute;

namespace Application.Tests.Services;

public class GameServiceTests
{
    private readonly IMinePlacer _minePlacer;
    private readonly GameServiceFactory _gameServiceFactory;
    public GameServiceTests()
    {
        _minePlacer = Substitute.For<IMinePlacer>();
        _gameServiceFactory = new GameServiceFactory(_minePlacer);
    }

    [Fact]
    public void ShouldRevealNonMineAndFloodIfZeroAdjacent()
    {
        var gridSize = 3;

        _minePlacer.When(m => m.PlaceMines(Arg.Any<Grid>(), Arg.Any<int>()))
                   .Do(x =>
                   {
                       var grid = x.Arg<Grid>(); 
                       grid.Cells[0, 0].HasMine = true;
                   });

        var service = _gameServiceFactory.Create(gridSize, 1);

        var result = service.Reveal(2, 2);

        Assert.True(result);
        Assert.True(service.GetGrid().Cells[2, 2].IsRevealed);
    }

    [Fact]
    public void ShouldLoseGameIfMineHit()
    {
        _minePlacer.When(m => m.PlaceMines(Arg.Any<Grid>(), Arg.Any<int>()))
                  .Do(x =>
                  {
                      var grid = x.Arg<Grid>();
                      grid.Cells[1, 1].HasMine = true;
                  });

        var service = _gameServiceFactory.Create(3, 1);

        var result = service.Reveal(1, 1);

        Assert.False(result);
    }

    [Fact]
    public void ShouldWinGameWhenAllNonMinesAreRevealed()
    {
        var gridSize = 3;
        var minePlacer = Substitute.For<IMinePlacer>();

        minePlacer.When(m => m.PlaceMines(Arg.Any<Grid>(), Arg.Any<int>()))
                  .Do(x =>
                  {
                      var grid = x.Arg<Grid>();
                      grid.Cells[0, 0].HasMine = true;
                      grid.Cells[1, 1].HasMine = true;
                  });

        var service = _gameServiceFactory.Create(gridSize, 2);

        service.Reveal(0, 2);
        service.Reveal(1, 0);
        service.Reveal(1, 2);
        service.Reveal(2, 0);
        service.Reveal(2, 1);

        Assert.True(service.IsGameWon());
    }

    [Fact]
    public void ShouldCalculateAdjacentMinesCorrectly()
    {
        var gridSize = 3;

        _minePlacer.When(m => m.PlaceMines(Arg.Any<Grid>(), Arg.Any<int>()))
                  .Do(x =>
                  {
                      var grid = x.Arg<Grid>();
                      grid.Cells[0, 0].HasMine = true;
                      grid.Cells[1, 1].HasMine = true;
                  });

        var service = _gameServiceFactory.Create(gridSize, 2);

        var grid = service.GetGrid();

        Assert.Equal(2, grid.Cells[0, 1].AdjacentMines);
        Assert.Equal(1, grid.Cells[0, 2].AdjacentMines);
        Assert.Equal(2, grid.Cells[1, 0].AdjacentMines);
        Assert.Equal(1, grid.Cells[1, 2].AdjacentMines);
        Assert.Equal(1, grid.Cells[2, 0].AdjacentMines);
        Assert.Equal(1, grid.Cells[2, 1].AdjacentMines);
        Assert.Equal(1, grid.Cells[2, 2].AdjacentMines);
    }
}