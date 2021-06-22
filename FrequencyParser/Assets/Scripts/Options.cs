using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class Options : MonoBehaviour {
    public Dropdown rangeDropdownMenu;
    public Dropdown microphoneDropdownMenu;
    public Slider noiseFilter;
    public Slider toneThresh;

    private FrequencyHandler _frequencyHandler;
    private VocalRanges ranges = new VocalRanges();

    void Start() {
        _frequencyHandler = FindObjectOfType<FrequencyHandler>();
        CreateMicDropdown();
        CreateRangeDropdown();
        
        
        if (SceneHandler.NoiseFilter == 0) {
            noiseFilter.value = 0.05f;
        }
        else {
            noiseFilter.value = SceneHandler.NoiseFilter;
        }
        
        if (SceneHandler.ToneThresh == 0) {
            toneThresh.value = 0.1f;
        }
        else {
            toneThresh.value = SceneHandler.ToneThresh;
        }

    }

    public void SetSliders() {
        var noiseFilterValue = noiseFilter.value;
        var toneThreshValue = toneThresh.value;
        
        SceneHandler.NoiseFilter = noiseFilterValue;
        SceneHandler.ToneThresh = toneThreshValue;
        
        _frequencyHandler.NoiseFilter = noiseFilterValue;
        _frequencyHandler.OvertoneThresh = toneThreshValue;
    }

    private void CreateMicDropdown() {
        microphoneDropdownMenu.ClearOptions();
        microphoneDropdownMenu.AddOptions(Microphone.devices.ToList());
    }

    private void CreateRangeDropdown() {
        rangeDropdownMenu.ClearOptions();
        List<string> options = new List<string>();

        foreach (string range in ranges.ToStringArray()) {
            options.Add(range);
        }

        rangeDropdownMenu.AddOptions(options);
    }


    public void BackToMenu() {
        SceneHandler.NoiseFilter = noiseFilter.value;
        SceneHandler.ToneThresh = toneThresh.value;

        SceneHandler.ChangeScene("MainMenu");
    }

    public void MicValue() {
        SceneHandler.InputDevice = microphoneDropdownMenu.options[microphoneDropdownMenu.value].text;
    }

    public void RangeValue() {
        SceneHandler.SetRange(ranges.ToArray()[rangeDropdownMenu.value],
            ranges.ToStringArray()[rangeDropdownMenu.value]);
    }

    public void RangeValueOnExit() {
        if (SceneHandler.GetRange() == null) {
            RangeValue();
        }
    }
    
}