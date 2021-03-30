using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    
    private static string inputDevice;
    
    public static void changeScene(string currentScene, string loadScene){
        SceneManager.LoadScene(loadScene);
        SceneManager.UnloadSceneAsync(currentScene);
    }
    
    public static void setInput(string deviceName) {
        inputDevice = deviceName;
    }
    public static string getInput(){
        return inputDevice;
    }
}