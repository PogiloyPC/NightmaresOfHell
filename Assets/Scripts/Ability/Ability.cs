using BulletInterface;
using UnityEngine;

public interface IAbility
{
    Sprite GetIconAbility();

    IDAbility GetID();

    LevelAbility GetCurrentLevel();

    TypeAbility GetTypeAbility();

    void ResetAbility();

    void UpgradeAbility();

    void LaunchAbility();
}

public enum IDAbility
{
    meterorsRain,
    swordsPaladin,
    barrier,
    magicDamageBuster,
    physicalDamageBuster,
    turretDistanceAttackBuster,
    abilityCoolDownBuster,
    shield,
    healthUpper,
    experienceBuster,    
}

public enum LevelAbility
{
    firstLevel = 0,
    secondLevel = 1,
    thirdLevel = 2,
    fourthLevel = 3,
    maxLevel = 4
}

public enum TypeAbility
{
    activeAbility,
    passiveAbility
}

public abstract class Ability : ScriptableObject, IAbility
{
    [SerializeField] private Sprite _iconAbility;

    [SerializeField] private TypeAbility _typeAbility;
    [SerializeField] private IDAbility _idAbility;
    private LevelAbility _levelAbility = LevelAbility.firstLevel;

    public TypeAbility GetTypeAbility() => _typeAbility;

    public IDAbility GetID() => _idAbility;

    public LevelAbility GetCurrentLevel() => _levelAbility;

    public Sprite GetIconAbility() => _iconAbility;

    public abstract void ResetAbility();

    public abstract void UpgradeAbility();

    public abstract void LaunchAbility();
}

public abstract class ActiveAbility : Ability
{
    [SerializeField] private TypeDamage _typeDamage;

    [SerializeField] private float _damage;
    [SerializeField] private float _cooldownAbility;
    private int _countAttack;

    public abstract void Update();
}

public abstract class PassiveAbility : Ability
{

}

public class AbilityMeteorsRain : ActiveAbility
{    
    public override void LaunchAbility()
    {

    }

    public override void Update()
    {
        
    }

    public override void ResetAbility()
    {

    }

    public override void UpgradeAbility()
    {
        if (GetCurrentLevel() == LevelAbility.maxLevel)
            return;
    }
}

public class AbilityArchabgelCrosses : ActiveAbility
{
    public override void LaunchAbility()
    {

    }

    public override void ResetAbility()
    {

    }

    public override void UpgradeAbility()
    {
        
    }

    public override void Update()
    {
        
    }
}