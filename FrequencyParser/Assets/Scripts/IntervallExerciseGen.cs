using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures;
using TMPro;
using UnityEngine;

public class IntervallExerciseGen : MonoBehaviour
{
    
    public TMP_Text _exerciseOut;
    public TMP_Text _exerciseStellung;
    public double currentExer = 4;
    public FrequencyHandler _FrequencyHandler;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nextExer();
        print(Math.Abs(_FrequencyHandler.getHz() - currentExer));
        if (Math.Abs(_FrequencyHandler.getHz() - currentExer) < 3) {
            _exerciseOut.text = "Richtig";
        }
        else {
            _exerciseOut.text = "Falsch";
        }
    }

    void nextExer() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentExer = genIntervall((float) ReineIntervalleCent.QUINTE, 110);
            _exerciseStellung.text = ReineIntervalleCent.QUINTE.ToString();
            _exerciseOut.text = "Suche";
        }
    }

    float genIntervall(float intervall, float grundton) {
        return grundton * (float)Math.Pow(2 ,(intervall / 1200) );
    }
}
