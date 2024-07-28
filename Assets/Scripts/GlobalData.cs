using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class GlobalData {
  public static float defaultHealth = 100f;
  public static float defaultReward = 10f;
  public static float defaultDamage = 1f;
  public static float defaultSpeed = 2f;

  public static int currentWave = 1;
  public static int currentLevel = 1;
  public static float startLives = 20f;
  public static Vector3 waypointPosition;
  public static bool hasActiveTurret = false;
  public static bool lastEnemyInWaveDied = false;
  public static bool lastEnemyInWaveSpawned = false;

  public static void SetGameObjectTransparency(GameObject gameObj, float alpha) {
    Image[] images = gameObj.GetComponentsInChildren<Image>();
    foreach (Image img in images) {
      Color color = img.color;
      color.a = alpha;
      img.color = color;
    }

    TextMeshProUGUI[] texts = gameObj.GetComponentsInChildren<TextMeshProUGUI>();
    foreach (TextMeshProUGUI text in texts) {
      Color color = text.color;
      color.a = alpha;
      text.color = color;
    }

    SpriteRenderer[] sprites = gameObj.GetComponentsInChildren<SpriteRenderer>();
    foreach (SpriteRenderer sprite in sprites) {
      Color color = sprite.color;
      color.a = alpha;
      sprite.color = color;
    }
  }
}