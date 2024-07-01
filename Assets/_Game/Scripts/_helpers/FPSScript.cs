using TMPro;
using UnityEngine;

public class FPSScript : MonoBehaviour
{
    private TextMeshProUGUI _FPSText;

    private float deltaTime = 0.0f;
    void Awake()
    {
        _FPSText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _FPSText.text = "FPS: " + Mathf.Round(fps);
    }
}