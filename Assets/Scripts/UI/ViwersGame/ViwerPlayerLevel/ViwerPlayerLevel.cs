using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ViwerPlayerLevel : Text
{
    private string KEY_FOR_LEVEL = "LEVEL: ";

    private float _displayTime = 2f;

    public void DisplayPlayerLevel(IPlayerLevel playerLevel)
    {
        text = KEY_FOR_LEVEL + playerLevel.GetPlayerLevel();

        StartCoroutine(UdisplayPlayerLevel());
    }

    private IEnumerator UdisplayPlayerLevel()
    {
        yield return new WaitForSeconds(_displayTime);

        text = "";
    }
}
