using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text tonlageOut;
    void Start() {
        tonlageOut.text = "Tonlage: " + SceneHandler.GetRangeName();
        if (SceneHandler.NoiseFilter == 0 && SceneHandler.ToneThresh == 0) {
            SceneHandler.NoiseFilter = 0.05f;
            SceneHandler.ToneThresh = 0.1f;    
        }
    }

    public void StartInfinite() {
        SceneHandler.ChangeScene("InfiniteTest");
    }
    
    public void StartOptions() {
        SceneHandler.ChangeScene("Options");
    }

    public void StartModule() {
        SceneHandler.ChangeScene("IntervallModule");
    }
    public void StartTheory() {
        SceneHandler.ChangeScene("TheoryBook");
    }
    public void EndGame() {
        Application.Quit();
    }
}
