using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurretButton : MonoBehaviour, ISeterPrice, IPurchasable
{
    [SerializeField] private Button _buyButton;

    [SerializeField] private ContainerTurret _containerTurret;

    [SerializeField] private PriceUI _priceUi;

    private Action _undisplayShopPanel;

    private IBuilderTurret<Turret> _factoryTurret;

    private IPlayerWallet _playerWallet;

    [SerializeField] private int _priceTurret;

    public int GetPrice() => _priceTurret;

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
        _priceUi.InitPrice(this);

        _factoryTurret = factoryTurret;

        _undisplayShopPanel = undisplayShop;

        _playerWallet = playerWallet;
    }

    private void BuyTurret()
    {
        if (_playerWallet.GetCountMoney() >= _priceTurret)
            if (_factoryTurret.BuildTurret(_containerTurret.Turret))
                _playerWallet.SpendMoney(this);

        _undisplayShopPanel.Invoke();
    }
}
