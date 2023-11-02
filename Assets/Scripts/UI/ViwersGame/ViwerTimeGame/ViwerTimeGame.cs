using UnityEngine.UI;

public class ViwerTimeGame : Text
{
    public void DisplayTimeGame(ITimeGame timeGame) => text = timeGame.GetCurrentTimeGame().ToString("00.00");
}

