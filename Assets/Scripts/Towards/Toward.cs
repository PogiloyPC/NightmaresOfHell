using EnemyInterface;
using UnityEngine;
using System;

public class Toward : MonoBehaviour, IToward, IHealthToward, IPropertyHealthToward, ILevelToward
{
    [SerializeField] private PartToward[] _partsToward;

    [SerializeField] private CircleWallToward[] _circleWallsToward;

    private Action<IPropertyHealthToward> _onDisplayHealth;

    [SerializeField] private float _hightToward;

    [SerializeField] private float _startHealth;
    private float _currentHealth;

    [SerializeField] private int _maxLevelToward;
    public int Level { get; private set; } = 1;

    public Vector3 GetPosition() => transform.position;

    public int GetLevel() => Level;

    public void InitToward(Action<IPropertyHealthToward> onDisplayHealth)
    {
        _onDisplayHealth = onDisplayHealth;

        _currentHealth = _startHealth;

        foreach (CircleWallToward wallToward in _circleWallsToward)
            wallToward.InitWall(this);

        foreach (PartToward part in _partsToward)
            part.InitPartToward(this);
    }

    public void TakeDamage(IDamageDiller damageDiller)
    {
        _currentHealth -= damageDiller.GetDamage();

        _onDisplayHealth?.Invoke(this);
    }

    public float GetCurrentHealth() => _currentHealth;

    public float GetStartHealth() => _startHealth;

    public bool GetMaxLevel()
    {
        if (Level < _maxLevelToward)
            return false;
        else
            return true;
    }

    public void LevelUp()
    {
        Level++;

        foreach (CircleWallToward wallToward in _circleWallsToward)
            wallToward.SetEnabled(this);
    }
}
