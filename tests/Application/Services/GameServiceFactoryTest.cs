using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using NSubstitute;

namespace Application.Tests.Services;

public class GameServiceFactoryTests
{
    private readonly IMinePlacer _minePlacer;
    private readonly GameServiceFactory _gameServiceFactory;

    public GameServiceFactoryTests()
    {
        _minePlacer = Substitute.For<IMinePlacer>();
        _gameServiceFactory = new GameServiceFactory(_minePlacer);
    }

    [Fact]
    public void ShouldCreateGameServiceWithCorrectDependencies()
    {
        int size = 4;
        int mineCount = 5;

        var gameService = _gameServiceFactory.Create(size, mineCount);

        Assert.NotNull(gameService);
        var grid = gameService.GetGrid();
        Assert.Equal(size, grid.Size);

        _minePlacer.Received(1).PlaceMines(Arg.Any<Grid>(), mineCount);
    }
}