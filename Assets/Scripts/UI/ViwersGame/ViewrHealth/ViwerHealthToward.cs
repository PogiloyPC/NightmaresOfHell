using UnityEngine.UI;

public class ViwerHealthToward : Image
{   
    public void DisplayHealth(IPropertyHealthToward healthToward)
    {
        fillAmount = healthToward.GetCurrentHealth() / healthToward.GetStartHealth();
    }
}

