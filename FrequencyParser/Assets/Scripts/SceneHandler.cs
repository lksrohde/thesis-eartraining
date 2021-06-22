using System;
using DataStructures;
using UnityEngine.SceneManagement;

public static class SceneHandler {
    private static string _inputDevice;
    private static Tuple<GleichstufigFreq, GleichstufigFreq> _range;
    private static string _rangeName;
    private static float _noiseFilter;
    private static float _toneThresh;
    private static string _lastScene;
    
    public static void ChangeScene(string loadScene) {
        _lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(loadScene);
    }

    public static void ToLastScene() {
        SceneManager.LoadScene(_lastScene);
    }

    public static void SetRange(Tuple<GleichstufigFreq, GleichstufigFreq> inRange, string inRangeName) {
        _range = inRange;
        _rangeName = inRangeName;
    }

    public static Tuple<GleichstufigFreq, GleichstufigFreq> GetRange() {
        return _range;
    }

    public static string GetRangeName() {
        return _rangeName;
    }
    
    public static string InputDevice { get; set; }

    public static float NoiseFilter { get; set; }

    public static float ToneThresh { get; set; }
}