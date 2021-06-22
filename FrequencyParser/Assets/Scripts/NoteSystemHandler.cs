using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using UnityEngine;
using UnityEngine.Analytics;
using Random = System.Random;

public class NoteSystemHandler : MonoBehaviour {
    public List<GameObject> noteAreasPlayer, noteAreasEngine;
    private List<OnehotNote> _noteAreaScriptsPlayer, _noteAreaScriptsEngine;
    public Sprite notenKopf;
    public Sprite notenKopfFlat;
    public Sprite notenKopfSharp;
    
    private OnehotNote _lastEngineNote, _lastPlayerNote, _nullNote;
    private Random _random = new Random();
    private FrequencyUtil _freqUtil = new FrequencyUtil();
    void Start() {
        _nullNote = null;
        _lastEngineNote = _nullNote;
        _lastPlayerNote = _nullNote;
        
        _noteAreaScriptsPlayer = new List<OnehotNote>();
        _noteAreaScriptsEngine = new List<OnehotNote>();
        foreach (GameObject note in noteAreasPlayer) {
            var script = note.GetComponentInChildren<OnehotNote>();
            _noteAreaScriptsPlayer.Add(script);
            script.EnableClickIn = true;
        }

        foreach (GameObject note in noteAreasEngine) {
            var script = note.GetComponentInChildren<OnehotNote>();
            _noteAreaScriptsEngine.Add(script);
            script.EnableClickIn = false;
        }
    }

    public void StayOn(OnehotNote onehot, bool stay) {
        foreach (OnehotNote note in _noteAreaScriptsPlayer) {
            if (onehot == note) continue;
            note.StayOff = stay;
        }
    }

    public void SetEngineTone(int noteVal) {
        if (_lastEngineNote != _nullNote) _lastEngineNote.RenderNote(false);
        var noteValGanz = _freqUtil.CheckForHalbton(noteVal);
        var note = GetActiveEngineNoteViaValue(noteValGanz);
        
        if (noteVal > noteValGanz) {
            note.AddSharp();
        } else if (noteVal < noteValGanz) {
            note.AddFlat();
        }

        note.RenderNote(true);
        _lastEngineNote = note;
    }

    public void SetPlayerTone(int noteVal) {
        var noteValGanz = _freqUtil.CheckForHalbton(noteVal);
        var note = GetActivePlayerNoteViaName(noteValGanz);

        if (note == _nullNote) {
            return;
        }
        
        if (_lastPlayerNote == _nullNote) {
            _lastPlayerNote = note;
        }

        if (_lastPlayerNote == note) return; 
        
        _lastPlayerNote.RenderNote(false);

        if (noteVal > noteValGanz) {
            note.AddSharp();   
        } else if (noteVal < noteValGanz) {
            note.AddFlat();
        }
        note.RenderNote(true);
        _lastPlayerNote = note;
        
    }

    public string GetChosenNote() {
        var note = GetActivePlayerNoteViaMouse();
        if (note == _nullNote) {
            return "none";
        }

        return note.name;
    }

    private OnehotNote GetActivePlayerNoteViaMouse() {
        foreach (var note in _noteAreaScriptsPlayer.Where(note => note.IsVisible)) {
            return note;
        }

        return _nullNote;
    }

    private OnehotNote GetActivePlayerNoteViaName(int noteVal) {
        var noteName = Enum.GetName(typeof(GleichstufigFreq), noteVal);
        
        foreach (var note in _noteAreaScriptsPlayer.Where(note => note.gameObject.name == noteName)) {
            return note;
        }

        return _nullNote;
    }

    private OnehotNote GetActiveEngineNoteViaValue(int noteVal) {
        var noteName = Enum.GetName(typeof(GleichstufigFreq), noteVal);

        foreach (var note in _noteAreaScriptsEngine.Where(note => note.gameObject.name == noteName)) {
            return note;
        }

        return _nullNote;
    }

    public void SetMouseControl(bool control) {
        foreach (var note in _noteAreaScriptsPlayer) {
            note.EnableClickIn = control;
        }
    }
}