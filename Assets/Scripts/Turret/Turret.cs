using BulletInterface;
using EnemyInterface;
using UnityEngine;

public abstract class Turret : GameMono, ILoaderBullet
{
    [SerializeField] private Point _shotPosition;

    public IEnemy _enemy { get; private set; }

    private IFactoryReclaim<Turret> _factory;

    [SerializeField] private LayerMask _enemyMask;


    [SerializeField] private TypeDamageBullet _typeDamageBullet;
    public Vector3 ShotPosition => _shotPosition.GetPosition();

    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _distanceSearchEnemy;
    private float _progresShot;
    
    [SerializeField] private float _damage;


    public float DistanceSearchEnemy { get { return _distanceSearchEnemy; } private set { } }

    public float GetLaunchDamage() => _damage;

    public TypeDamageBullet GetLaunchTypeDamage() => _typeDamageBullet;

    public void Init(IFactoryReclaim<Turret> factory)
    {
        _factory = factory;
    }

    public void InitTurret(ICreaterBuild positionSpawn)
    {        
        transform.position = positionSpawn.GetCurrentPositionBuild();
        transform.rotation = Quaternion.identity;
    }

    public override void GameUpdate()
    {
        SearchEnemy();

        _progresShot += Time.deltaTime * _attackSpeed;

        if (_enemy == null)
            return;

        LookEnemy();

        if (_progresShot >= 1f)
        {
            Shot();
            _progresShot -= _progresShot;
        }
    }

    protected void LookEnemy() => _shotPosition.LooakAt(_enemy.GetPosition());

    protected abstract void Shot();    

    protected void SearchEnemy()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, _distanceSearchEnemy, _enemyMask);

        if (coll.Length >= 1)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                IEnemy enemy = coll[i].gameObject.GetComponent<IEnemy>();

                if (enemy != null)
                    _enemy = enemy;
                else
                    _enemy = null;
            }
        }
        else
        {
            _enemy = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distanceSearchEnemy);
    }
}
