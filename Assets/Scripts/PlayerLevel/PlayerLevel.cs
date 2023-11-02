using EnemyInterface;
using System;
using UnityEngine;

public class PlayerLevel : IPlayerLevel, IPlayerExperience, IInitPlayerLevel
{
    private int _level;

    private float _currentExperience;
    private float _experienceForUpLevel;

    private float _procentForUpExperience = 45f;

    private Action<IPlayerLevel> _onUpLevel;
    private Action<IPlayerExperience> _onChangeExperience;

    public PlayerLevel(float experienceForUpLevel)
    {        
        _experienceForUpLevel = experienceForUpLevel; 
    }

    public void InitPlayerLevel(Action<IPlayerLevel> onDisplayLevelPlayer, Action<IPlayerExperience> onDisplayExperiencePlayer)
    {
        _onUpLevel = onDisplayLevelPlayer;
        _onChangeExperience = onDisplayExperiencePlayer;

        _onChangeExperience.Invoke(this);
    }

    public int GetPlayerLevel() => _level;

    public float GetCurrentExperience() => _currentExperience;
    public float GetFinishExperience() => _experienceForUpLevel;

    private void LevelUp()
    {
        _level++;

        _onUpLevel.Invoke(this);
        _onChangeExperience.Invoke(this);

        float procentExperience = _experienceForUpLevel / 100f;
        _experienceForUpLevel += procentExperience * _procentForUpExperience;

        Debug.Log(_experienceForUpLevel);
    }

    public void TakeExperience(IRewardExperienceEnemy rewardExperience)
    {
        _currentExperience += rewardExperience.GetRewardExperienceEnemy();

        _onChangeExperience.Invoke(this);

        if (_currentExperience >= _experienceForUpLevel)
        {
            LevelUp();

            _currentExperience -= _currentExperience;   
        }
    }
}
