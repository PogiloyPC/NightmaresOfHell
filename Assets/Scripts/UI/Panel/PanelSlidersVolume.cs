using UnityEngine;

public class PanelSlidersVolume : MonoBehaviour, IPanelSlidersVolume
{
    private bool _stateActive;

    private void Start()
    {
        gameObject.SetActive(_stateActive);
    }

    public void ChangedState()
    {
        _stateActive = !_stateActive;

        gameObject.SetActive(_stateActive);
    }

    public bool GetActiveState() => gameObject.activeSelf;
}
