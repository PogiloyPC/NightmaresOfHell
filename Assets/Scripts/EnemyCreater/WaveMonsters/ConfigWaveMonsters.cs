using UnityEngine;
using System;
using EnemyInterface;
using BulletInterface;

public interface IConfigWaveMonsters
{
    Enemy[] GetAllEnemies();
}

[Serializable]
public class ConfigWaveMonsters : IConfigWaveMonsters
{
    [SerializeField] private Enemy[] _enemies;

    public Enemy[] GetAllEnemies() => _enemies;
}

public abstract class SpawnerEnemy : ScriptableObject
{
    [SerializeField] private ConfigWaveMonsters _waveMonsters;

    public IConfigWaveMonsters WaveMonsters => _waveMonsters;

    [SerializeField] private CurrentTimeMinute _timeSpawnWave;
    public CurrentTimeMinute TimeSpawnWave { get { return _timeSpawnWave; } private set { } }

    [SerializeField] private float _timeSpawnEnemy;
    public float TimeSpawnEnemy { get { return _timeSpawnEnemy; } private set { } }

    public abstract void SpawnEnemy(Enemy enemy, Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage, IToward toward, Vector3 launchPosition);
}