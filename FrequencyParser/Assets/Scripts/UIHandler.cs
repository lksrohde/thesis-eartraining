using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = System.Diagnostics.Debug;

public class UIHandler : MonoBehaviour {
    public Canvas _uiCanvas;
    private Dropdown microphoneDropdownMenu;

    public FrequencyReader _frequencyReader;
    
    // Start is called before the first frame update
    void Start() {
        
        microphoneDropdownMenu = _uiCanvas.GetComponentInChildren<Dropdown>();
        
        microphoneDropdownMenu.ClearOptions();
        microphoneDropdownMenu.AddOptions(Microphone.devices.ToList());
        
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartInfinite() {
        SceneHandler.changeScene("MainMenu", "InfiniteTest");
    }

    public void ValueChanged() {
        _frequencyReader.deviceName = microphoneDropdownMenu.options[microphoneDropdownMenu.value].text;
        SceneHandler.setInput(_frequencyReader.deviceName);
    }
}
