using System;
using DataStructures;
using UnityEngine;

public class InputHandler : MonoBehaviour{
    private FrequencyHandler _frequencyHandler;
    private NoteSystemHandler _systemHandler;
    private GameUI _uiHandler;
    
    private float _currentFreq;
    private string _currentNoteMic;
    private int _staticIntervall;
    
    private int _currentNoteNormMouse;
    private string _currentNoteMouse;
    private bool _ascDesc;
    private int _staticGrundton;
    public bool _realGame;

    public int StaticGrundton => _staticGrundton;
    public bool AscDesc => _ascDesc;
    public float CurrentFreq => _currentFreq;
    public string CurrentNoteMic => _currentNoteMic;
    public string CurrentNoteMouse => _currentNoteMouse;
    public int CurrentNoteNormMouse => _currentNoteNormMouse;
    
    

    private void Start() {
        _frequencyHandler = FindObjectOfType<FrequencyHandler>();
        _systemHandler = FindObjectOfType<NoteSystemHandler>();
        _uiHandler = FindObjectOfType<GameUI>();
        FixedUpdate();
    }

    private void FixedUpdate() {
        _currentFreq = _frequencyHandler.GetHz();
        _currentNoteMic = _frequencyHandler.GetNote();
        
        if (_realGame) {
            _currentNoteNormMouse = _systemHandler.GetChosenNote();
            _currentNoteMouse = _currentNoteNormMouse != 0 ? Enum.GetName(typeof(GleichstufigFreq), _currentNoteNormMouse) : "none";
            _ascDesc = _uiHandler.AscDesc;
            _staticGrundton = _uiHandler.GetStaticGrundton();            
        }

    }
}