using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FrequencyHandler : MonoBehaviour {
    public FrequencyReader _frequencyReader;
    public TMP_Text _testFrequencyOutput;
    private float[] spectrum = new float[8192];
    private double hz;
    void Start()
    {
        print(spectrum.Length);             
    }

    
    void Update() {
        hz = computeHertz();
        _testFrequencyOutput.text = hz.ToString() + " Hz";
    }

    public double getHz() {
        return hz;
    }

    double computeHertz() {
        _frequencyReader.playback.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float max = spectrum.Max();
        print(max);
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            // Find Maximum in spectrumData
            if (Math.Abs(spectrum[i] - max) < 0.0001) {
                print("Max i: " + i);
                
                // 440 / 162 = 2.69938650307
                // 162 durch Testen ermittelt
                
                return 2.69938650307 * i;
            }
            /*
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
            */ 
        }

        return 0;
    }
}
