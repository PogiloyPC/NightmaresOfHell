using UnityEngine;
public class DefaultTurret : Turret
{
    [SerializeField] private Point _gunTurret;

    [SerializeField] private StraightBullet _bulletPrefab;        

    protected override void RotateGun()
    {
        Vector3 rotation = _gunTurret.GetPosition() - transform.position;

        rotation.x = 0;
        rotation.z = 0;

        _gunTurret.LookTarget(rotation);
    }

    protected override void Shot()
    {
        StraightBullet bullet = Game.SpawnBullet(_bulletPrefab);
        bullet.InitBullet(ShotPosition, _enemy.GetPosition());
        bullet.InitDamageDiller(this.TurretStats);
    }
}

