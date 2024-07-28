using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum HandleShapes {
    Circle = 1,
    Square = 2,
}

public class Waves : MonoBehaviour {
    [HideInInspector] public GameSettings gameSettings;

    public float delay = 5f;
    public GameObject[] waves;
    private int currentWaveIndex = 0;
    private bool gameStarted = false;
    [HideInInspector] public bool wavesStarted = false;
    // private bool isWaitingForNextWave = false;

    public bool alwaysShowPath = true;
    public Vector3[] waypoints;
    public int fontSize = 16;
    public float handleSize = 0.25f;
    public Color fontColor = Color.white;
    public Color pathColor = Color.black;
    public Color handleColor = Color.black;
    public Color wireCircleColor = Color.black;
    public HandleShapes handleShape = HandleShapes.Circle;

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
        SetPath();
        SetWaves();
        StartWaves();
    }

    void SetPath() {
        gameStarted = true;
        GlobalData.waypointPosition = transform.position;
        gameSettings.finishLineX = waypoints[waypoints.Length - 1].x;
    }

    void SetWaves() {
        bool userNotProvidedWaves = waves == null || waves.Length == 0 || waves[0] == null;
        if (userNotProvidedWaves) {
            List<GameObject> waveList = new List<GameObject>();
            foreach (Transform child in transform) {
                if (child.GetComponent<Wave>() != null) {
                    waveList.Add(child.gameObject);
                }
            }
            waves = waveList.ToArray();
        }
    }

    void StartWaves() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        gameSettings.currentLevel = currentSceneIndex + 1;
        for (int i = 0; i < waves.Length; i++) {
            waves[i].SetActive(false);
        }
    }

    public Vector3 GetWaypointPosition(int index) {
        return GlobalData.waypointPosition + waypoints[index];
    }

    public void ActivateNextWave() {
        // Debug.Log("Outer Activate Wave " + currentWaveIndex);
        if (currentWaveIndex < waves.Length - 1) {
            if (wavesStarted == true) {
                // waves[currentWaveIndex].SetActive(false);
                currentWaveIndex++;
            }
            GlobalData.currentWave = currentWaveIndex + 1;
            GlobalData.lastEnemyInWaveSpawned = false;
            GlobalData.lastEnemyInWaveDied = false;
            waves[currentWaveIndex].SetActive(true);
            // SetActiveWave();
            wavesStarted = true;
            // Debug.Log("Inner Activate Wave " + currentWaveIndex);
            // GlobalData.Message = "Wave Started";
        }
    }

    // void SetActiveWave() {

    // }

    // void Update() {
    // UpdateWaves();
    // }

    // void UpdateWaves() {
    //     bool readyForNextWave = GlobalData.lastEnemyInWaveSpawned && GlobalData.lastEnemyInWaveDied;
    //     if (readyForNextWave && !isWaitingForNextWave) {
    //         StartCoroutine(StartNextWaveAfterDelay());
    //     }
    // }

    public void DrawPath() {
        if (waypoints == null || waypoints.Length == 0) return;
        if (gameStarted && transform.hasChanged) GlobalData.waypointPosition = transform.position;

        for (int i = 0; i < waypoints.Length; i++) {
            bool isFirst = i == 0;
            bool isLast = i == (waypoints.Length - 1);
            bool notFirstAndNotLast = i < waypoints.Length - 1;
            Gizmos.color = isFirst ? Color.green : isLast ? Color.red : wireCircleColor;
            Gizmos.DrawWireSphere(waypoints[i] + GlobalData.waypointPosition, 0.45f);
            if (notFirstAndNotLast) {
                bool isLastLine = i >= waypoints.Length - 2;
                Gizmos.color = isFirst ? Color.green : (isLast || isLastLine) ? Color.red : pathColor;
                Gizmos.DrawLine(waypoints[i] + GlobalData.waypointPosition, waypoints[i + 1] + GlobalData.waypointPosition);
            }
        }
    }

    private void OnDrawGizmos() {
        if (!alwaysShowPath) return;
        DrawPath();
    }

    private void OnDrawGizmosSelected() {
        if (alwaysShowPath) return;
        DrawPath();
    }
}