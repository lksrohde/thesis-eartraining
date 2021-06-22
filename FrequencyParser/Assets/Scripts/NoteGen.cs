using System;
using System.Collections;
using UnityEngine;

public class NoteGen : MonoBehaviour {
    private FrequencyUtil _freqUtil;

    private int _sampleFreq;
    private AudioSource _toneGen;

    private void Awake() {
        _toneGen = GetComponentInParent<AudioSource>();
        _freqUtil = new FrequencyUtil();
        _sampleFreq = 44100;
    }

    public AudioClip GetSinNoteFromFreq(float freq) {
        float[] samples = new float[_sampleFreq];
        freq = _freqUtil.GetFrequency(freq);

        for (int i = 0; i < samples.Length; i++) {
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * freq / _sampleFreq);
        }

        AudioClip tone = AudioClip.Create("Note", samples.Length, 1, _sampleFreq, false);
        tone.SetData(samples, 0);

        return tone;
    }

    public AudioSource PlayFreq(float freq) {
        _toneGen.clip = GetSinNoteFromFreq(freq);
        _toneGen.Play();

        return _toneGen;
    }
    
}