using UnityEngine.UI;
using UnityEngine;

public class ButtonLevelUpToward : GameButton, IPurchasable, ISeterPrice
{
    private PriceUI _priceLevelUpUI;

    private IPlayerWallet _playerWallet;
    private ILevelToward _toward;

    private int _priceLevelUpToward = 1000;

    public int GetPrice() => _priceLevelUpToward;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += OnClickButton;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= OnClickButton;
    }

    public void InitButton(PriceUI priceViwer, IPlayerWallet playerWallet, ILevelToward toward)
    {
        _priceLevelUpUI = priceViwer;

        _playerWallet = playerWallet;

        _toward = toward;

        _priceLevelUpUI.InitPrice(this);

        if (_toward.GetMaxLevel())
            gameObject.SetActive(false);
    }

    private void OnClickButton()
    {
        if (_playerWallet.GetCountMoney() >= _priceLevelUpToward)
        {
            _playerWallet.SpendMoney(this);

            _toward.LevelUp();

            _priceLevelUpToward *= 3;

            _priceLevelUpUI.InitPrice(this);
        }

        if (_toward.GetMaxLevel())
            gameObject.SetActive(false);
    }
}

