using System.Collections;
using UnityEngine;

public abstract class BallisticBullet : Bullet
{
    private Vector3 _launchVelocity;
    private Vector3 _launchPoint;

    private float _age;

    public void InitBullet(Vector3 launchPosition, Vector3 targetPosition, Vector3 launchVelocity)
    {
        transform.position = launchPosition;
        _launchPoint = launchPosition;
        _enemyTarget = targetPosition;
        _launchVelocity = launchVelocity;

        transform.LookAt(targetPosition);        
    }

    protected override void MoveTarget()
    {
        _age += Time.deltaTime;

        Vector3 p = _launchPoint + _launchVelocity * _age;
        p.y -= 0.5f * 9.81f * _age * _age;

        transform.localPosition = p;
    }
}
