using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;

    public float speed = 1f;
    public float damage = 10f;
    public float reward = 100f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    
    private int waypointIndex = 0;
    public Waves waves;
    public int waveMax = 15;
    public int wavePosition = 1;

    public TextMeshProUGUI healthText;

    void Start() {
        if (waves == null) waves = FindObjectOfType<Waves>();
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    void Update() {
        Move();
        UpdateHealthText();
        DieIfHealth0();
    }

    void Move() {
        // MoveTowardsFinishLine();
        if (waves != null && waves.waypoints.Length > 0) {
            MoveTowardsWaypoints();
        }
    }

    void MoveTowardsWaypoints() {
        Vector3 targetPosition = waves.GetWaypointPosition(waypointIndex);
        Vector3 direction = targetPosition - transform.position;

        // Flip the sprite based on the direction of movement
        // if (flipSprite == true) {
        //     if (direction.x < 0) {
        //         spriteRenderer.flipX = flipInverse ? true : false;
        //     } else if (direction.x > 0) {
        //         spriteRenderer.flipX = flipInverse ? false : true;
        //     }
        // }

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Check if the enemy is close to the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            waypointIndex++;
            int amountOfWaypoints = waves.waypoints.Length;
            if (waypointIndex >= amountOfWaypoints) {
                // Reached the final waypoint, destroy the enemy or handle end of path
                GlobalData.startLives = GlobalData.startLives - damage;
                Die();
            }
        }
    }

    void MoveTowardsFinishLine() {
        if (gameSettings.finishLine != null) {
            transform.position = Vector3.MoveTowards(transform.position, gameSettings.finishLine.position, speed * Time.deltaTime);
        }
    }

    void UpdateHealthText() {
        if (healthText != null) {
            healthText.text = $"{gameSettings.RemoveDotZero(currentHealth.ToString("F2"))}";
        }
    }

    public void TakeDamage(float damage, bool criticalStrike) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
    }

    void Die() {
        Destroy(gameObject);
    }

    void DieIfHealth0() {
        if (currentHealth <= 0) {
            Die();
        }
    }
}