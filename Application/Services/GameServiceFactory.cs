using Domain.Interfaces;

namespace Application.Services;

public class GameServiceFactory : IGameServiceFactory
{
    private readonly IMinePlacer _minePlacer;

    public GameServiceFactory(IMinePlacer minePlacer)
    {
        _minePlacer = minePlacer;
    }

    public IGameService Create(int size, int mines)
    {
        return new GameService(size, mines, _minePlacer);
    }
}