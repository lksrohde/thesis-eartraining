using System;
using DatastructuresUtility;
using UnityEngine;
using Random = System.Random;

namespace DataStructures {
    public class BaseGameLogic : MonoBehaviour {
        // Base Values
        protected Tuple<GleichstufigFreq, GleichstufigFreq> _range;
        protected bool _exerciseAnswered;
        protected float _currentExer = 4;
        protected Exercises _exercises;
        protected int _exerCounter;
        protected int grundton, intervall;

        // Time Handling
        //protected float _audioStarted;
        protected float _firstTimeRight;
        protected float _lastTimeRight;
        protected bool _holdingTone;
        protected bool _exerciseCompleted;

        // Public Props
        public float timeHoldingToCount;
        public TextAsset exercisesJson;

        // Classes
        protected VocalRanges ranges = new VocalRanges();
        protected FrequencyUtil _freqUtil = new FrequencyUtil(SceneHandler.GetRange());
        protected NoteGen _generator;
        protected GameUI _uiHandler;
        protected InputHandler _inputHandler;
        protected ExerJsonLoader _jsonLoader;
        
        void Start() {
            _exerCounter = 0;
            _inputHandler = FindObjectOfType<InputHandler>();
            _uiHandler = FindObjectOfType<GameUI>();
            _generator = FindObjectOfType<NoteGen>();
            _jsonLoader = new ExerJsonLoader(exercisesJson);

            _exercises = _jsonLoader.LoadExercises();
        
            _exerciseAnswered = false;
            _range = SceneHandler.GetRange();

            if (_range == null) {
                _range = ranges.Bariton;
                SceneHandler.SetRange(_range, "Bariton");
            }

            _freqUtil = new FrequencyUtil(_range);

            HandleExerciseHolding("init");
            NextExer();
        }

        
        protected void HandleExerciseHolding(string currentNote) {
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

            if (_lastTimeRight - _firstTimeRight > timeHoldingToCount && _exerciseAnswered && !_exerciseCompleted) {
                _exerCounter++;
                _exerciseCompleted = true;

                _uiHandler.FeedbackField = "Richtig!";
            }
        }

        protected void GetGrundton() {
            if (_uiHandler.ToggledGrundton) {
                grundton = _inputHandler.StaticGrundton;
            }
            else {
                grundton = _freqUtil.GetRandomGrundton(_uiHandler.AscDesc);
            }
        }

        protected string GetInput() {
            string currentNote = "none";

            if (_uiHandler.ToggledSinging) {
                currentNote = _inputHandler.CurrentNoteMic;
            }
            else {
                currentNote = _inputHandler.CurrentNoteMouse;
            }

            return currentNote;
        }

        protected bool CheckExer(string currentNote) {
            if (currentNote == null) {
                return false;
            }
            if (currentNote.Equals(_freqUtil.GetNoteNameFromFreq(_currentExer))) {
                return true;
            }

            return currentNote == "init";
        }

        protected virtual void NextExer() {}

        public virtual void NextExerButton() {}

        public void Skip() {
            _exerCounter++;
            NextExer();
        }

        public void RefreshInputVals() {
            GetGrundton();
            _generator.PlayFreq(grundton);
            _currentExer =
                _freqUtil.GenExerIntervall(_freqUtil.GetFrequency(grundton), intervall, _uiHandler.AscDesc);
            _uiHandler.UpdateOutput(_freqUtil.GetNearestNoteFromFreq(_currentExer), grundton, intervall);
        }
        
    }
}