using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurretButton : MonoBehaviour
{
    [SerializeField] private Button _buyButton;

    [SerializeField] private ViwerTurret _imageTurret;

    [SerializeField] private ContainerTurret _containerTurret;

    [SerializeField] private PriceUI _priceUi;

    private Action _undisplayShopPanel;

    private IBuilderTurret<Turret> _factoryTurret;

    private IPlayerWallet _playerWallet;

    protected void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyTurret);
    }

    protected void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyTurret);
    }

    public void Init(IBuilderTurret<Turret> factoryTurret, Action undisplayShop, IPlayerWallet playerWallet)
    {
        _priceUi.InitPrice(_containerTurret);

        _imageTurret.InitImage(_containerTurret.ImageTurret);

        _factoryTurret = factoryTurret;

        _undisplayShopPanel = undisplayShop;

        _playerWallet = playerWallet;
    }

    private void BuyTurret()
    {
        if (_playerWallet.GetCountMoney() >= _containerTurret.GetPrice())
            if (_factoryTurret.BuildTurret(_containerTurret.Turret))
                _playerWallet.SpendMoney(_containerTurret);

        _undisplayShopPanel.Invoke();
    }
}
