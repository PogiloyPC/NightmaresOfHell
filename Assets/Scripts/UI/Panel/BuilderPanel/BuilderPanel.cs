using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderPanel : PanelGame, IBuilderPanel
{
    private ButtonEnterTurretShop _buttonEnterTurretShop;
    private ButtonCreaterExtension _buttonCreater;

    private bool _displayShopButton;
    private bool _displayCreaterButton;

    public bool SetActiveButtonShop() => _displayShopButton;
    public bool SetActiveButtonCreatre() => _displayCreaterButton;

    public void InitPanelBuilder(ButtonEnterTurretShop buttonEnterShop, ButtonCreaterExtension buttonCreater)
    {
        _buttonEnterTurretShop = buttonEnterShop;
        _buttonCreater = buttonCreater;
    }

    public void DisplayButton(TowardForTurret place)
    {
        Vector3 zero = new Vector3();
        Quaternion rot = new Quaternion();

        if (place.HaveTurret)
            _displayShopButton = false;
        else
            _displayShopButton = true;

        if (place.CheckSpace(out zero, out rot))
            _displayCreaterButton = true;
        else
            _displayCreaterButton = false;

        _buttonEnterTurretShop.SetActive(this);
        _buttonCreater.SetActive(this);
    }
}
