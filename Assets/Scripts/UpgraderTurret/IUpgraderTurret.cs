using UnityEngine;

public interface IUpgraderTurret : IPurchasable, IReseterTurret
{
    NameTurret GetTurretNameUpgrader();

    void SetTurretForUpgrade(IUpgradableTurret turret);

    void Upgrade();

    Sprite GetImageTurret(LevelTurret turret);
}

public interface IReseterTurret
{
    void ResetTurret();
}