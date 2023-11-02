using UnityEngine;
public class DefaultTurret : Turret
{
    [SerializeField] private StraightBullet _bulletPrefab;        

    protected override void Shot()
    {
        StraightBullet bullet = Game.SpawnBullet(_bulletPrefab);
        bullet.InitBullet(ShotPosition, _enemy.GetPosition());
        bullet.InitDamageDiller(this);
    }
}

