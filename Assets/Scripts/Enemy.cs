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

    public TextMeshProUGUI healthText;

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    void Update() {
        Move();
        UpdateHealthText();
        DieIfHealth0();
    }

    void Move() {
        MoveTowardsFinishLine();
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

    void DieIfHealth0() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}