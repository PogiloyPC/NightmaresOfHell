using UnityEngine;
using System.Collections.Generic;

public class FactoryTurret : Factory<Turret>
{    
}

public class CreaterExtension : ICreaterExtension, IPurchasable
{
    private ExtensionForTurret _prefab;

    private TowardForTurret _toward;

    public EffectBuild _particleBuild;

    private IPlayerWallet _playerWallet;

    private int _priceExtension = 200;

    public int GetPrice() => _priceExtension;

    public CreaterExtension(ExtensionForTurret extension, EffectBuild particleBuild,IPlayerWallet playerWallet)
    {
        _prefab = extension;

        _particleBuild = particleBuild;

        _playerWallet = playerWallet;
    }

    public void SetTowardForExtension(TowardForTurret toward)
    {
        _toward = toward;
    }

    public void BuildExtension()
    {
        if (_toward == null)
            return;        

        Vector3 position = new Vector3();
        Quaternion rotation = new Quaternion();

        if (_playerWallet.GetCountMoney() >= _priceExtension)
        {
            if (_toward.CheckSpace(out position, out rotation))
            {
                _playerWallet.SpendMoney(this);

                GameObject.Instantiate(_prefab, position, rotation);

                _particleBuild.PlayBuild(this);

                _toward = null;
            }
        }
    }

    public Vector3 GetCurrentPositionBuild()
    {
        if (_toward)
            return _toward.GetPositionForTurret();
        else
            return new Vector3();
    }
}