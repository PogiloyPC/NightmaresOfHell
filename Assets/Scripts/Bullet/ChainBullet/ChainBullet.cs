using EnemyInterface;
using UnityEngine;

public class ChainBullet : StraightBullet
{
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private float _radiusSearchEnemy;

    private int _strenghtBullet = 3;

    protected override void GiveDamage(IEnemy enemy)
    {
        enemy.TakeDamage(this);

        _strenghtBullet--;

        if (_strenghtBullet <= 0)
            _factoryBullet.Reclaim(this);

        if (enemy.GetNextEnemy())
            _enemyTarget = enemy.GetNextEnemy().GetPosition() - transform.position;
    }    
}
