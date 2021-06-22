using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnehotNote : MonoBehaviour {
    private SpriteRenderer renderer;
    private Sprite notenKopf;
    private NoteSystemHandler _systemHandler;
    private BoxCollider _collider;
    private bool _stayOn;
    private Vector2 standSize;
    private Vector2 collSize;
    private bool _isSharp;
    private bool _isFlat;
    private void Awake() {
        renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.rendererPriority = 1;
        
        renderer.drawMode = SpriteDrawMode.Sliced;
        _systemHandler = GetComponentInParent<NoteSystemHandler>();
        _collider = GetComponent<BoxCollider>();

        collSize = _collider.size;
        standSize = new Vector2(collSize.y * 2.5f, collSize.y * 2);    
        
        notenKopf = _systemHandler.notenKopf;
    }

    private void OnMouseEnter() {
        if (_stayOn || StayOff || !EnableClickIn) return;
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
            renderer.sprite = notenKopf;
            renderer.size = standSize;
            renderer.rendererPriority = 1;
            gameObject.layer = 1;
        }
        else {
            IsVisible = false;
            renderer.sprite = null;
            notenKopf = _systemHandler.notenKopf;
            standSize = new Vector2(collSize.y * 2.5f, collSize.y * 2);    

        }
    }

    public void AddSharp() {
        notenKopf = _systemHandler.notenKopfSharp;
        standSize = new Vector2(collSize.y * 3.5f, collSize.y * 2);
        
    }
    
    public void AddFlat() {
        notenKopf = _systemHandler.notenKopfFlat;
        standSize = new Vector2(collSize.y * 3.5f, collSize.y * 2);
    }
    public bool StayOff { get; set; }

    public bool EnableClickIn { get; set; }

    public bool IsVisible { get; private set; }
}
