using System;
using System.Collections.Generic;
using DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = System.Random;

public class IntervallExerciseGen : MonoBehaviour {
    private Tuple<GleichstufigFreq, GleichstufigFreq> _range;
    private float _completionTime;
    private bool _exerciseAnswered;
    private float _grundtonNorm;
    private bool _waitForNextExer;
    private float _currentExer = 4;
    private AudioSource _toneGen;
    private bool _enableSinging;
    
    private float _firstTimeRight;
    private float _lastTimeRight;
    private bool _holdingTone;
    public float _timeHoldingToCount = 1;
    public bool debug;
    
    private VocalRanges ranges = new VocalRanges();
    public NoteGen _generator;
    private FrequencyUtil _freqUtil;
    public FrequencyHandler frequencyHandler;
    public GameUI uiHandler;
    
    void Start() {
        _exerciseAnswered = false;
        _range = SceneHandler.GetRange();
        
        if (_range == null) {
            _range = ranges.Bariton;
            SceneHandler.SetRange(_range, "Bass");
        }
        Debug.Log("SH: " + _range);
        _freqUtil = new FrequencyUtil(_range);
        
        HandleExerciseHolding("init");
    }

    void Update() {
        
        var currentNote = frequencyHandler.GetNote();

        if (!uiHandler.ToggledGrundton) {
            _grundtonNorm = _freqUtil.GetRandomGrundton();
        }
        else {
            _grundtonNorm = uiHandler.GetStaticGrundton();
        }

        HandleExerciseHolding(currentNote);
    }


    private void HandleExerciseHolding(string currentNote) {
        _exerciseAnswered = CheckExer(currentNote);
        
        if (_exerciseAnswered) {
            _lastTimeRight = Time.fixedTime;
            
            if (!_holdingTone) {
                _firstTimeRight = Time.fixedTime;
                _holdingTone = true;    
            }
        }
        else {
            _holdingTone = false;
        }

        if (_lastTimeRight - _firstTimeRight > _timeHoldingToCount && _exerciseAnswered) {
            NewExer();
        }
        
    }
    public void NewExer() {
        int intervall = _freqUtil.GetRandomIntervallInCent();
        _currentExer = _freqUtil.GenExerIntervall(_grundtonNorm / 100, intervall, true);
            
        if (debug) {
            uiHandler.UpdateOutputDebug(_freqUtil.GetNearestNoteFromFreq(_currentExer), (int) _grundtonNorm, intervall);
        }
        else {
            uiHandler.UpdateOutput(_freqUtil.GetNearestNoteFromFreq(_currentExer), (int) _grundtonNorm, intervall);
        }
        _toneGen = _generator.PlayFreq(_grundtonNorm);

    }

    bool CheckExer(string currentNote) {
        if (currentNote.Equals(_freqUtil.GetNoteNameFromFreq(_currentExer))) {
            return true;
        }

        if (currentNote == "init") {
            return true;
        }

        return false;
    }

    
}