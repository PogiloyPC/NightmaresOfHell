using UnityEngine;

[System.Serializable]
public class ConfigTurretStats
{
    [SerializeField] private NameTurret _nameTurretStatsConfig;

    [SerializeField] private TurretStats _firstLevelStats, _secondLevelStats, _thirdLevelTurret, _fourthLevelTurret, _fifthLevelTurret;

    public NameTurret GetNameTurretStatsConfig() => _nameTurretStatsConfig;

    public TurretStats GetLaunchStatsTurret() => _firstLevelStats;

    public TurretStats GetStatsForUpgrade(LevelTurret nextLevel)
    {
        switch (nextLevel + 1)
        {            
            case (LevelTurret.secondLevel):
                return _secondLevelStats;                
            case (LevelTurret.thirdLevel):
                return _thirdLevelTurret;
            case (LevelTurret.fourthLevel):
                return _fourthLevelTurret;
            case (LevelTurret.maxLevel):
                return _fifthLevelTurret;
            default:
                return null;
        }        
    }
}

