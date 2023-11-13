using UnityEngine.UI;

public class PanelUpgradeTurret : PanelGame, ISeterPrice, IPanelTurretUpgrade
{
    private IUpgradableTurret _turret;

    private PriceUI _priceUpgrade;

    private ViwerTurret _currentLevelImage;
    private ViwerTurret _nextLevelImage;

    private int _priceTurretUpgrade;

    public void InitPanel(PriceUI priceUpgrade, ViwerTurret currentLevelImage, ViwerTurret nextLevelImage)
    {
        _priceUpgrade = priceUpgrade;

        _currentLevelImage = currentLevelImage;
        _nextLevelImage = nextLevelImage;
    }

    public void SetTurretForUpgrade(IUpgradableTurret turret, IUpgraderTurret upgraderTurret)
    {
        if (turret.GetCurrentLevelTurret() == LevelTurret.maxLevel)
        {
            gameObject.SetActive(false);

            return;
        }

        _turret = turret;

        _priceTurretUpgrade = upgraderTurret.GetPrice();

        _priceUpgrade.InitPrice(this);

        _currentLevelImage.InitImage(upgraderTurret.GetImageTurret(turret.GetCurrentLevelTurret()));
        _nextLevelImage.InitImage(upgraderTurret.GetImageTurret(turret.GetCurrentLevelTurret() + 1));
    }

    public int GetPrice()
    {
        if (_turret != null)
            return _priceTurretUpgrade;
        else
            return int.MaxValue;
    }
}

