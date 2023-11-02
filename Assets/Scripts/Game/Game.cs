using UnityEngine;
using UnityEngine.Audio;
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

    private static FactoryEnemy _factoryEnemy;
    private static FactoryBullet _factoryBullet;
    private static FactoryTurret _factoryTurret;
    private static FactoryEffect _factoryEffect;
    private static FactoryExperienceStone _factoryExperienceStone;
    private CreaterExtension _createrExtension;

    private BuilderTurret<Turret> _builderTurret;

    private PlayerWallet _playerWallet;
    private PlayerLevel _playerLevel;   
    
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);

    [SerializeField] private int _startMoney;

    [SerializeField] private float _experienceForUpLevel;
    private float _timeGame;

    private bool _active;

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

        _gameUI.InitGameUI(out _onDisplayPanel, out _onDisplayHealthToward, out onTakeDamage,
            _builderTurret, _createrExtension, _playerWallet, _toward, _playerLevel);

        _toward.InitToward(_onDisplayHealthToward);

        _enemySpawner.InitCreater(_toward, onTakeDamage, this);        

        _gameUI.DisplayCountMoney(_playerWallet);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            _active = !_active;

        if (_active)
            return;

        _timeGame += Time.deltaTime;

        _gameUI.SetCurrentTimeGame(this);

        _factoryBullet.GameUpdate();
        _factoryTurret.GameUpdate();
        _factoryEffect.GameUpdate();
        _factoryExperienceStone.GameUpdate();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            CreateBuild();
    }

    private void FixedUpdate()
    {
        if (_active)
            return;

        _factoryEnemy.GameUpdate();        
    }

    private void TakeReward(IRewardMoneyEnemy rewardMoney)
    {
        _playerWallet.FillUpMoney(rewardMoney);        
    }
    
    private void TakeReward(IRewardExperienceEnemy rewardExperience)
    {        
        _playerLevel.TakeExperience(rewardExperience);
    }

    public float GetCurrentTimeGame() => _timeGame;

    private void CreateBuild()
    {
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit) && !_gameUI.GetLockTochScreen())
        {
            TowardForTurret place = hit.collider.gameObject.GetComponent<TowardForTurret>();

            ExtensionForTurret place2 = hit.collider.gameObject.GetComponent<ExtensionForTurret>();            

            if (place != null)
            {
                _builderTurret.SetPlaceForTurret(place);

                _createrExtension.SetTowardForExtension(place);

                _onDisplayPanel?.Invoke(place);
            }
            else if (place2 != null)
            {
                _builderTurret.SetPlaceForTurret(place2);

                _onDisplayPanel?.Invoke(place2);
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
