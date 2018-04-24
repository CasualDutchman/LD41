using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {

    public string enemyName;
    public EnemyType enemyType;
    public string enemyDesc;
    public float maxHealth;
    public float strength;

    public Rarity rarity;

    public Sprite image;

    public AudioClip attackClip;

    public float scale = 1;

    public float distanceToHit = 1;
    public float attackTime = 1;

    public AnimationCurve meleeAttackCurve;
    public float timeDamageDealt = 0;

    [Header("Range")]
    public GameObject projectile;
    public float projectileSpeed;
}

public enum EnemyType { MELEE, RANGE };
