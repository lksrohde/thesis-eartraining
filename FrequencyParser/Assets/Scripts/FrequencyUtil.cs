using System;
using DataStructures;

public class FrequencyUtil {
    private Random _random = new Random();
    private Tuple<GleichstufigFreq, GleichstufigFreq> _range;
    private int[] intervallArr = (int[]) Enum.GetValues(typeof(GleichstufigeIntervalleCent));
    private int[] noteArr = (int[]) Enum.GetValues(typeof(GleichstufigFreq));
    public FrequencyUtil(Tuple<GleichstufigFreq, GleichstufigFreq> range) {
        _range = range;
    }

    public FrequencyUtil() {
        _range = new Tuple<GleichstufigFreq, GleichstufigFreq>(GleichstufigFreq.E, GleichstufigFreq.c3);
    }

    /**
     * Generates a intervall-based Exercise
     */
    public float GenExerIntervall(float grundtonFreq, float intervall, bool up) {
        float diff = ComputeIntervall(intervall, grundtonFreq);
        if (up) {
            return grundtonFreq + diff;
        }

        return grundtonFreq - diff;
    }

    /**
     * Computes the intervall on a given base tone in Hz
     */
    public float ComputeIntervall(float intervall, float grundton) {
        return (grundton * (float) Math.Pow(2, (intervall / 1200))) - grundton;
    }
    
    /**
     * returns a random Intervall in Cent excluding Prime
     */
    public int GetRandomIntervallInCent() {
        return intervallArr[_random.Next(1, intervallArr.Length)];
    }
    /**
     * returns a random Basetone to build an exercise on
     * within the specified range of the user
     */
    public int GetRandomGrundton(bool ascdesc) {
        float baseTone;
        int mid = ((int) _range.Item2 + (int) _range.Item1) / 2;
        if (ascdesc) {
            baseTone = _random.Next((int) _range.Item1, mid); // get random tone in range    
        }
        else {
            baseTone = _random.Next(mid, (int) _range.Item2); // get random tone in range
        }
        return GetNearestNoteFromFreq(baseTone); // get nearest actual tone
    }


    public string GetNoteNameFromFreq(float freq) {
        return ((GleichstufigFreq) GetNearestNoteFromFreq(freq)).ToString();
    }

    public int GetNearestNoteFromFreq(float freq) {
        int freqNormalised = GetNormalized(freq);

        float min = float.MaxValue;
        int lastNote = 0;

        foreach (int note in noteArr) {
            int diffVal = Math.Abs(note - freqNormalised);

            if (diffVal < min) {
                min = diffVal;
                lastNote = note;
                continue;
            }

            return lastNote;
        }

        return 0;
    }
    
    public int CheckForHalbton(int noteVal) {
        if (!Enum.GetName(typeof(GleichstufigFreq), noteVal).Contains("_")) return noteVal;
        return GetUpperGanzton(noteVal); // Only flat tones allowed
    }
    
    public int GetLowerGanzton(int ton) {
        for (int i = 1; i < noteArr.Length; i++) {
            if (noteArr[i] == ton) {
                return noteArr[i - 1];
            }
        }

        return ton;
    }
    
    public int GetUpperGanzton(int ton) {
        for (int i = 0; i < noteArr.Length - 1; i++) {
            if (noteArr[i] == ton) {
                return noteArr[i + 1];
            }
        }

        return ton;
    }
    
    public int GetNormalized(float value) {
        if (value < 2750) {
            return (int) value * 100;
        }

        return (int) value;
    }

    public int GetFrequency(float value) {
        if (value >= 2750) {
            return (int) value / 100;
        }

        return (int) value;
    }
}