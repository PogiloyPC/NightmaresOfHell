using UnityEngine;
using EnemyInterface;

public class ExperienceStone : GameMono, IRewardExperienceEnemy, IGameContent
{
    [SerializeField] private TypeGameContent _typeGameContent;

    private IFactoryReclaim<ExperienceStone> _factory;

    private IToward _toward;

    private Vector3 _t;
    private Vector3 _t1;
    private Vector3 _t2;

    private float rewardExperience;
    private float _time;
    private float _flightTime;
    private float _speed = 15f;

    public void Init(IFactoryReclaim<ExperienceStone> factory)
    {
        _factory = factory;
    }

    public void InitStone(IEnemy enemy, IRewardExperienceEnemy reward, IToward toward)
    {
        _toward = toward;
        float hight = 10f;

        transform.position = enemy.GetPosition();
        transform.LookAt(toward.GetPosition());

        float x = Vector3.Distance(transform.position, _toward.GetPosition());

        _flightTime = x / _speed;

        _t = transform.position;
        _t1 = new Vector3(transform.position.x + (x / 4), hight, transform.position.z) + transform.forward;
        _t2 = new Vector3(transform.position.x + ((x * 3) / 4), hight, transform.position.z) + transform.forward;

        rewardExperience = reward.GetRewardExperienceEnemy();
    }

    public TypeGameContent GetTypeGameContent() => _typeGameContent;

    public override void GameUpdate()
    {
        _time += Time.deltaTime;

        Move();

        if (_time >= _flightTime)
        {
            _factory.Reclaim(this);
        }
    }

    public float GetRewardExperienceEnemy() => rewardExperience;

    private void Move()
    {
        Vector3 t = Vector3.Lerp(_t, _t1, _time / _flightTime);
        Vector3 t1 = Vector3.Lerp(_t1, _t2, _time / _flightTime);
        Vector3 t2 = Vector3.Lerp(_t2, _toward.GetPosition(), _time / _flightTime);
        Vector3 t01 = Vector3.Lerp(t, t1, _time / _flightTime);
        Vector3 t12 = Vector3.Lerp(t1, t2, _time / _flightTime);
        Vector3 bezie = Vector3.Lerp(t01, t12, _time / _flightTime);

        transform.position = bezie;
    }
}

