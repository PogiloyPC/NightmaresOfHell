using UnityEngine.UI;

public class CountMoneyUI : Text
{
    public void DisplayCountMoney(ICountMoneyPlayer countPlayer)
    {
        text = countPlayer.GetCountMoney().ToString();
    }
}
