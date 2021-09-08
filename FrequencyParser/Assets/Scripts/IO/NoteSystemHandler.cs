using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using DatastructuresUtility;
using IO;
using UnityEngine;
using Random = System.Random;

public class NoteSystemHandler : MonoBehaviour {
    private FrequencyUtil _freqUtil = new FrequencyUtil();
    private FrequencyHandler _frequencyHandler;
    private BaseGameUI _uiHandler;

    private OnehotNote _lastEngineNote, _lastPlayerNote, _nullNote, _pickedNote;
    private List<OnehotNote> _noteAreaScriptsPlayer, _noteAreaScriptsEngine;

    private bool _pickedNoteIsHalftone;
    private Random _random = new Random();
    public List<GameObject> noteAreasPlayer, noteAreasEngine;
    public Sprite notenKopf;
    public Sprite notenKopfFlat;
    public Sprite notenKopfSharp;

    void Start() {
        _nullNote = null;
        _pickedNote = _nullNote;
        _lastEngineNote = _nullNote;
        _lastPlayerNote = _nullNote;
        
        _uiHandler = FindObjectOfType<GameUI>();
        if (_uiHandler == null) {
            _uiHandler = FindObjectOfType<BaseGameUI>();
        }
        
        _frequencyHandler = FindObjectOfType<FrequencyHandler>();

        GleichstufigFreq[] freqs = (GleichstufigFreq[]) Enum.GetValues(typeof(GleichstufigFreq));

        _noteAreaScriptsPlayer = new List<OnehotNote>();
        _noteAreaScriptsEngine = new List<OnehotNote>();

        foreach (GameObject note in noteAreasPlayer) {
            var script = note.GetComponentInChildren<OnehotNote>();

            script.ToneVal = (GleichstufigFreq) Enum.Parse(typeof(GleichstufigFreq), note.name);
            script.HalfToneLowerVal = GetIndexBelow(freqs, script.ToneVal);
            script.HalfToneUpperVal = (GleichstufigFreq) _freqUtil.GetNearestNoteFromFreq(
                _freqUtil.GenExerIntervall(_freqUtil.GetNormalized((int) script.ToneVal),
                    (float) GleichstufigeIntervalleCent.KLEINE_SEKUNDE,
                    true));

            _noteAreaScriptsPlayer.Add(script);
            script.EnableClickIn = true;
        }

        foreach (GameObject note in noteAreasEngine) {
            var script = note.GetComponentInChildren<OnehotNote>();

            script.ToneVal = (GleichstufigFreq) Enum.Parse(typeof(GleichstufigFreq), note.name);
            script.HalfToneLowerVal = GetIndexBelow(freqs, script.ToneVal);
            script.HalfToneUpperVal = (GleichstufigFreq) _freqUtil.GetNearestNoteFromFreq(
                _freqUtil.GenExerIntervall(_freqUtil.GetNormalized((int) script.ToneVal),
                    (float) GleichstufigeIntervalleCent.KLEINE_SEKUNDE,
                    true));

            _noteAreaScriptsEngine.Add(script);
            script.EnableClickIn = false;
        }

        SetMouseControl(false);
    }

    private GleichstufigFreq GetIndexBelow(GleichstufigFreq[] freqs, GleichstufigFreq element) {
        for (int i = 0; i < freqs.Length; i++) {
            if (freqs[i] == element) {
                return freqs[i - 1];
            }
        }

        return GleichstufigFreq.Dis_Es;
    }

    public void StayOn(OnehotNote onehot, bool stay) {
        if (stay) {
            _pickedNote = onehot;
        }
        else {
            _pickedNote = _nullNote;
        }

        foreach (OnehotNote note in _noteAreaScriptsPlayer) {
            if (onehot == note) continue;
            note.StayOff = stay;
        }
    }

    public void SetEngineTone(int noteVal) {
        if (_lastEngineNote != _nullNote) _lastEngineNote.RenderNote(false);
        var noteValGanz = _freqUtil.CheckForHalbton(noteVal);
        var note = GetEngineNoteViaValue(noteValGanz);

        if (noteVal != noteValGanz) {
            note.AddFlat();
        }
        else if (noteVal == noteValGanz) {
            note.ResetSprite();
        }

        note.RenderNote(true);
        _lastEngineNote = note;
    }

    public void SetPlayerTone(int noteVal) {
        var noteValGanz = _freqUtil.CheckForHalbton(noteVal);
        var note = GetPlayerNoteViaValue(noteValGanz);

        if (note == _nullNote) {
            return;
        }

        if (_lastPlayerNote == _nullNote) {
            _lastPlayerNote = note;
        }

        if (_lastPlayerNote == note) return;

        _lastPlayerNote.RenderNote(false);

        if (noteVal != noteValGanz) {
            if (!_uiHandler.AscDesc) {
                note = GetGanztonNachUnten(noteValGanz);

                note.AddSharp();
            }
            else {
                note.AddFlat();
            }
        }
        else if (noteVal == noteValGanz) {
            note.ResetSprite();
        }

        note.RenderNote(true);
        _lastPlayerNote = note;
    }

    private OnehotNote GetGanztonNachUnten(int note) {
        var tempNote = _freqUtil.GetNearestNoteFromFreq(_freqUtil.GenExerIntervall(_freqUtil.GetFrequency(note),
            (float) GleichstufigeIntervalleCent.GROßE_SEKUNDE, false));
        OnehotNote newNote = GetPlayerNoteViaValue(tempNote);
        if (newNote == _nullNote) {
            return GetPlayerNoteViaValue((int) GleichstufigFreq.E);
        }

        return newNote;
    }

    public int GetChosenNote() {
        var note = _pickedNote;

        if (note == _nullNote) {
            return 0;
        }

        if (_pickedNoteIsHalftone) {
            if (_uiHandler.AscDesc) {
                return (int) note.HalfToneLowerVal;
            }

            return (int) note.HalfToneUpperVal;
        }

        note.ResetSprite();
        return (int) note.ToneVal;
    }

    private OnehotNote GetPlayerNoteViaValue(int noteVal) {
        var noteName = Enum.GetName(typeof(GleichstufigFreq), noteVal);

        foreach (var note in _noteAreaScriptsPlayer.Where(note => note.gameObject.name == noteName)) {
            return note;
        }

        return _nullNote;
    }

    private OnehotNote GetEngineNoteViaValue(int noteVal) {
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

    public bool PickedNoteIsHalftone {
        get => _pickedNoteIsHalftone;
        set => _pickedNoteIsHalftone = value;
    }
}