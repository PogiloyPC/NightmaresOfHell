using EnemyInterface;
using UnityEngine;

public class ExplosiveBullet : BallisticBullet
{
    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] private float _radiusExplosion;

    [SerializeField] private Explosion _prefabExplosion;

    protected override void GiveDamage(IEnemy enemy)
    {        
        Explose();
    }

    protected override void CollisionGround()
    {
        Explose();
    }

    private void Explose()
    {
        Game.SpawnEffect(_prefabExplosion).InitExplosion(this, transform.position, _radiusExplosion, _enemyMask);

        _factoryBullet.Reclaim(this);
    }
}
