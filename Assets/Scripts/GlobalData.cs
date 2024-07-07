using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class GlobalData {
  // public static Wave activeWave;
  public static int maxWaves = 3;
  public static int currentWave = 1;
  public static int currentLevel = 1;
  public static float finishLineX = 0;
  public static float startLives = 20f;
  public static Vector3 waypointPosition;
  public static bool hasActiveTurret = false;
  public static bool lastEnemyInWaveSpawned = false;
  public static bool lastEnemyInWaveDied = false;
}