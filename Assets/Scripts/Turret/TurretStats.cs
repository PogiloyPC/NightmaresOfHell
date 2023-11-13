using BulletInterface;
using UnityEngine;

[System.Serializable]
public class TurretStats : ILoaderBullet
{
    [SerializeField] private TypeDamageBullet _typeDamageBullet;

    [SerializeField] private LevelTurret _levelTurret;
    public LevelTurret Level { get { return _levelTurret; } private set { } }

    [SerializeField] private float _damage;
    [SerializeField] private float _shotsPerMinute;
    public float ShotsPerMinute { get { return _shotsPerMinute; } private set { } }

    public float GetLaunchDamage() => _damage;

    public TypeDamageBullet GetLaunchTypeDamage() => _typeDamageBullet;
}

