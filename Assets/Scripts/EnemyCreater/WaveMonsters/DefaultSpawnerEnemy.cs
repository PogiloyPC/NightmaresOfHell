using BulletInterface;
using EnemyInterface;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Spawner")]
public class DefaultSpawnerEnemy : SpawnerEnemy
{
    public override void SpawnEnemy(Enemy enemy, Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage, IToward toward, Vector3 launchPosition)
    {
        Game.SpawnEnemy<Enemy>(enemy).InitEnemy(onTakeDamage, toward, launchPosition);
    }
}