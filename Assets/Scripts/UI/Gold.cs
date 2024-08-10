using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Gold : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;
    [HideInInspector]
    public Text goldCount;

    void Start() {
        goldCount = gameObject.GetComponent<Text>();
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    void Update() {
        if (goldCount != null) goldCount.text = $"Gold - {GlobalData.currentGold}";
    }
}