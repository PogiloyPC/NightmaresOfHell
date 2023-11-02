using UnityEngine;
using System;
using System.Collections;
using BulletInterface;
using EnemyInterface;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveMonsters _waveMonsters;

    [SerializeField] private FieldSpawnEnemy[] _fieldSpawn;

    private Action<float, IDamageDillerEnemy, IEnemy> _onTakeDamage;

    private IToward _toward;

    private ITimeGame _timeGame;

    [SerializeField] private float _speedSpawn;
    private float _progresSpawn;

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
        while (_timeGame.GetCurrentTimeGame() <= 180f)
        {
            for (int i = 0; i < _fieldSpawn.Length; i++)
            {
                if (_timeGame.GetCurrentTimeGame() >= 0f)
                {
                    float x = UnityEngine.Random.Range(_fieldSpawn[i].PositionField().x - (_fieldSpawn[i].SizeField.x / 2),
                        _fieldSpawn[i].PositionField().x + (_fieldSpawn[i].SizeField.x / 2));
                    float z = UnityEngine.Random.Range(_fieldSpawn[i].PositionField().z - (_fieldSpawn[i].SizeField.y / 2),
                        _fieldSpawn[i].PositionField().z + (_fieldSpawn[i].SizeField.y / 2));

                    Vector3 launchPosition = new Vector3(x, transform.position.y, z);

                    Game.SpawnEnemy(_waveMonsters.EasyEnemy).InitEnemy(_onTakeDamage, _toward, launchPosition);
                }
                if (_timeGame.GetCurrentTimeGame() >= 60f)
                {
                    float x = UnityEngine.Random.Range(_fieldSpawn[i].PositionField().x - (_fieldSpawn[i].SizeField.x / 2),
                        _fieldSpawn[i].PositionField().x + (_fieldSpawn[i].SizeField.x / 2));
                    float z = UnityEngine.Random.Range(_fieldSpawn[i].PositionField().z - (_fieldSpawn[i].SizeField.y / 2),
                        _fieldSpawn[i].PositionField().z + (_fieldSpawn[i].SizeField.y / 2));

                    Vector3 launchPosition = new Vector3(x, transform.position.y, z);

                    Game.SpawnEnemy(_waveMonsters.PreMidEnemy).InitEnemy(_onTakeDamage, _toward, launchPosition);
                }
            }

            yield return new WaitForSeconds(_speedSpawn);
        }
    }
}

