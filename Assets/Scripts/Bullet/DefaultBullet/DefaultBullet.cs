using EnemyInterface;
using UnityEngine;

public class DefaultBullet : StraightBullet
{
    protected override void GiveDamage(IEnemy enemy)
    {
        enemy.GetDamage(this);

        _factoryBullet.Reclaim(this);
    }
}
