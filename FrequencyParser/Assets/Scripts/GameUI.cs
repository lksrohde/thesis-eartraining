using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    private bool _toggledGrundton;
    private bool _toggledSinging;
    private bool _toggledFlat;

    private Tuple<GleichstufigFreq, GleichstufigFreq> _range;
    private VocalRanges ranges = new VocalRanges();
    
    public NoteSystemHandler noteSystemHandler;
    public Dropdown customGrundtonDropdown;
    public Toggle customGrundtonToggle;
    public Toggle enableSinging;
    public TMP_Text exerOut;
    public Canvas optionScreen;
    public Toggle makeItFlat;
    void Start() {
        _range = SceneHandler.GetRange();
        
        if (_range == null) {
            _range = ranges.Bariton;
        }
        
        ToggleSinging();
        ToggleGrundton();
        ShowOptions();
    }
    
    public void ShowOptions() {
        var show = optionScreen.gameObject.activeSelf;
        optionScreen.gameObject.SetActive(!show);
    }
    
    public void ToggleFlat() {
        _toggledFlat = makeItFlat.isOn;
    }
    
    public void ToggleGrundton() {
        _toggledGrundton = customGrundtonToggle.isOn;
        if (!_toggledGrundton) {
            customGrundtonDropdown.gameObject.SetActive(false);
            makeItFlat.gameObject.SetActive(false);
        }
        else {
            customGrundtonDropdown.gameObject.SetActive(true);
            makeItFlat.gameObject.SetActive(true);
            CreateGrundtonDropdown();
        }
    }

    public void ToggleSinging() {
        _toggledSinging = enableSinging.isOn;
        noteSystemHandler.SetMouseControl(!_toggledSinging);
    }

    private void CreateGrundtonDropdown() {
        customGrundtonDropdown.ClearOptions();
        List<string> tones = new List<string>();

        foreach (string tone in ranges.GetRangeNames(_range)) {
            tones.Add(tone);
        }

        customGrundtonDropdown.AddOptions(tones);
    }

    public void UpdateOutput(int _currentExer, int currentGrundton, int currentIntervall) {
        noteSystemHandler.SetEngineTone(currentGrundton);
        exerOut.text = "Singe folgendes Intervall: " + (ReineIntervalleCent) currentIntervall;
    }

    public void GoToMainMenu() {
        SceneHandler.ChangeScene("InfiniteTest", "MainMenu");
    }

    

    public int GetStaticGrundton() {
        return ranges.GetRangeValues(_range)[customGrundtonDropdown.value];
    }
    
    public bool ToggledGrundton => _toggledGrundton;
    public bool ToggledSinging => _toggledSinging;
    public bool ToggledFlat => _toggledFlat;
    
}
