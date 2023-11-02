using UnityEngine;
using System;

public abstract class PanelGame : MonoBehaviour
{    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayPanel()
    {
        gameObject.SetActive(true);
    }

    public bool GetActivePanel() => gameObject.activeSelf;

    public void UndisplayPanel()
    {
        gameObject.SetActive(false);
    }
}
