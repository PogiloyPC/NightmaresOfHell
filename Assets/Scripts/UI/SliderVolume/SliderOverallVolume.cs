using UnityEngine.Audio;
using UnityEngine;

public class SliderOverallVolume : SliderValume
{
    public override void InitSlider(AudioMixerGroup audioMixer)
    {
        _audioMixer = audioMixer;

        value = PlayerPrefs.GetFloat("overall volume");

        _audioMixer.audioMixer.SetFloat("OverallValume", Mathf.Lerp(-80f, 20f, value));
    }

    protected override void ChangeValume(float valume)
    {
        value = valume;

        _audioMixer.audioMixer.SetFloat("OverallValume", Mathf.Lerp(-80f, 20f, value));

        PlayerPrefs.SetFloat("overall volume", value);
    }
}

