using UnityEngine;

public class ActivatorSettingsVolume : GameButton
{
    private Sprite _activeView;
    private Sprite _inactiveView;

    private IPanelSlidersVolume _panelSliders;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += ActivatePanelSettings;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= ActivatePanelSettings;
    }

    public void InitButton(Sprite activeView, Sprite inactiveView, IPanelSlidersVolume panelSliders)
    {
        _activeView = activeView;
        _inactiveView = inactiveView;

        _panelSliders = panelSliders;
    }

    private void ActivatePanelSettings()
    {
        _panelSliders.ChangedState();

        if (_panelSliders.GetActiveState())
            sprite = _inactiveView;
        else
            sprite = _activeView;
    }
}

