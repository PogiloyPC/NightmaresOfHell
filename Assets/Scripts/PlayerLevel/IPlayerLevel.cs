using System;

public interface IPlayerLevel
{
    int GetPlayerLevel();
}

public interface IInitPlayerLevel
{
    void InitPlayerLevel(Action<IPlayerLevel> onDisplayLevelPlayer, Action<IPlayerExperience> onDisplayExperiencePlayer);
}

public interface IPlayerExperience
{
    float GetCurrentExperience();

    float GetFinishExperience();
}