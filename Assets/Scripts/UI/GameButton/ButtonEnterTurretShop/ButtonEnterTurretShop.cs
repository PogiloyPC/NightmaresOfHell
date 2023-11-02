using UnityEngine;

public class ButtonEnterTurretShop : GameButton
{
    private BuilderPanel _builderPanel;
    private PanelShopTurret _shopPanel;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += EnterShopTurret;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= EnterShopTurret;
    }

    public void InitButton(BuilderPanel builderPanel, PanelShopTurret shopPanel)
    {
        _builderPanel = builderPanel;

        _shopPanel = shopPanel;
    }

    private void EnterShopTurret()
    {
        _builderPanel.UndisplayPanel();

        _shopPanel.DisplayPanel();
    }
}
