using UnityEngine;

public class FrequencyReader : MonoBehaviour {
    public AudioSource playback;
    public const int SampleRate = 44100;

    void Start() {
        // Setze deviceName auf das ausgewählte Eingabegerät des Nutzers 
        var deviceName = SceneHandler.InputDevice ?? Microphone.devices[0];
        
        // Start Microphone Recording
        // Loop = true damit kontinuierlich aufgenommen wird

        playback.clip = Microphone.Start(deviceName, true, 1, SampleRate);

        // loop = true, damit der clip nicht endet.
        playback.loop = true;

        // Warte auf Samples ->Wenn Samples ankommen startet Audio
        // so wird eine möglich niedrige Latenz erzielt.
        while (Microphone.GetPosition(deviceName) < 1) { }
        
        playback.Play();
    }

}

