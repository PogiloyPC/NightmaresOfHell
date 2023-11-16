using UnityEngine;
using System.Collections;
using BulletInterface;
using EnemyInterface;

public class Explosion : Effect, IDamageDillerEnemy
{
    [SerializeField] private AnimationCurve _upSize;
    [SerializeField] private AnimationCurve _attenuation;

    [SerializeField] private Material _material;

    private LayerMask _enemyMask;

    private TypeDamage _typeDamage;

    private float _damage;
    private float _timeAnimation;
    private float _radiusExplosion;

    private Vector3 _size;

    private float interpolate = 0f;

    public float GetDamageBullet() => _damage;

    public TypeDamage GetTypeDamage() => _typeDamage;

    public void InitExplosion(IDamageDillerEnemy bullet, Vector3 positionExplosion, float radiusExplosion, LayerMask enemyMask)
    {
        transform.position = positionExplosion;

        _typeDamage = bullet.GetTypeDamage();

        _size = transform.localScale;

        _enemyMask = enemyMask;

        _timeAnimation = _upSize.keys[_upSize.keys.Length - 1].time;
        _radiusExplosion = radiusExplosion;
        _damage = bullet.GetDamageBullet();

        Explose();
    }

    public override void GameUpdate()
    {
        AnimationExplosive();
    }

    private void AnimationExplosive()
    {
        interpolate += Time.deltaTime;

        transform.localScale = new Vector3(_size.x + _upSize.Evaluate(interpolate), _size.y + (_upSize.Evaluate(interpolate) * 1.5f),
            _size.z + _upSize.Evaluate(interpolate));

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, _attenuation.Evaluate(interpolate));

        if (interpolate >= _timeAnimation)
            _factory.Reclaim(this);
    }

    private void Explose()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion, _enemyMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            IEnemy enemy = colliders[i].gameObject.GetComponent<IEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(this);
            }
        }
    }
}

