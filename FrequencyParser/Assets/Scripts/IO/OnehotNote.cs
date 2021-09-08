using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures;
using DatastructuresUtility;
using UnityEngine;

public class OnehotNote : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private Sprite _notenKopf;
    private NoteSystemHandler _systemHandler;
    private BoxCollider _collider;
    
    private bool _stayOn;
    public bool StayOff { get; set; }
    public bool EnableClickIn { get; set; }
    public bool IsVisible { get; private set; }
    
    private GleichstufigFreq _toneVal;
    private GleichstufigFreq _halfToneLowerVal;
    private GleichstufigFreq _halfToneUpperVal;
    
    private Vector2 _standSize;
    private Vector2 _collSize;
    
    public GameObject[] balken;
    private GameUI _uiHandler;
    
    private void Awake() {
        _uiHandler = FindObjectOfType<GameUI>();
        
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        /*_spriteRenderer.rendererPriority = 1;
        
        _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        */
        _systemHandler = GetComponentInParent<NoteSystemHandler>();
        _collider = GetComponent<BoxCollider>();
        
        _collSize = _collider.size;
        _standSize = new Vector2(_collSize.y * 2.5f, _collSize.y * 2);

        _notenKopf = _systemHandler.notenKopf;
        RenderBalken(false);
    }

    private void OnMouseEnter() {
        if (_stayOn || StayOff || !EnableClickIn) return;
        if (_systemHandler.PickedNoteIsHalftone) {
            if (!_uiHandler.AscDesc) {
                AddSharp();
            }
            else {
                AddFlat();
            }
        } 
        else {
            ResetSprite();
        }

        RenderNote(true);
    }

    private void OnMouseExit() {
        if (_stayOn || StayOff || !EnableClickIn) return;
        RenderNote(false);
    }

    private void OnMouseDown() {
        if (StayOff || !EnableClickIn) return;
        _stayOn = !_stayOn;
        _systemHandler.StayOn(this, _stayOn);
    }

    public void RenderNote(bool on) {
        if (on) {
            IsVisible = true;
            _spriteRenderer.sprite = _notenKopf;
            _spriteRenderer.size = _standSize;
            RenderBalken(true);

            //_spriteRenderer.rendererPriority = 1;
            gameObject.layer = 1;
        }
        else {
            IsVisible = false;
            _spriteRenderer.sprite = null;
            _notenKopf = _systemHandler.notenKopf;
            RenderBalken(false);
            _standSize = new Vector2(_collSize.y * 2.5f, _collSize.y * 2);
        }
    }

    private void RenderBalken(bool render) {
        if (balken.Length != 0) {
            foreach (var balk in balken) {
                balk.SetActive(render); 
            }
        }
    }

    public void ResetSprite() {
        _notenKopf = _systemHandler.notenKopf;
        _standSize = new Vector2(_collSize.y * 2.5f, _collSize.y * 2);
    }

    public void AddSharp() {
        _notenKopf = _systemHandler.notenKopfSharp;
        _standSize = new Vector2(_collSize.y * 3.5f, _collSize.y * 2);
    }

    public void AddFlat() {
        _notenKopf = _systemHandler.notenKopfFlat;
        _standSize = new Vector2(_collSize.y * 3.5f, _collSize.y * 2);
    }
    
    public GleichstufigFreq ToneVal {
        get => _toneVal;
        set => _toneVal = value;
    }

    public GleichstufigFreq HalfToneLowerVal {
        get => _halfToneLowerVal;
        set => _halfToneLowerVal = value;
    }

    public GleichstufigFreq HalfToneUpperVal {
        get => _halfToneUpperVal;
        set => _halfToneUpperVal = value;
    }
}