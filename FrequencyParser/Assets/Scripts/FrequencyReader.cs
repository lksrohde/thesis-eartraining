using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrequencyReader : MonoBehaviour
{
    AudioClip microphoneInput;
    public AudioSource playback;
    public AudioClip micClip;
    
    public string deviceName;
    
    void Start() {
        
        if (deviceName == "") {
            deviceName = SceneHandler.getInput();
            if (deviceName == null) {
                deviceName = Microphone.devices[0];
            }
        }
        
        print(deviceName);
        
        //Start Microphone Recording
        micClip = Microphone.Start(deviceName, true, 1, 44100);
        playback.clip = micClip;
        playback.loop = true;
        
        
        //Warte bis 10ms in samples vergangen sind -> 44100 / 1000 * 10 = 440
        //Umso die Latenz des Microphones auf 10ms zu setzen
        //TODO WIE GENAU FUNKTIONIERT DAS
        while (Microphone.GetPosition(deviceName) < 440) {
            print("Polling Samples");
        }

        playback.Play();
        
    }

    void Update() {
    }

    private void FixedUpdate() {
        //print(Microphone.GetPosition(deviceName));
    }
}
