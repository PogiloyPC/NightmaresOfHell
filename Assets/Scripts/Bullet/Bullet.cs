using System.Collections;
using BulletInterface;
using EnemyInterface;
using UnityEngine;

public abstract class Bullet : GameMono, IDamageDillerEnemy
{
    protected IFactoryReclaim<Bullet> _factoryBullet { get; private set; }

    private TypeDamageBullet _typeDamage;

    protected Vector3 _enemyTarget;

    private float _damage;

    [SerializeField] private float _lifeTimeBullet = 7f;
    public float LifeTimeBullet { get { return _lifeTimeBullet; } private set { } }

    public float GetDamageBullet() => _damage;

    public TypeDamageBullet GetTypeDamage() => _typeDamage;

    public void Init(IFactoryReclaim<Bullet> factory) => _factoryBullet = factory;

    public void InitDamageDiller(ILoaderBullet loaderBullet)
    {
        _damage = loaderBullet.GetLaunchDamage();

        _typeDamage = loaderBullet.GetLaunchTypeDamage();
    }

    public override void GameUpdate()
    {
        _lifeTimeBullet -= Time.deltaTime;

        if (_lifeTimeBullet <= 0f)
            _factoryBullet.Reclaim(this);

        MoveTarget();
    }

    protected abstract void MoveTarget();

    protected abstract void GiveDamage(IEnemy enemy);

    protected virtual void CollisionGround()
    {
        _factoryBullet.Reclaim(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemy enemy = other.gameObject.GetComponent<IEnemy>();

        Ground ground = other.gameObject.GetComponent<Ground>();

        if (enemy != null)
            GiveDamage(enemy);
        else if (ground != null)
            CollisionGround();
    }
}
