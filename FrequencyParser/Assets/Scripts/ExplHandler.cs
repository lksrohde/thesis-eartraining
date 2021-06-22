using UnityEngine;
using UnityEngine.UI;

public class ExplHandler : MonoBehaviour {
    public Sprite note, schluessel, intervall;
    private Image explImg;
    void Start() {
        explImg = GetComponent<Image>();
    }

    public void SetNote() {
        explImg.sprite = note;
    }
    public void SetSchluessel() {
        explImg.sprite = schluessel;
    }
    public void SetIntervall() {
        explImg.sprite = intervall;
    }
    public void Back() {
        SceneHandler.ToLastScene();
    }
}
