using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrequencyHandler : MonoBehaviour {
    private int _cutoffArray;
    private float[] _cutSpectrum;
    private float _hz;
    private FrequencyUtil _freqUtil;
    
    private float _sampleCoeficient;

    private float[] _spectrum = new float[8192];
    public FrequencyReader frequencyReader;
    public int maxReadableFrequency = 1100;

    private float noiseFilter;
    private float overtoneThresh;


    void Start() {
        Debug.Log(_spectrum.Length);
        Debug.Log(frequencyReader.SampleRate);
        
        noiseFilter = SceneHandler.NoiseFilter;
        overtoneThresh = SceneHandler.ToneThresh;
        
        // 44100 / 2 / 8192 = 2.690..
        // Abtasttheorem führt zu SampleRate / 2
        // Sopran singt bis zu c6 ca. 1100hz -> 400 samples reichen bei einem sampleCoefficient von ca. 2.69
        // 400 * 2.69 = 1076hz

        _sampleCoeficient = (float) frequencyReader.SampleRate / 2 / _spectrum.Length;
        _cutoffArray = (int) (maxReadableFrequency / _sampleCoeficient) + 1;

        _cutSpectrum = new float[_cutoffArray];

        Debug.Log("SampleCoefficient: " + _sampleCoeficient);

        _freqUtil = new FrequencyUtil();
    }


    void FixedUpdate() {
        _hz = ComputeFrequency();
    }

    public float GetHz() {
        return _hz;
    }

    public string GetNote() {
        return _freqUtil.GetNoteNameFromFreq(_hz);
    }

    float ComputeFrequency() {
        frequencyReader.playback.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);

        Array.Copy(_spectrum, 0, _cutSpectrum, 0, _cutoffArray);

        int absMaxIndex = 0;
        float max = _cutSpectrum.Max();
        float avg = _cutSpectrum.Average();
        _cutSpectrum[0] = 0;

        for (int i = 1; i < _cutSpectrum.Length - 1; i++) {
            // Noise Filter
            if (_cutSpectrum[i] < avg * noiseFilter) {
                _cutSpectrum[i] = 0;
                continue;
            }

            // Find Maximum in spectrumData
            // -> Der stärkste Oberton häufig auch der Grundton
            if (Math.Abs(_cutSpectrum[i] - max) < 0.00001) {
                absMaxIndex = i;
            }
            
        }

        int bestIndex = GetFundamentalFreq(absMaxIndex);

        Interpolation inter = new Interpolation(_cutSpectrum);
        float interpolatedIndex = inter.GetMaxIndexInterpolated(bestIndex);

        if (Math.Abs(interpolatedIndex) > 0.000001) {
            return interpolatedIndex * _sampleCoeficient;
        }

        return bestIndex * _sampleCoeficient;
    }


    int HandleOvertones(int absMaxIndex) {
        int bestIndex = absMaxIndex;

        int newReturnVal = GetFundamentalFreq(absMaxIndex);
        if (newReturnVal != 0 && _cutSpectrum[newReturnVal] >= _cutSpectrum[absMaxIndex] * overtoneThresh) {
            bestIndex = newReturnVal;
        }

        return bestIndex;
    }

    private int GetFundamentalFreq(int absMaxIndex) {
        float overtoneMax = 0;
        int overtoneMaxIndex = absMaxIndex;

        for (int i = absMaxIndex; i > 0; i--) {
            float currentVal = _cutSpectrum[i];

            if (Math.Abs(currentVal) < 0.00001) {
                overtoneMax = 0;
            }

            if (currentVal > _cutSpectrum[absMaxIndex] * overtoneThresh) {
                if (overtoneMax < currentVal) {
                    overtoneMax = currentVal;
                    overtoneMaxIndex = i;
                }
            }
        }

        return overtoneMaxIndex;
    }

    List<Tuple<int, int>> FindAllOvertones(int absMaxIndex) {
        List<Tuple<int, int>> startEndIndize = new List<Tuple<int, int>>();
        for (int i = absMaxIndex; i > 0;) {
            float currentVal = _cutSpectrum[i];
            while (Math.Abs(currentVal) < 0.00001) {
                currentVal = _cutSpectrum[i];
                i--;
            }

            int start = i;
            while (currentVal > 0.00001) {
                currentVal = _cutSpectrum[i];
                i--;
            }

            startEndIndize.Append(new Tuple<int, int>(i + 1, start));
        }

        return startEndIndize;
    }
    
    public float NoiseFilter {
        get => noiseFilter;
        set => noiseFilter = value;
    }

    public float OvertoneThresh {
        get => overtoneThresh;
        set => overtoneThresh = value;
    }
}