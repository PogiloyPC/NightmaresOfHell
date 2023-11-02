using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonValume : GameButton
{
    private AudioMixerGroup _audioMixer;

    private Sprite _lockedImage;
    private Sprite _unlockedImage;

    private float _minVolume = -80f;
    private float _maxVolume = 0f;

    public bool _lockedVolume;

    public void InitButton(AudioMixerGroup audioMixer, Sprite lockedImage, Sprite unlockedImage)
    {
        _audioMixer = audioMixer;

        _lockedImage = lockedImage;
        _unlockedImage = unlockedImage;

         int locked = PlayerPrefs.GetInt("lockedVolume");
       
        if (locked == 1)
        {
            _lockedVolume = true;
            _audioMixer.audioMixer.SetFloat("MasterValume", _minVolume);
        }
        else
        {
            _lockedVolume = false;
            _audioMixer.audioMixer.SetFloat("MasterValume", _maxVolume);
        }

        ChangedVolumeGame();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += ChangedVolumeGame;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= ChangedVolumeGame;
    }

    private void ChangedVolumeGame()
    {
        _lockedVolume = !_lockedVolume;

        float valume = 0f;

        if (_lockedVolume)
        {
            valume = _minVolume;

            sprite = _lockedImage;                       
        }
        else
        {
            valume = _maxVolume;
            
            sprite = _unlockedImage;            
        }

        _audioMixer.audioMixer.SetFloat("MasterValume", valume);

        PlayerPrefs.SetInt("lockedVolume", _lockedVolume ? 1 : 0);        
    }
}

