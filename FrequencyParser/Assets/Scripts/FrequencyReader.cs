using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrequencyReader : MonoBehaviour
{
    AudioClip microphoneInput;
    public AudioSource playback;
    
    public string deviceName;
    
    void Start() {
        if (deviceName == "") {
            deviceName = Microphone.devices[0];    
        }
        
        print(deviceName);
        
        //Start Microphone Recording
        playback.clip = Microphone.Start(deviceName, true, 1, 44100);
        playback.loop = true;
        
        //Warte bis 10ms in samples vergangen sind -> 44100 / 1000 * 10 = 440
        //Umso die Latenz des Microphones auf 10ms zu setzen
        //TODO WIE GENAU FUNKTIONIERT DAS
        while (Microphone.GetPosition(deviceName) < 440) {
            print(Microphone.GetPosition(deviceName));
        }
        
        //playback.Play();
        
    }

    void Update() {
    }

    private void FixedUpdate() {
        //print(Microphone.GetPosition(deviceName));
    }
}
