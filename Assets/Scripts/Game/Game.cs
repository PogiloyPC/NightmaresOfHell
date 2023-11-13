using UnityEngine;
using System;
using EnemyInterface;
using BulletInterface;

public class Game : MonoBehaviour, ITimeGame
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Toward _toward;

    [SerializeField] private ContainerExperienceStone _containerExpStone;

    [SerializeField] private EnemySpawner _enemySpawner;

    [SerializeField] private GameUI _gameUI;

    [SerializeField] private EffectBuild _particleBuild;

    [SerializeField] private ExtensionForTurret _extension;

    private Action<PlaceForTurret> _onDisplayPanel;
    private Action<IRewardMoneyEnemy> _onTakeRewardMoney;
    private Action<IRewardExperienceEnemy> _onTakeRewardExperience;
    private Action<IPlayerWallet> _onDisplayCountMoney;
    private Action<IPropertyHealthToward> _onDisplayHealthToward;
    private Action<IUpgradableTurret, IUpgraderTurret> _onDisplayPanelUpgradeTurret;

    private static FactoryEnemy _factoryEnemy;
    private static FactoryBullet _factoryBullet;
    private static FactoryTurret _factoryTurret;
    private static FactoryEffect _factoryEffect;
    private static FactoryExperienceStone _factoryExperienceStone;
    private CreaterExtension _createrExtension;

    private BuilderTurret<Turret> _builderTurret;

    private PlayerWallet _playerWallet;
    private PlayerLevel _playerLevel;

    [SerializeField] private TurretUpgradeController _turretUpgradeController;

    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);

    [SerializeField] private int _startMoney;

    [SerializeField] private float _experienceForUpLevel;
    private float _timeGame;

    private void OnEnable()
    {
        _onTakeRewardMoney += TakeReward;
        _onTakeRewardExperience += TakeReward;
        _onDisplayCountMoney += _gameUI.DisplayCountMoney;
    }

    private void OnDisable()
    {
        _onTakeRewardMoney -= TakeReward;
        _onTakeRewardExperience -= TakeReward;
        _onDisplayCountMoney -= _gameUI.DisplayCountMoney;
    }

    private void Start()
    {
        IPanelTurretUpgrade panelTurretUpgrade = null;

        Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage;

        _playerWallet = new PlayerWallet(_onDisplayCountMoney, _startMoney);
        _playerLevel = new PlayerLevel(_experienceForUpLevel);

        _factoryBullet = new FactoryBullet();
        _factoryEffect = new FactoryEffect();
        _factoryEnemy = new FactoryEnemy(_onTakeRewardMoney);
        _factoryTurret = new FactoryTurret();
        _factoryExperienceStone = new FactoryExperienceStone(_onTakeRewardExperience, _containerExpStone);

        _createrExtension = new CreaterExtension(_extension, _particleBuild, _playerWallet);
        _builderTurret = new BuilderTurret<Turret>(_particleBuild);

        _gameUI.InitAction(out _onDisplayPanel, out _onDisplayHealthToward, out onTakeDamage, out _onDisplayPanelUpgradeTurret,
            out panelTurretUpgrade);
        _gameUI.InitButton(_builderTurret, _createrExtension, _playerWallet, _toward);
        _gameUI.InitPanel(_playerLevel);

        _toward.InitToward(_onDisplayHealthToward);

        _enemySpawner.InitCreater(_toward, onTakeDamage, this);

        _turretUpgradeController.InitUpgradeView(panelTurretUpgrade);

        _gameUI.DisplayCountMoney(_playerWallet);
    }

    private void Update()
    {
        _timeGame += Time.deltaTime;

        _gameUI.SetCurrentTimeGame(this);

        _factoryBullet.GameUpdate();
        _factoryTurret.GameUpdate();
        _factoryEffect.GameUpdate();
        _factoryExperienceStone.GameUpdate();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            ShootRaycastHit();
    }

    private void FixedUpdate()
    {
        _factoryEnemy.GameUpdate();
    }

    private void TakeReward(IRewardMoneyEnemy rewardMoney) => _playerWallet.FillUpMoney(rewardMoney);

    private void TakeReward(IRewardExperienceEnemy rewardExperience) => _playerLevel.TakeExperience(rewardExperience);

    public float GetCurrentTimeGame() => _timeGame;

    private void ShootRaycastHit()
    {
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit) && !_gameUI.GetLockTochScreen())
        {
            IGameContent gameContent = hit.collider.gameObject.GetComponent<IGameContent>();

            switch (gameContent.GetTypeGameContent())
            {
                case (TypeGameContent.placeForTurret):
                    PlaceForTurret place = (PlaceForTurret)gameContent;

                    HitPlaceForTurret(place);
                    break;
                case (TypeGameContent.turret):
                    Turret turret = (Turret)gameContent;

                    HitTurret(turret);
                    break;
            }
        }
    }

    private void HitPlaceForTurret(PlaceForTurret place)
    {
        if (place != null)
        {
            _builderTurret.SetPlaceForTurret(place);

            _onDisplayPanel?.Invoke(place);

            if (place is TowardForTurret toward)
                _createrExtension.SetTowardForExtension(toward);
        }
    }

    private void HitTurret(Turret turret)
    {
        IUpgraderTurret[] upgraders = _turretUpgradeController.GetAllUpgradersTurret();

        for (int i = 0; i < upgraders.Length; i++)
        {
            if (turret.GetNameTurret() == upgraders[i].GetTurretNameUpgrader() &&
                turret.GetCurrentLevelTurret() != LevelTurret.maxLevel)
            {                
                upgraders[i].SetTurretForUpgrade(turret);

                _onDisplayPanelUpgradeTurret.Invoke(turret, upgraders[i]);
            }
        }
    }

    public static T SpawnBullet<T>(T bullet) where T : Bullet
    {
        T instance = (T)_factoryBullet.GetInstance(bullet);

        instance.Init(_factoryBullet);

        return instance;
    }

    public static T SpawnTurret<T>(T turret) where T : Turret
    {
        T instance = (T)_factoryTurret.GetInstance(turret);

        instance.Init(_factoryTurret);

        return instance;
    }

    public static T SpawnEffect<T>(T explosion) where T : Effect
    {
        T instance = (T)_factoryEffect.GetInstance(explosion);

        instance.Init(_factoryEffect);

        return instance;
    }

    public static T SpawnEnemy<T>(T enemy) where T : Enemy
    {
        T instance = (T)_factoryEnemy.GetInstance(enemy);

        instance.Init(_factoryEnemy, _factoryEnemy);

        return instance;
    }

    public static T SpawnExperienceStone<T>(IEnemy enemy) where T : ExperienceStone
    {
        T instance = (T)_factoryExperienceStone.GetInstance(enemy);

        instance.Init(_factoryExperienceStone);

        return instance;
    }
}
