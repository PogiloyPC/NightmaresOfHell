using UnityEngine;

[CreateAssetMenu(fileName = "Wave Monsters")]
public class WaveMonsters : ScriptableObject
{
    [SerializeField] private Enemy _easyEnemy;
    [SerializeField] private Enemy _preMidEnemy;
    [SerializeField] private Enemy _midEnemy;
    [SerializeField] private Enemy _preHardEnemy;
    [SerializeField] private Enemy _hardEnemy;

    public Enemy EasyEnemy { get { return _easyEnemy; } private set { } }
    public Enemy PreMidEnemy { get { return _preMidEnemy; } private set { } }
    public Enemy MidEnemy { get { return _midEnemy; } private set { } }
    public Enemy PreHardEnemy { get { return _preHardEnemy; } private set { } }
    public Enemy HardEnemy { get { return _hardEnemy; } private set { } }
}

