using UnityEngine;

[CreateAssetMenu(fileName = "Upgrader Turret")]
public class UpgraderTurret : ContainerAllLevelTurret, IUpgraderTurret, ILounchPosition, IReseterTurret
{
    private IUpgradableTurret _turret;

    private IPanelTurretUpgrade _panelUpgrade;

    [SerializeField] private NameTurret _nameTurretUpgrader;

    [SerializeField] private BoxImageTurret _imagesTurret;

    [SerializeField] private BoxPriceTurret _pricesTurret;

    public void InitUpgrader(IPanelTurretUpgrade panelUpgrade) => _panelUpgrade = panelUpgrade;

    public NameTurret GetTurretNameUpgrader() => _nameTurretUpgrader;

    public void SetTurretForUpgrade(IUpgradableTurret turret) => _turret = turret;

    public void Upgrade()
    {
        switch (_turret.GetCurrentLevelTurret())
        {
            case (LevelTurret.firstLevel):
                ReloadTurret(FirstLevelUpTurret);
                break;
            case (LevelTurret.secondLevel):
                ReloadTurret(SecondLevelUpTurret);
                break;
            case (LevelTurret.thirdLevel):
                ReloadTurret(ThirdLevelUpTurret);
                break;
            case (LevelTurret.fourthLevel):
                ReloadTurret(FourthLevelUpTurret);
                break;
            case (LevelTurret.maxLevel):
                break;
            default:
                break;
        }
    }

    private void ReloadTurret(Turret turret)
    {
        ConfigTurretStats config = StatsConfigData.GetConfigStats(_nameTurretUpgrader);

        TurretStats turretStats = config.GetStatsForUpgrade(_turret.GetCurrentLevelTurret());

        if (turretStats == null)
            return;

        Turret newTurret = Game.SpawnTurret(turret);
        newTurret.InitTurret(this);
        newTurret.InitTurretStats(turretStats);

        _turret.ReloadTurret();

        _turret = newTurret;

        _panelUpgrade.SetTurretForUpgrade(_turret, this);
    }

    public Sprite GetImageTurret(LevelTurret levelTurret) => _imagesTurret.GetImageTurret(levelTurret);

    public void ResetTurret() => _turret = null;

    public int GetPrice()
    {
        if (_turret != null)
            return _pricesTurret.GetCurrentPrice(_turret);
        else
            return int.MaxValue;
    }

    public Vector3 GetLounchPosition()
    {
        if (_turret != null)
            return _turret.GetPosition();
        else
            return new Vector3();
    }
}
