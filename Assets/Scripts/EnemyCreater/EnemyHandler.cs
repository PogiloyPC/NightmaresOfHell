using UnityEngine;
using System;
using System.Collections;
using BulletInterface;
using EnemyInterface;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private SpawnerEnemy[] _spawnerEnemies;

    [SerializeField] private FieldSpawnEnemy[] _fieldSpawn;

    private Action<float, IDamageDillerEnemy, IEnemy> _onTakeDamage;

    private IToward _toward;

    private ITimeGame _timeGame;

    private float _maxTimeGame = (float)(CurrentTimeMinute.tenthMinute + 1) * 60f;

    public void InitCreater(IToward toward, Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage,
        ITimeGame timeGame)
    {
        _onTakeDamage = onTakeDamage;

        _toward = toward;

        _timeGame = timeGame;

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        SpawnerEnemy currentSpawner = null;

        while (_timeGame.GetCurrentTimeGame() <= _maxTimeGame)
        {
            for (int y = 0; y < _spawnerEnemies.Length; y++)
            {
                if (Game.CurrentTimeGame == _spawnerEnemies[y].TimeSpawnWave)
                {
                    currentSpawner = _spawnerEnemies[y];

                    IConfigWaveMonsters configEnemies = currentSpawner.WaveMonsters;

                    for (int t = 0; t < configEnemies.GetAllEnemies().Length; t++)
                    {
                        for (int i = 0; i < _fieldSpawn.Length; i++)
                        {
                            float x = UnityEngine.Random.Range(_fieldSpawn[i].PositionField().x - (_fieldSpawn[i].SizeField.x / 2),
                            _fieldSpawn[i].PositionField().x + (_fieldSpawn[i].SizeField.x / 2));
                            float z = UnityEngine.Random.Range(_fieldSpawn[i].PositionField().z - (_fieldSpawn[i].SizeField.y / 2),
                                _fieldSpawn[i].PositionField().z + (_fieldSpawn[i].SizeField.y / 2));

                            Vector3 launchPosition = new Vector3(x, transform.position.y, z);

                            currentSpawner.SpawnEnemy(configEnemies.GetAllEnemies()[t], _onTakeDamage, _toward, launchPosition);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(currentSpawner.TimeSpawnEnemy);
        }
    }
}

