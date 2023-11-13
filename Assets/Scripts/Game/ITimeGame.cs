public interface ITimeGame
{
    float GetCurrentTimeGame();
}

public interface IGameContent
{
    TypeGameContent GetTypeGameContent();
}

public enum TypeGameContent
{
    placeForTurret,
    turret,
    bullet,
    enemy,
    reward
}