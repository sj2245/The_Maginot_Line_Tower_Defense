using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Turret : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;

    public bool canAim = true;
    public bool canFire = true;
    public bool turretPlaced = false;
    public bool alwaysShowRangeIndicator = false;
    public int level = 1;
    public int unlockedAfterWave = 1;
    
    public float cooldown = 0.0f;
    public float critChance = 10.0f;
    public float critMultiplier = 2.0f;
    public float attackSpeed = 2.0f;
    public float damageMin = 5.0f;
    public float damageMax = 10.0f;
    public float radiusModifier = 3.33f;
    public float cost = 100.0f;
    public float baseCost = 100.0f;
    
    public AudioSource shootSound;
    public GameObject projectile;
    private Transform finishLine;
    public AudioSource hitSound;
    private Transform target;
    public Transform barrel;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
        GameObject finishLineObject = GameObject.FindGameObjectWithTag("Finish");
        if (finishLineObject != null) finishLine = finishLineObject.GetComponent<Transform>();
    }

    void Update() {
        if (canAim) {
            enemiesInRange.RemoveAll(enemy => enemy == null || enemy.GetComponent<Enemy>().currentHealth <= 0);
            FindTarget();
            ShootAtTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy")) enemiesInRange.Add(trigger.gameObject);
    }

    private void OnTriggerExit2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy")) enemiesInRange.Remove(trigger.gameObject);
    }

    private void FindTarget() {
        // Debug.Log("Finding Target");
        GameObject closestEnemy = GetClosestEnemy();
        if (closestEnemy != null) {
            target = closestEnemy.transform;
        } else {
            target = null;
        }
    }

    GameObject GetClosestEnemy() {
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        // Debug.Log("Finding Closest Enemy");
        foreach (GameObject enemy in enemiesInRange) {
            if (enemy == null || !enemy.activeInHierarchy) continue;
            float distanceToFinishLineX = Mathf.Abs(enemy.transform.position.x - (finishLine == null ? gameSettings.finishLineX : finishLine.position.x));
            if (distanceToFinishLineX < shortestDistance) {
                shortestDistance = distanceToFinishLineX;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    void ShootAtTarget() {
        if (target != null) {
            RotateTowardsTarget();
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f) {
                Shoot(target.gameObject);
                cooldown = 1.0f / attackSpeed;
            }
        }
    }

    void RotateTowardsTarget() {
        if (target == null) return;
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = rotation;
    }

    private void OnDrawGizmos() {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null) {
            Handles.color = Color.cyan;
            float colliderRange = (float)collider.radius / radiusModifier;
            Handles.DrawWireDisc(transform.position, Vector3.forward, colliderRange);
        }
    }

    void Shoot(GameObject target) {
        if (projectile != null && barrel != null) {
            GameObject projectileObject = Instantiate(projectile, barrel.position, barrel.rotation);
            Projectile proj = projectileObject.GetComponent<Projectile>();
            if (proj != null) {
                bool isCriticalStrike = false;
                float randomValueInRange = Random.Range(0f, 100f);
                float damageInRange = Random.Range(damageMin, damageMax);
                if (randomValueInRange < critChance) {
                    isCriticalStrike = true;
                    damageInRange *= critMultiplier;
                }
                if (shootSound != null) shootSound.Play();
                proj.Seek(target, damageInRange, isCriticalStrike, hitSound, this);
            }
        }
    }
}