using System;
using System.Collections;
using DataStructures;
using DatastructuresUtility;
using UnityEngine;

public class IntervallGameModule_GL : BaseGameLogic {

    void Update() {
        HandleExerciseHolding(GetInput());
    }

    protected override void NextExer() {
        _exerciseCompleted = false;
        _uiHandler.FeedbackField = "";
        
        // Check ob alle Module absolviert wurden
        if (_exerCounter >= _exercises.exercisesIntervall.Length) {
            _uiHandler.SetCustomOutput(_freqUtil.GetNearestNoteFromFreq(_currentExer), 0, 0, "Modul erfolgreich abgeschlossen!");
            return;
        }

        var currentExercise =  _exercises.exercisesIntervall[_exerCounter];
        
        // Set Intervall
        if (!currentExercise.intervall.Equals("skip")) {
            intervall = (int) Enum.Parse(typeof(GleichstufigeIntervalleCent), currentExercise.intervall);    
        }
        else {
            _exerCounter++;
            _exerciseCompleted = true;
            _uiHandler.FeedbackField = "Zum fortfahren auf 'Weiter' klicken.";
            
            intervall = 0;
            return;
        }
        // Set Grundton
        if (currentExercise.grundton.Equals("random")) {
            GetGrundton();
        }
        else {
            grundton = (int) Enum.Parse(typeof(GleichstufigFreq), currentExercise.grundton);
        }
        
        // Init die Aufgabe
        _currentExer = _freqUtil.GenExerIntervall(_freqUtil.GetFrequency(grundton), intervall, _inputHandler.AscDesc);
        _uiHandler.SetCustomOutput(_freqUtil.GetNearestNoteFromFreq(_currentExer), grundton, intervall, currentExercise.text);
        _generator.PlayFreq(grundton);

    }
    
    public override void NextExerButton() {
        if (_exerciseCompleted) {
            NextExer();
        }
        else if (_exerCounter >= _exercises.exercisesIntervall.Length) {
            SceneHandler.ChangeScene("MainMenu");
        }
        else {
            _uiHandler.FeedbackField = "Löse zunächst die Aufgabe.";
        }
    }
    

}