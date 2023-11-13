using UnityEngine;

public class EffectBuild : ParticleEffect
{   
    public override void GameUpdate()
    {
        _timeEffect -= Time.deltaTime;

        if (_timeEffect <= 0f)
            _factory.Reclaim(this);
    }

    public void PlayBuild(ILounchPosition creater)
    {
        transform.position = creater.GetLounchPosition();

        InitTimeEffect();

        PlayEffect();
    }
}
