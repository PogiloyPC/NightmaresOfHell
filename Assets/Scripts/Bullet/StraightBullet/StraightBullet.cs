using System.Collections;
using UnityEngine;

public abstract class StraightBullet : Bullet
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _speed;

    public void InitBullet(Vector3 launchPosition, Vector3 targetPosition)
    {
        transform.position = launchPosition;
        _enemyTarget = targetPosition - transform.position;

        transform.LookAt(targetPosition);
    }

    protected override void MoveTarget()
    {       
        _rb.velocity = _enemyTarget.normalized * _speed;
    }
}

