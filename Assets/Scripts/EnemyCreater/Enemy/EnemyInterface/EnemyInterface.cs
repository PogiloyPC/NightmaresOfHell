using BulletInterface;
using UnityEngine;
using System;

namespace EnemyInterface
{
    public interface IDamageDiller
    {
        float GetDamage();
    }

    public interface IHealthEnemy
    {
        bool TakeDamage(float damage);
    }

    public interface IEnemyMovable
    {
        void Move();
    }

    public interface IEnemy
    {
        Vector3 GetPosition();

        void TakeDamage(IDamageDillerEnemy bullet);

        Enemy GetNextEnemy();

        TypeExperienceStone GetTypeExperience();
    }

    public interface IRewardMoneyEnemy
    {
        int GetRewardMoneyEnemy();
    }
    
    public interface IRewardExperienceEnemy
    {
        float GetRewardExperienceEnemy();
    }

    public class HealthEnemy : IHealthEnemy
    {        
        private TypeArmor _typeArmor;

        private float _health;
        private float _procentArmor;

        public HealthEnemy(TypeArmor typeArmor, float health, float procentArmor)
        {            
            _typeArmor = typeArmor;

            _health = health;

            if (typeArmor != TypeArmor.emptyArmor)
                _procentArmor = procentArmor;
        }

        public bool TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0f)
                return true;
            else
                return false;
        }        
    }

    public class EnemyMovable : IEnemyMovable
    {
        private Rigidbody _rb;

        private Point _positionEnemy;

        private IToward _towardPosition;

        private float _speed;

        public EnemyMovable(Rigidbody rb, Point positionEnemy, IToward toward, float speed)
        {
            _rb = rb;

            _towardPosition = toward;
            _positionEnemy = positionEnemy;

            _speed = speed;
        }

        public void Move()
        {
            Vector3 direction = _towardPosition.GetPosition() - _positionEnemy.GetPosition();
            Vector3 directionNormalized = direction.normalized;

            _rb.velocity = directionNormalized * _speed;
        }
    }

    public enum TypeArmor
    {
        emptyArmor,
        physicalArmor,
        magicArmor,
        physicalAndMagicArmor
    }
}
