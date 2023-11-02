using UnityEngine.Audio;
using UnityEngine;

public class SliderMusicValume : SliderValume
{
    public override void InitSlider(AudioMixerGroup audioMixer)
    {
        _audioMixer = audioMixer;

        value = PlayerPrefs.GetFloat("valume music");

        _audioMixer.audioMixer.SetFloat("MusicValume", Mathf.Lerp(-80f, 20f, value));        
    }


    protected override void ChangeValume(float valume)
    {
        value = valume;

        _audioMixer.audioMixer.SetFloat("MusicValume", Mathf.Lerp(-80f, 20f, valume));

        PlayerPrefs.SetFloat("valume music", valume);
    }    
}

