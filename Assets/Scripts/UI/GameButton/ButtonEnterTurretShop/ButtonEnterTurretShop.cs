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

    public void SetActive(IBuilderPanel panel) => gameObject.SetActive(panel.SetActiveButtonShop());

    private void EnterShopTurret()
    {
        _builderPanel.UndisplayPanel();

        _shopPanel.DisplayPanel();
    }
}
