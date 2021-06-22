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

    private VocalRanges ranges = new VocalRanges();

    void Start() {
        CreateMicDropdown();
        CreateRangeDropdown();
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
        SceneHandler.SetNoiseFilter(noiseFilter.value);
        SceneHandler.SetToneThresh(toneThresh.value);

        SceneHandler.ChangeScene("Options", "MainMenu");
    }

    public void MicValue() {
        SceneHandler.SetInput(microphoneDropdownMenu.options[microphoneDropdownMenu.value].text);
    }

    public void RangeValue() {
        SceneHandler.SetRange(ranges.ToArray()[rangeDropdownMenu.value],
            ranges.ToStringArray()[rangeDropdownMenu.value]);
    }
}