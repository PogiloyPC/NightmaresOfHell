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

public enum CurrentTimeMinute
{
    firstMinute = 0,
    secondMinute = 1,
    thirdMinute = 2,
    fourthMinute = 3,
    fifthMinute = 4,
    sixthMinute = 5,
    seventhMinute = 6,
    eighthMinute = 7,
    ninethMinute = 8,
    tenthMinute = 9
}