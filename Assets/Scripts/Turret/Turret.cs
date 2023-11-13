using BulletInterface;
using EnemyInterface;
using UnityEngine;

public abstract class Turret : GameMono, IGameContent, IUpgradableTurret
{
    [SerializeField] private Point _shotPosition;
    [SerializeField] private Point _headTurret;

    [SerializeField] private Animator _anim;

    public TurretStats TurretStats { get; private set; }

    public IEnemy _enemy { get; private set; }

    private IFactoryReclaim<Turret> _factory;

    [SerializeField] private TypeGameContent _typeGameContent = TypeGameContent.turret;

    [SerializeField] private NameTurret _nameTurret;

    [SerializeField] private LayerMask _enemyMask;
    public Vector3 ShotPosition => _shotPosition.GetPosition();

    [SerializeField] private float _distanceSearchEnemy;
    private float _progresShot;
    private float _timeShot;

    public float DistanceSearchEnemy { get { return _distanceSearchEnemy; } private set { } }

    public TypeGameContent GetTypeGameContent() => _typeGameContent;

    public void Init(IFactoryReclaim<Turret> factory) => _factory = factory;

    public void InitTurretStats(TurretStats turretStats)
    {
        TurretStats = turretStats;

        _timeShot = 60f / TurretStats.ShotsPerMinute;
    }

    public void InitTurret(ILounchPosition positionSpawn)
    {
        transform.position = positionSpawn.GetLounchPosition();
        transform.rotation = Quaternion.identity;
    }

    public override void GameUpdate()
    {
        SearchEnemy();

        _progresShot += Time.deltaTime;

        if (_enemy == null)
            return;

        LookEnemy();

        if (_progresShot >= _timeShot)
        {
            Shot();
            _progresShot -= _progresShot;
        }
    }

    private void LookEnemy()
    {
        Vector3 directionRotate = _enemy.GetPosition() - _headTurret.GetPosition();
        directionRotate.y = 0;

        _headTurret.LookTarget(directionRotate);
    }

    protected virtual void RotateGun()
    {

    }

    protected abstract void Shot();

    protected void SearchEnemy()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, _distanceSearchEnemy, _enemyMask);

        float distance = 0f;
        float distanceNearyEnemy = float.MaxValue;


        if (coll.Length >= 1)
        {

            for (int i = 0; i < coll.Length; i++)
            {
                IEnemy enemy = coll[i].gameObject.GetComponent<IEnemy>();

                distance = Vector3.Distance(enemy.GetPosition(), transform.position);

                if (distance <= distanceNearyEnemy)
                {
                    _enemy = enemy;

                    distanceNearyEnemy = distance;
                }
            }
        }
        else
        {
            _enemy = null;
        }
    }

    public Vector3 GetPosition() => transform.position;

    public void ReloadTurret() => _factory.Reclaim(this);

    public NameTurret GetNameTurret() => _nameTurret;

    public LevelTurret GetCurrentLevelTurret() => TurretStats.Level;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distanceSearchEnemy);
    }
}
