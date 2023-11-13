using UnityEngine;

public class ButtonUpgradeTurret : GameButton
{
    private IUpgraderTurret _upgraderTurret;

    private IPlayerWallet _walletPlayer;

    public void InitButton(IPlayerWallet walletPlayer)
    {
        _walletPlayer = walletPlayer;
    }

    public void SetUpgrader(IUpgraderTurret upgraderTurret)
    {
        _upgraderTurret = upgraderTurret;

        if (_walletPlayer.GetCountMoney() >= _upgraderTurret.GetPrice())
            color = new Color(0.2f, 0.7f, 0.1f, 1f);
        else
            color = new Color(0.75f, 0.1f, 0.1f, 1f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += UpgradeTurret;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= UpgradeTurret;
    }

    private void UpgradeTurret()
    {
        if (_walletPlayer.GetCountMoney() >= _upgraderTurret.GetPrice())
        {
            _walletPlayer.SpendMoney(_upgraderTurret);

            _upgraderTurret.Upgrade();
        }
    }
}

