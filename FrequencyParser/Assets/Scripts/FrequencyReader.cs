using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrequencyReader : MonoBehaviour {
    private AudioClip _microphoneInput;
    public AudioSource playback;
    public AudioClip micClip;
    public string deviceName;

    private int _minFreq;
    private int _maxFreq;

    private int _sampleRate;


    void Start() {
        if (deviceName == "") {
            deviceName = SceneHandler.InputDevice;
            if (deviceName == null) {
                deviceName = Microphone.devices[0];
            }
        }

        Microphone.GetDeviceCaps(deviceName, out _minFreq, out _maxFreq);
        Debug.Log("MinFreq: " + _minFreq + " MaxFreq: " + _maxFreq);
        Debug.Log(deviceName);

        //Start Microphone Recording
        // Loop = true damit kontinuierlich aufgenommen wird
        _sampleRate = 44100;

        micClip = Microphone.Start(deviceName, true, 1, _sampleRate);

        playback.clip = micClip;

        // loop =  true, damit der clip nicht endet
        playback.loop = true;

        // Warte auf Samples -> wenn Samples ankommen startet das Audio
        // so erziele ich die niedrigst mögliche Latenz.
        while (Microphone.GetPosition(deviceName) < 1) {
            //print("Waiting for Mic");
        }


        playback.Play();
    }
    
    public int SampleRate => _sampleRate;

}