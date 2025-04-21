namespace Domain.Interfaces;

public interface IGameServiceFactory
{
    IGameService Create(int size, int mines);
}