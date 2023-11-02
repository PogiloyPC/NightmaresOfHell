public interface IPlayerWallet : ICountMoneyPlayer
{    
    void SpendMoney(IPurchasable purchasable);
}

public interface ICountMoneyPlayer
{
    int GetCountMoney();    
}