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
    
    public float critChance = 10.0f;
    public float critMultiplier = 2.0f;
    public float attackSpeed = 2.0f;
    public float damageMin = 1.0f;
    public float damageMax = 3.0f;
    public float radiusModifier = 3.33f;
    public float cost = 100.0f;
    public float baseCost = 100.0f;
    
    public AudioSource shootSound;
    public GameObject projectile;
    public AudioSource hitSound;
    public Transform barrel;
    public Transform target;

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    void Update() {
        if (target != null) {
            Shoot(target.gameObject);
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