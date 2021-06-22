using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = System.Diagnostics.Debug;

public class MainMenu : MonoBehaviour {
    public Canvas _uiCanvas;

    public Text TonlageOut;
    // Start is called before the first frame update
    void Start() {
        TonlageOut.text = "Tonlage: " + SceneHandler.GetRangeName();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartInfinite() {
        SceneHandler.ChangeScene("MainMenu", "InfiniteTest");
    }
    
    public void StartOptions() {
        SceneHandler.ChangeScene("MainMenu", "Options");
    }
    
    public void EndGame() {
        Application.Quit();
    }
}
