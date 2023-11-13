using UnityEngine;

public class BuilderTurret<T> : IBuilderTurret<T> where T : Turret
{
    private PlaceForTurret _placeForTurret;

    private EffectBuild _particleBuild;

    public BuilderTurret(EffectBuild particleBuild)
    {
        _particleBuild = particleBuild;
    }

    public void SetPlaceForTurret(PlaceForTurret place)
    {
        _placeForTurret = place;
    }

    public bool BuildTurret(T turretShop)
    {
        if (_placeForTurret == null)
            return false;

        ConfigTurretStats config = StatsConfigData.GetConfigStats(turretShop.GetNameTurret());

        TurretStats turretStats = config.GetLaunchStatsTurret();

        Turret turret = Game.SpawnTurret(turretShop);
        turret.InitTurret(this);
        turret.InitTurretStats(turretStats);

        EffectBuild effect = Game.SpawnEffect(_particleBuild);
        effect.PlayBuild(this);

        _placeForTurret.SetTurret();
        _placeForTurret = null;

        return true;
    }

    public Vector3 GetLounchPosition()
    {
        if (_placeForTurret)
            return _placeForTurret.GetPositionForTurret();
        else
            return new Vector3();
    }
}

