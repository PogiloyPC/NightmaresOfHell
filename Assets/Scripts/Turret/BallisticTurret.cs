using UnityEngine;
public class BallisticTurret : Turret
{
    [SerializeField] private BallisticBullet _bulletPrefab;

    private float _launchSpeed;

    private void Start()
    {
        float x = DistanceSearchEnemy;
        float y = ShotPosition.y;

        _launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

    protected override void Shot()
    {
        Vector3 launchPoint = ShotPosition;
        Vector3 targetPoint = _enemy.GetPosition();
        targetPoint.y = 0f;

        Vector3 dir = new Vector3();
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.z - launchPoint.z;

        float x = dir.magnitude;
        float y = -launchPoint.y;
        dir /= x;

        float g = 9.81f;
        float s = _launchSpeed;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;

        BallisticBullet bullet = Game.SpawnBullet(_bulletPrefab);
        bullet.InitBullet(launchPoint, targetPoint, new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y));
        bullet.InitDamageDiller(this.TurretStats);
    }
}

