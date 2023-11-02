using UnityEngine.Audio;
using UnityEngine;

public class SiderGameValume : SliderValume
{
    public override void InitSlider(AudioMixerGroup audioMixer)
    {
        _audioMixer = audioMixer;
        
        value = PlayerPrefs.GetFloat("valume game");
        
        _audioMixer.audioMixer.SetFloat("GameValume", Mathf.Lerp(-80f, 20f, value));
    }


    protected override void ChangeValume(float valume)
    {
        value = valume;

        _audioMixer.audioMixer.SetFloat("GameValume", Mathf.Lerp(-80f, 20f, value));

        PlayerPrefs.SetFloat("valume game", value);
    }
}

