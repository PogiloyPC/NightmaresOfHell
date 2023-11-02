using EnemyInterface;
using System;
using System.Collections.Generic;

public class FactoryEnemy : Factory<Enemy>, IContaineEnemy
{
    private Action<IRewardMoneyEnemy> _onKillEnemy;

    public FactoryEnemy(Action<IRewardMoneyEnemy> onKillEnemy)
    {
        _onKillEnemy = onKillEnemy;
    }

    public void GetRandomEnemy(out Enemy enemy)
    {
        if (_objectsFactory.Count <= 0f)
        {
            enemy = null;

            return;
        }

        int random = UnityEngine.Random.Range(0, _objectsFactory.Count - 1);

        enemy = _objectsFactory[random];
    }

    protected override void DoingAction(Enemy enemy) => _onKillEnemy.Invoke(enemy);

}
