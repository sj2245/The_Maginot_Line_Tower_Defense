using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Lives : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;
    [HideInInspector]
    public Text livesCount;

    void Start() {
        livesCount = gameObject.GetComponent<Text>();
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    void Update() {
        if (livesCount != null) livesCount.text = $"Lives - {GlobalData.currentLives}";
    }
}