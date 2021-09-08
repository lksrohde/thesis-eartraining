using System;
using DataStructures;
using UnityEditor;
using UnityEngine;

public class GameLogic : BaseGameLogic {

    void Update() {
        HandleExerciseHolding(GetInput());
    }

    protected override void NextExer() {
        _exerciseCompleted = false;
        _uiHandler.FeedbackField = "";
        intervall = _freqUtil.GetRandomIntervallInCent();
        GetGrundton();

        _currentExer = _freqUtil.GenExerIntervall(_freqUtil.GetFrequency(grundton), intervall, _inputHandler.AscDesc);
        _uiHandler.UpdateOutput(_freqUtil.GetNearestNoteFromFreq(_currentExer), grundton, intervall);
        _generator.PlayFreq(grundton);
    }

    public override void NextExerButton() {
        if (_exerciseCompleted) {
            NextExer();
        }
        else {
            _uiHandler.FeedbackField = "Löse zunächst die Aufgabe.";
        }
    }
}