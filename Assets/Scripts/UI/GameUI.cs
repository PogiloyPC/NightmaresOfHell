using UnityEngine;
using UnityEngine.Audio;
using System;
using BulletInterface;
using EnemyInterface;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform _containerViwerGetDamageEnemy;

    [SerializeField] private BuyTurretButton[] _buyButton;
    [SerializeField] private ButtonEnterTurretShop _buttonEnterShopTurret;
    [SerializeField] private ButtonCreaterExtension _buttonCreaterExtension;
    [SerializeField] private ButtonLevelUpToward _buttonLevelUp;
    [SerializeField] private ButtonValume _buttonVolume;
    [SerializeField] private ButtonUpgradeTurret _buttonUpgradeTurret;
    [SerializeField] private ButtonCloseUpgradePanle _buttonCloseUpgradePanle;
    [SerializeField] private ActivatorSettingsVolume _buttonSettings;

    [SerializeField] private ViwerHealthToward _viwerHealthToward;
    [SerializeField] private ViwerExperienceLevel _viwerExperience;
    [SerializeField] private ViwerPlayerLevel _viwerPlayerLevel;
    [SerializeField] private ViwerTimeGame _viwerTimeGame;
    [SerializeField] private ViwerDamageTurret _prefabViwer;

    [SerializeField] private BuilderPanel _panelBuilder;
    [SerializeField] private PanelShopTurret _panelShop;
    [SerializeField] private PanelSlidersVolume _panelSlidersVolume;
    [SerializeField] private PanelUpgradeTurret _panelUpgradeTurret;

    [SerializeField] private PriceUI _priceLevelUpToward;
    [SerializeField] private PriceUI _viwerPriceUpgradeTurret;

    [SerializeField] private ViwerTurret _currentImageTurret;
    [SerializeField] private ViwerTurret _nextImageTurret;

    [SerializeField] private CountMoneyUI _countMoneyUI;

    [SerializeField] private SliderValume[] _slidersSound;

    [SerializeField] private Sprite _lockedValume;
    [SerializeField] private Sprite _unlockedValume;
    [SerializeField] private Sprite _activeState;
    [SerializeField] private Sprite _inactiveState;

    private Action<IUpgradableTurret, IUpgraderTurret> _onDisplayPanelUpgradeTurret;
    //Action for buy build
    private Action _undisplayShop;
    private Action<PlaceForTurret> _onDisplayPanel;
    //Action for display health toward
    private Action<IPropertyHealthToward> _onDisplayHealthToward;
    //Action for display level player
    private Action<IPlayerLevel> _onDisplayLevelPlayer;
    private Action<IPlayerExperience> _onDisplayExperiencePlayer;
    //Action for display get damage enemy
    private Action<float, IDamageDillerEnemy, IEnemy> _onTakeDamage;

    [SerializeField] private AudioMixerGroup _audioMixer;

    private void OnEnable()
    {
        _onTakeDamage += SpawnViwerDamageTurret;
        _onDisplayExperiencePlayer += _viwerExperience.DisplayExperiencePlayer;
        _onDisplayLevelPlayer += _viwerPlayerLevel.DisplayPlayerLevel;
        _undisplayShop += _panelShop.UndisplayPanel;
        _onDisplayPanel += DisplayPanel;
        _onDisplayHealthToward += _viwerHealthToward.DisplayHealth;
        _onDisplayPanelUpgradeTurret += ActivatePanelUpgradeTurret;
    }

    private void OnDisable()
    {
        _onTakeDamage -= SpawnViwerDamageTurret;
        _onDisplayExperiencePlayer -= _viwerExperience.DisplayExperiencePlayer;
        _onDisplayLevelPlayer -= _viwerPlayerLevel.DisplayPlayerLevel;
        _undisplayShop -= _panelShop.UndisplayPanel;
        _onDisplayPanel -= DisplayPanel;
        _onDisplayHealthToward -= _viwerHealthToward.DisplayHealth;
        _onDisplayPanelUpgradeTurret -= ActivatePanelUpgradeTurret;
    }

    public void InitAction(out Action<PlaceForTurret> onDisplayPanel, out Action<IPropertyHealthToward> onDisplayHealthToward,
        out Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage, out Action<IUpgradableTurret, IUpgraderTurret> onDisplayPanelUpgradeTurret,
        out IPanelTurretUpgrade panelTurretUpgrade)
    {
        onDisplayPanel = _onDisplayPanel;
        onDisplayHealthToward = _onDisplayHealthToward;
        onTakeDamage = _onTakeDamage;
        onDisplayPanelUpgradeTurret = _onDisplayPanelUpgradeTurret;

        panelTurretUpgrade = _panelUpgradeTurret;
    }

    public void InitButton(IBuilderTurret<Turret> factoryTurret, ICreaterExtension createrExtension, IPlayerWallet playerWallet,
        ILevelToward toward)
    {
        _buttonEnterShopTurret.InitButton(_panelBuilder, _panelShop);
        _buttonCreaterExtension.Init(_panelBuilder, createrExtension);
        _buttonLevelUp.InitButton(_priceLevelUpToward, playerWallet, toward);

        _buttonUpgradeTurret.InitButton(playerWallet);
        _buttonCloseUpgradePanle.InitButton(_panelUpgradeTurret);

        foreach (BuyTurretButton button in _buyButton)
            button.Init(factoryTurret, _undisplayShop, playerWallet);

        foreach (SliderValume slider in _slidersSound)
            slider.InitSlider(_audioMixer);

        _buttonVolume.InitButton(_audioMixer, _lockedValume, _unlockedValume);
        _buttonSettings.InitButton(_activeState, _inactiveState, _panelSlidersVolume);
    }

    public void InitPanel(IInitPlayerLevel initPlayerLevel)
    {
        _panelBuilder.InitPanelBuilder(_buttonEnterShopTurret, _buttonCreaterExtension);

        initPlayerLevel.InitPlayerLevel(_onDisplayLevelPlayer, _onDisplayExperiencePlayer);

        _panelUpgradeTurret.InitPanel(_viwerPriceUpgradeTurret, _currentImageTurret, _nextImageTurret);
    }   

    public void DisplayCountMoney(ICountMoneyPlayer playerWallet) => _countMoneyUI.DisplayCountMoney(playerWallet);

    public void SetCurrentTimeGame(ITimeGame timeGame) => _viwerTimeGame.DisplayTimeGame(timeGame);

    private void SpawnViwerDamageTurret(float damage, IDamageDillerEnemy bullet, IEnemy enemy)
    {
        Game.SpawnEffect(_prefabViwer).
            InitViwerTakeDamgeEnemy(damage, bullet, _containerViwerGetDamageEnemy, enemy.GetPosition() + new Vector3(0f, 0.5f, 0f));
    }

    private void ActivatePanelUpgradeTurret(IUpgradableTurret turret, IUpgraderTurret upgraderTurret)
    {
        _panelUpgradeTurret.SetTurretForUpgrade(turret, upgraderTurret);

        _buttonUpgradeTurret.SetUpgrader(upgraderTurret);

        _panelUpgradeTurret.DisplayPanel();
    }

    public bool GetLockTochScreen() => _panelBuilder.GetActivePanel() || _panelShop.GetActivePanel() || _panelSlidersVolume.GetActiveState()
        || _panelUpgradeTurret.GetActivePanel();

    public void DisplayPanel(PlaceForTurret place)
    {
        switch (place)
        {
            case (TowardForTurret):
                if (place is TowardForTurret toward)
                {
                    _panelBuilder.DisplayButton(toward);

                    if (toward.HaveSpaceForExtension || !toward.HaveTurret)
                        _panelBuilder.DisplayPanel();
                }
                break;
            case (ExtensionForTurret):
                if (!place.HaveTurret)
                    _panelShop.DisplayPanel();
                break;
        }
    }
}
