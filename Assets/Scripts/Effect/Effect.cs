using UnityEngine;

public abstract class Effect : GameMono
{
    protected IFactoryReclaim<Effect> _factory { get; private set; }

    public void Init(IFactoryReclaim<Effect> factory) => _factory = factory;        
}

public abstract class ParticleEffect : Effect
{
    [SerializeField] private ParticleSystem _effect;

    protected float _timeEffect;

    protected void InitTimeEffect() => _timeEffect = _effect.duration;

    protected void PlayEffect() => _effect.Play();
}