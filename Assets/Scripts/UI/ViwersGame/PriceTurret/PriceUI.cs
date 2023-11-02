using UnityEngine;
using UnityEngine.UI;

public class PriceUI : Text
{    
    public void InitPrice(ISeterPrice seterPrice)
    {
        text = seterPrice.GetPrice().ToString();
    }
}

