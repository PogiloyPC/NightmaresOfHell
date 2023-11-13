using UnityEngine;

public class ContainerAllLevelTurret : ScriptableObject
{
    [SerializeField] private Turret _firstLevelUpTurret;
    [SerializeField] private Turret _secondLevelUpTurret;
    [SerializeField] private Turret _thirdLevelUpTurret;
    [SerializeField] private Turret _fourthLevelUpTurret;

    protected Turret FirstLevelUpTurret => _firstLevelUpTurret;
    protected Turret SecondLevelUpTurret => _secondLevelUpTurret;
    protected Turret ThirdLevelUpTurret => _thirdLevelUpTurret;
    protected Turret FourthLevelUpTurret => _fourthLevelUpTurret;
}

