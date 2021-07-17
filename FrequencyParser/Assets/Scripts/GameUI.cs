using System;
using System.Collections.Generic;
using DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseGameUI {
    public Canvas optionScreen;
    public Dropdown customGrundtonDropdown;
    public Toggle customGrundtonToggle;
    public Toggle enableSinging;
    public Toggle makeItFlat;
    public TMP_Text exerOut;
    public Dropdown ascDescDropdown;
    public TextMeshProUGUI feedbackText;

    void Start() {
        _range = SceneHandler.GetRange();
        _inputHandler = FindObjectOfType<InputHandler>();
        _noteSystemHandler = FindObjectOfType<NoteSystemHandler>();
        _frequencyUtil = new FrequencyUtil(SceneHandler.GetRange());
        
        if (_range == null) {
            _range = ranges.Bariton;
        }

        _rangeVals = ranges.GetRangeValues(_range);
        ToggleSinging();
        AscDescVal();
        ToggleGrundton();
        ToggleFlat();
        ShowOptions();
    }

    protected override void Update() {
        if (_currentGrundton != 0) {
            _noteSystemHandler.SetEngineTone(_currentGrundton);
        }
        feedbackText.text = _feedbackField;
        if (!isModule) {
            if (_toggledSinging) {
                _noteSystemHandler.SetPlayerTone(_frequencyUtil.GetNearestNoteFromFreq(_inputHandler.CurrentFreq));
                
                exerOut.text = "Singe folgendes Intervall: " + NormaliseEnumName(
                                   ((GleichstufigeIntervalleCent) _currentIntervall).ToString());
            }
            else {
                exerOut.text = "Vervollständige folgendes Intervall: " + NormaliseEnumName(
                                   ((GleichstufigeIntervalleCent) _currentIntervall).ToString());
            }
        }
        else {
            if (_toggledSinging) {
                _noteSystemHandler.SetPlayerTone(_frequencyUtil.GetNearestNoteFromFreq(_inputHandler.CurrentFreq));
                exerOut.text = _customOutput;
            }
            else {
                exerOut.text = _customOutput;
            }
        }
    }

    public int GetStaticGrundton() {
        return _rangeVals[customGrundtonDropdown.value];
    }

    public void AscDescVal() {
        _ascDesc = ascDescDropdown.value.ToString().Equals("0");
    }

    public void ShowOptions() {
        var show = optionScreen.gameObject.activeSelf;
        optionScreen.gameObject.SetActive(!show);
    }
    
    public void ToggleFlat() {
        _toggledFlat = makeItFlat.isOn;
        _noteSystemHandler.PickedNoteIsHalftone = _toggledFlat;
    }
    
    public void ToggleGrundton() {
        _toggledGrundton = customGrundtonToggle.isOn;
        if (!_toggledGrundton) {
            customGrundtonDropdown.gameObject.SetActive(false);
        }
        else {
            customGrundtonDropdown.gameObject.SetActive(true);
            CreateGrundtonDropdown();
        }
    }

    public void ToggleSinging() {
        _toggledSinging = enableSinging.isOn;
        _noteSystemHandler.SetMouseControl(!_toggledSinging);
        makeItFlat.gameObject.SetActive(!_toggledSinging);

    }

    private void CreateGrundtonDropdown() {
        customGrundtonDropdown.ClearOptions();
        List<string> tones = new List<string>();

        foreach (string tone in ranges.GetRangeNames(_range)) {
            tones.Add(tone);
        }

        customGrundtonDropdown.AddOptions(tones);
    }
    
    
    public void GoToTheory() {
        SceneHandler.ChangeScene("TheoryBook");
    }
    
    
}
