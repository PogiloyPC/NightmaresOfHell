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
    [SerializeField] private ActivatorSettingsVolume _buttonSettings;

    [SerializeField] private ViwerHealthToward _viwerHealthToward;
    [SerializeField] private ViwerExperienceLevel _viwerExperience;
    [SerializeField] private ViwerPlayerLevel _viwerPlayerLevel;
    [SerializeField] private ViwerTimeGame _viwerTimeGame;
    [SerializeField] private ViwerDamageTurret _prefabViwer;

    [SerializeField] private BuilderPanel _panelBuilder;
    [SerializeField] private PanelShopTurret _panelShop;
    [SerializeField] private PanelSlidersVolume _panelSlidersVolume;

    [SerializeField] private PriceUI _priceLevelUpToward;

    [SerializeField] private CountMoneyUI _countMoneyUI;

    [SerializeField] private SliderValume[] _slidersSound;
    
    [SerializeField] private Sprite _lockedValume;
    [SerializeField] private Sprite _unlockedValume;
    [SerializeField] private Sprite _activeState;
    [SerializeField] private Sprite _inactiveState;

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
    }

    private void OnDisable()
    {        
        _onTakeDamage -= SpawnViwerDamageTurret;
        _onDisplayExperiencePlayer -= _viwerExperience.DisplayExperiencePlayer;
        _onDisplayLevelPlayer -= _viwerPlayerLevel.DisplayPlayerLevel;
        _undisplayShop -= _panelShop.UndisplayPanel;
        _onDisplayPanel -= DisplayPanel;
        _onDisplayHealthToward -= _viwerHealthToward.DisplayHealth;
    }

    public void InitGameUI(out Action<PlaceForTurret> onDisplayPanel, out Action<IPropertyHealthToward> onDisplayHealthToward,
        out Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage,
        IBuilderTurret<Turret> factoryTurret, ICreaterExtension createrExtension, IPlayerWallet playerWallet, ILevelToward toward,
        IInitPlayerLevel initPlayerLevel)
    {
        onDisplayPanel = _onDisplayPanel;
        onDisplayHealthToward = _onDisplayHealthToward;
        onTakeDamage = _onTakeDamage;

        _buttonEnterShopTurret.InitButton(_panelBuilder, _panelShop);
        _buttonCreaterExtension.Init(_panelBuilder, createrExtension);
        _buttonLevelUp.InitButton(_priceLevelUpToward, playerWallet, toward);

        initPlayerLevel.InitPlayerLevel(_onDisplayLevelPlayer, _onDisplayExperiencePlayer);

        foreach (BuyTurretButton button in _buyButton)
            button.Init(factoryTurret, _undisplayShop, playerWallet);

        foreach(SliderValume slider in _slidersSound)
            slider.InitSlider(_audioMixer);

        _buttonVolume.InitButton(_audioMixer, _lockedValume, _unlockedValume);
        _buttonSettings.InitButton(_activeState, _inactiveState,_panelSlidersVolume);        
    }

    public void DisplayCountMoney(ICountMoneyPlayer playerWallet) => _countMoneyUI.DisplayCountMoney(playerWallet);

    public void SetCurrentTimeGame(ITimeGame timeGame) => _viwerTimeGame.DisplayTimeGame(timeGame);

    private void SpawnViwerDamageTurret(float damage, IDamageDillerEnemy bullet, IEnemy enemy)
    {
        Game.SpawnEffect(_prefabViwer).
            InitViwerTakeDamgeEnemy(damage, bullet, _containerViwerGetDamageEnemy, enemy.GetPosition() + new Vector3(0f, 0.5f, 0f));        
    }

    public bool GetLockTochScreen() => _panelBuilder.GetActivePanel() || _panelShop.GetActivePanel() || _panelSlidersVolume.GetActiveState();

    public void DisplayPanel(PlaceForTurret place)
    {
        switch (place)
        {
            case (TowardForTurret):
                _panelBuilder.DisplayPanel();
                break;
            case (ExtensionForTurret):
                _panelShop.DisplayPanel();
                break;
        }
    }
}
