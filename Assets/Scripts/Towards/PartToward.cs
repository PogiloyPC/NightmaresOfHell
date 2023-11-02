using EnemyInterface;
using UnityEngine;

public class PartToward : MonoBehaviour, IContaineHealthToward
{
    private IHealthToward _healthToward;

    public void InitPartToward(IHealthToward healthToward)
    {
        _healthToward = healthToward;
    }

    public void TakeDamageDiller(IDamageDiller damageDiller)
    {
        _healthToward.TakeDamage(damageDiller);
    }
}
