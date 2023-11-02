using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public abstract class SliderValume : Slider
{
    protected AudioMixerGroup _audioMixer;

    protected override void OnEnable()
    {
        base.OnEnable();

        onValueChanged.AddListener(ChangeValume);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();

        onValueChanged.RemoveListener(ChangeValume);
    }

    public abstract void InitSlider(AudioMixerGroup audioMixer);

    protected abstract void ChangeValume(float valume);        
}
