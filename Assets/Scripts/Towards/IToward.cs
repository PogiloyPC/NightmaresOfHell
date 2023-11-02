using EnemyInterface;
using UnityEngine;
public interface IToward
{
    Vector3 GetPosition();

    int GetLevel();
}

public interface ILevelToward
{
    bool GetMaxLevel();

    void LevelUp();
}

public interface IHealthToward
{
    void TakeDamage(IDamageDiller damageDiller);
}

public interface IPropertyHealthToward
{
    float GetCurrentHealth();

    float GetStartHealth();
}

public interface IContaineHealthToward
{
    void TakeDamageDiller(IDamageDiller damageDiller);
}