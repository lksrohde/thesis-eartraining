using System;
using DataStructures;
using DatastructuresUtility;
using UnityEngine;

namespace IO {
    public class BaseGameUI : MonoBehaviour {
        protected bool _toggledGrundton;
        protected bool _toggledSinging;
        protected bool _toggledFlat;
        protected bool _ascDesc;
        public bool isModule;

        protected Tuple<GleichstufigFreq, GleichstufigFreq> _range;
        protected VocalRanges ranges = new VocalRanges();
        protected int[] _rangeVals;    
        protected NoteSystemHandler _noteSystemHandler;
        protected FrequencyUtil _frequencyUtil;
        protected InputHandler _inputHandler;
        
        protected int _currentGrundton, _currentIntervall, _currentExer;
        protected string _customOutput, _feedbackField;
        protected virtual void Start() {
            _range = SceneHandler.GetRange();
            _inputHandler = FindObjectOfType<InputHandler>();
            _noteSystemHandler = FindObjectOfType<NoteSystemHandler>();
            _frequencyUtil = new FrequencyUtil(SceneHandler.GetRange());
        }

        protected virtual void Update() {
            _noteSystemHandler.SetPlayerTone(_frequencyUtil.GetNearestNoteFromFreq(_inputHandler.CurrentFreq));
        }

        public void UpdateOutput(int currentExer, int currentGrundton, int currentIntervall) {
            _currentExer = currentExer;
            _currentGrundton = currentGrundton;
            _currentIntervall = currentIntervall;
        
        }

        public void SetCustomOutput(int currentExer, int currentGrundton, int currentIntervall, string customText) {
            UpdateOutput(currentExer, currentGrundton, currentIntervall);
            _customOutput = customText;
        }

        public void GoToMainMenu() {
            SceneHandler.ChangeScene("MainMenu");
        }

        protected string NormaliseEnumName(string name) {
            string newName;
            name = name.ToLower();

            if (name.Contains("_")) {
                var split = name.Split('_');
                char[] uppedFirst = split[1].ToCharArray();
                uppedFirst[0] = Char.ToUpper(uppedFirst[0]);
                
                newName = split[0] + " " + new string(uppedFirst);
            }
            else {
                char[] uppedFirst = name.ToCharArray();
                uppedFirst[0] = Char.ToUpper(uppedFirst[0]);
                newName = new string(uppedFirst);
            }

            return newName;
        }
        
        public bool ToggledGrundton => _toggledGrundton;
        public bool ToggledSinging => _toggledSinging;
        public bool ToggledFlat => _toggledFlat;
        public bool AscDesc => _ascDesc;
        public string FeedbackField {
            get => _feedbackField;
            set => _feedbackField = value;
        }
    }
}