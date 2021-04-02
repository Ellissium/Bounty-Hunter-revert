using UnityEngine;

public class FPScontroller : MonoBehaviour
{
    GUIStyle style = new GUIStyle();
    float counter = 0;

    void Start()
    {
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 34), "FPS: " + counter, style);
    }

    void Update()
    {
        counter = 1.0f / Time.deltaTime;
    }
}
