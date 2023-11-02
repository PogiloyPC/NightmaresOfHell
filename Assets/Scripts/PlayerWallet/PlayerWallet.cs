using EnemyInterface;
using System;

public class PlayerWallet : IPlayerWallet
{
    private Action<IPlayerWallet> _onDisplayMoney;

    private int _countMoney;

    public PlayerWallet(Action<IPlayerWallet> onDisplayMoney, int startMoney)
    {
        _onDisplayMoney = onDisplayMoney;

        _countMoney = startMoney;
    }

    public int GetCountMoney() => _countMoney;

    public void SpendMoney(IPurchasable purchasable)
    {
        _countMoney -= purchasable.GetPrice();

        _onDisplayMoney.Invoke(this);
    }


    public void FillUpMoney(IRewardMoneyEnemy reward)
    {
        _countMoney += reward.GetRewardMoneyEnemy();

        _onDisplayMoney.Invoke(this);
    }
}
