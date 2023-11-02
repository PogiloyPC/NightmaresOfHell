using EnemyInterface;
using UnityEngine;
using System;
using BulletInterface;

public class Enemy : GameMono, IEnemy, IRewardMoneyEnemy, IRewardExperienceEnemy, IDamageDiller
{
    private Enemy _nextEnemy;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Point _positionEnemy;

    private Action<float, IDamageDillerEnemy, IEnemy> _onTakeDamage;

    [SerializeField] private TypeArmor _typeArmor;
    [SerializeField] private TypeExperienceStone _typeExperience;

    private IHealthEnemy _healthEnemy;
    private IEnemyMovable _movable;

    private IFactoryReclaim<Enemy> _factory;
    private IContaineEnemy _containe;

    private IToward _toward;

    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _procentArmor;
    [SerializeField] private float _damage;
    [SerializeField] private float _rewardEnemyExperience;

    [SerializeField] private int _rewardEnemyMoney;

    public void Init(IFactoryReclaim<Enemy> factory, IContaineEnemy containe)
    {
        _factory = factory;

        _containe = containe;
    }

    public void InitEnemy(Action<float, IDamageDillerEnemy, IEnemy> onTakeDamage, IToward toward, Vector3 launchPosition)
    {
        _healthEnemy = new HealthEnemy(_typeArmor, _health, _procentArmor);
        _movable = new EnemyMovable(_rb, _positionEnemy, toward, _speed);

        _toward = toward;

        transform.position = launchPosition;

        _onTakeDamage = onTakeDamage;
    }
    
    public override void GameUpdate()
    {
        _movable.Move();
    }

    public void GetDamage(IDamageDillerEnemy bullet)
    {
        float damage = 0f;

        switch (_typeArmor)
        {
            case (TypeArmor.emptyArmor):
                damage = bullet.GetDamageBullet();
                break;
            case (TypeArmor.magicArmor):
                if (bullet.GetTypeDamage() == TypeDamageBullet.magicDamage)
                    damage = CalculateDamage(bullet);
                else
                    damage = bullet.GetDamageBullet();
                break;
            case (TypeArmor.physicalArmor):
                if (bullet.GetTypeDamage() == TypeDamageBullet.physicalDamage)
                    damage = CalculateDamage(bullet);
                else
                    damage = bullet.GetDamageBullet();
                break;
            case (TypeArmor.physicalAndMagicArmor):
                damage = CalculateDamage(bullet);
                break;
        }

        _onTakeDamage.Invoke(damage, bullet, this);

        if (_healthEnemy.TakeDamage(damage))
        {
            Game.SpawnExperienceStone<ExperienceStone>(this).InitStone(this, this, _toward);

            _factory.Reclaim(this);
        }
    }

    private float CalculateDamage(IDamageDillerEnemy bullet)
    {
        float damageProcent = bullet.GetDamageBullet() / 100f;
        float damage = bullet.GetDamageBullet() - (damageProcent * _procentArmor);

        return damage;
    }

    public Enemy GetNextEnemy()
    {
        Enemy enemy = null;

        _containe.GetRandomEnemy(out enemy);

        return enemy;
    }

    public TypeExperienceStone GetTypeExperience() => _typeExperience;

    public int GetRewardMoneyEnemy() => _rewardEnemyMoney;

    public float GetRewardExperienceEnemy() => _rewardEnemyExperience;

    public Vector3 GetPosition() => _positionEnemy.GetPosition();

    public float GetDamage() => _damage;

    private void OnCollisionEnter(Collision other)
    {
        IContaineHealthToward partToward = other.gameObject.GetComponent<IContaineHealthToward>();

        if (partToward != null)
        {
            partToward.TakeDamageDiller(this);
        }
    }
}
