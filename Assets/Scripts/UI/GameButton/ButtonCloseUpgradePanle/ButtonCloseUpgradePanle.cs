public class ButtonCloseUpgradePanle : GameButton
{
    private PanelUpgradeTurret _panleUpgrade;

    public void InitButton(PanelUpgradeTurret panelUpgrade) => _panleUpgrade = panelUpgrade;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += ClosePanle;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= ClosePanle;
    }

    private void ClosePanle() => _panleUpgrade.UndisplayPanel();
}
