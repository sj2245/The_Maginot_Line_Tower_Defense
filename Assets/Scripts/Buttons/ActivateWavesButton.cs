using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ActivateWavesButton : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;
    private Button activateWavesButton;
    private bool readyForNextWave;
    private bool isButtonEnabled;
    private Waves waves;

    void Start() {
        if (waves == null) waves = FindObjectOfType<Waves>();
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
        GameObject activateWavesButtonCont = GameObject.FindGameObjectWithTag("ActivateWavesButton");
        if (activateWavesButtonCont) activateWavesButton = activateWavesButtonCont.GetComponent<Button>();
        // if (waves == null) waves = GameObject.FindGameObjectWithTag("Waves");
        UpdateButtonState();
    }

    void Update() {
        UpdateButtonState();
    }

    void UpdateButtonState() {
        bool wavesFinished = GlobalData.currentWave == waves.waves.Length;
        readyForNextWave = GlobalData.lastEnemyInWaveSpawned && GlobalData.lastEnemyInWaveDied;
        if (waves != null) {
            isButtonEnabled = wavesFinished == false && (waves.wavesStarted == false || readyForNextWave);
        }
        float buttonTransparency = isButtonEnabled ? 1f : 0.5f;
        GlobalData.SetGameObjectTransparency(gameObject, buttonTransparency);
        if (activateWavesButton != null) {
            activateWavesButton.interactable = isButtonEnabled;
        }
    }
}