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

        Game.SpawnTurret(turretShop).InitTurret(this);       

        EffectBuild effect = Game.SpawnEffect(_particleBuild);

        effect.PlayBuild(this);

        _placeForTurret = null;

        return true;
    }

    public Vector3 GetCurrentPositionBuild()
    {
        if (_placeForTurret)
            return _placeForTurret.GetPositionForTurret();
        else
            return new Vector3();
    }
}

