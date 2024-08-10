using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Wave : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;

    private Waves waves;
    private float spawnTimer;
    private int objectsSpawned;

    public float spawnDelay;
    public int maxObjects = 10;
    public float health = GlobalData.defaultHealth;
    public float speed = GlobalData.defaultSpeed;
    public float reward = GlobalData.defaultReward;
    public float damage = GlobalData.defaultDamage;
    public GameObject objectToSpawn;

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
        if (waves == null) waves = gameObject.transform.parent.GetComponent<Waves>();
    }

    private void Update() {
       spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0) {
           spawnTimer = spawnDelay;
            if (objectsSpawned >= maxObjects) GlobalData.lastEnemyInWaveSpawned = true;
            if (objectsSpawned < maxObjects) {
                objectsSpawned++;
                SpawnObject(objectsSpawned);
            }
        }
    }

    private void SpawnObject(int wavePosition) {
        GameObject newObjectToSpawn = Instantiate(objectToSpawn);
        Enemy enemyObjectSettings = newObjectToSpawn.GetComponent<Enemy>();
        if (enemyObjectSettings != null) {
            enemyObjectSettings.speed = speed;
            enemyObjectSettings.waves = waves;
            enemyObjectSettings.damage = damage;
            enemyObjectSettings.reward = reward;
            enemyObjectSettings.maxHealth = health;
            enemyObjectSettings.waveMax = maxObjects;
            enemyObjectSettings.currentHealth = health;
            enemyObjectSettings.wavePosition = wavePosition;
        }
    }
}