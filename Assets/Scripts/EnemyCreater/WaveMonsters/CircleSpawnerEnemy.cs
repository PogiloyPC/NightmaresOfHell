using BulletInterface;
using EnemyInterface;
using System;
using UnityEngine;

public class CircleSpawnerEnemy : SpawnerEnemy
{
    public override void SpawnEnemy(Enemy enemy, Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage, IToward toward, Vector3 launchPosition)
    {
        
    }
}
