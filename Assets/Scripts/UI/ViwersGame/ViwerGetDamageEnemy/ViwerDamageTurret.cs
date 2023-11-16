using UnityEngine.UI;
using UnityEngine;
using BulletInterface;
using System.Collections;

public class ViwerDamageTurret : Effect
{
    [SerializeField] private Text _countDamage;

    private float _destroyTime = 1f;

    private Vector3 _startPos;
    private Vector3 _finishPos;


    public void InitViwerTakeDamgeEnemy(float damage, IDamageDillerEnemy bullet, Transform containerViwer, Vector3 positionLaunch)
    {
        transform.position = positionLaunch;

        transform.SetParent(containerViwer, true);

        transform.localScale = Vector3.one;

        _countDamage.text = damage.ToString();

        if (bullet.GetTypeDamage() == TypeDamage.magicDamage)
            _countDamage.color = Color.green;
        else if (bullet.GetTypeDamage() == TypeDamage.physicalDamage)
            _countDamage.color = Color.yellow;

        _startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _finishPos = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }

    public override void GameUpdate()
    {
        AnimateDestroy();
    }

    private void AnimateDestroy()
    {                
        _destroyTime -= Time.deltaTime;

        _countDamage.color = new Color(_countDamage.color.r, _countDamage.color.g, _countDamage.color.b, _destroyTime);

        transform.position = Vector3.Lerp(_finishPos, _startPos, _destroyTime);

        if (_destroyTime <= 0f)
            _factory.Reclaim(this);
    }
}

