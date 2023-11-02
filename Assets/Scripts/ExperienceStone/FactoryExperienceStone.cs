using UnityEngine;
using EnemyInterface;
using System;

public class FactoryExperienceStone : Factory<ExperienceStone>
{
    private Action<IRewardExperienceEnemy> _onTakeReward;

    private ContainerExperienceStone _containerExpStone;

    public FactoryExperienceStone(Action<IRewardExperienceEnemy> onTakeReward, ContainerExperienceStone container)
    {
        _onTakeReward = onTakeReward;

        _containerExpStone = container;
    }

    public ExperienceStone GetInstance(IEnemy enemy)
    {
        ExperienceStone instance = null;

        if (enemy.GetTypeExperience() == TypeExperienceStone.easyStone)
            instance = GetInstance(_containerExpStone.EasyStone);
        if (enemy.GetTypeExperience() == TypeExperienceStone.preMidStone)
            instance = GetInstance(_containerExpStone.PreMidStone);
        if (enemy.GetTypeExperience() == TypeExperienceStone.midStone)
            instance = GetInstance(_containerExpStone.MidStone);
        if (enemy.GetTypeExperience() == TypeExperienceStone.preHardStone)
            instance = GetInstance(_containerExpStone.PreHardStone);
        if (enemy.GetTypeExperience() == TypeExperienceStone.hardStone)
            instance = GetInstance(_containerExpStone.HardStone);

        return instance;
    }

    protected override void DoingAction(ExperienceStone objectFactory) => _onTakeReward.Invoke(objectFactory);

}
