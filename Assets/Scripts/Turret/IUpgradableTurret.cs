using System;
using UnityEngine;

public interface IUpgradableTurret : ILevelTurret
{
    Vector3 GetPosition();

    void ReloadTurret();

    NameTurret GetNameTurret();
}

public interface ILevelTurret
{
    LevelTurret GetCurrentLevelTurret();
}

[Serializable]
public struct BoxImageTurret
{
    [SerializeField] private Sprite _firstLevelImageTurret;
    [SerializeField] private Sprite _secondLevelImageTurret;
    [SerializeField] private Sprite _thirdLevelImageTurret;
    [SerializeField] private Sprite _fourthLevelImageTurret;
    [SerializeField] private Sprite _fifthLevelImageTurret;

    public Sprite GetImageTurret(LevelTurret levelTurret)
    {
        switch (levelTurret)
        {
            case (LevelTurret.firstLevel):
                return _firstLevelImageTurret;
            case (LevelTurret.secondLevel):
                return _secondLevelImageTurret;
            case (LevelTurret.thirdLevel):
                return _thirdLevelImageTurret;
            case (LevelTurret.fourthLevel):
                return _fourthLevelImageTurret;
            case (LevelTurret.maxLevel):
                return _fifthLevelImageTurret;
            default:
                return null;
        }
    }
}

public enum NameTurret
{
    DrimTurret,
    Mortar,
    ChainTurret,
}

public enum LevelTurret
{
    firstLevel = 0,
    secondLevel = 1,
    thirdLevel = 2,
    fourthLevel = 3,
    maxLevel = 4
}

[Serializable]
public struct BoxPriceTurret
{
    [SerializeField] private int _firstLevelPrice;
    [SerializeField] private int _secondLevelPrice;
    [SerializeField] private int _thirdLevelPrice;
    [SerializeField] private int _fourthLevelPrice;

    public int GetCurrentPrice(IUpgradableTurret turret)
    {
        switch (turret.GetCurrentLevelTurret())
        {
            case (LevelTurret.firstLevel):
                return _firstLevelPrice;
            case (LevelTurret.secondLevel):
                return _secondLevelPrice;
            case (LevelTurret.thirdLevel):
                return _thirdLevelPrice;
            case (LevelTurret.fourthLevel):
                return _fourthLevelPrice;
            default:
                return 0;
        }
    }
}