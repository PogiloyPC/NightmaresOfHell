using UnityEngine;

[CreateAssetMenu(fileName = "Container Experience Stone")]
public class ContainerExperienceStone : ScriptableObject
{
    [SerializeField] private ExperienceStone _easyStone;
    [SerializeField] private ExperienceStone _preMidStone;
    [SerializeField] private ExperienceStone _midStone;
    [SerializeField] private ExperienceStone _preHardStone;
    [SerializeField] private ExperienceStone _hardStone;

    public ExperienceStone EasyStone { get { return _easyStone; } private set { } }
    public ExperienceStone PreMidStone { get { return _preMidStone; } private set { } }
    public ExperienceStone MidStone { get { return _midStone; } private set { } }
    public ExperienceStone PreHardStone { get { return _preHardStone; } private set { } }
    public ExperienceStone HardStone { get { return _hardStone; } private set { } }
}

