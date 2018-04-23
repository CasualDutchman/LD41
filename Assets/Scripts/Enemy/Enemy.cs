using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {

    public string enemyName;
    public EnemyType enemyType;
    public string enemyDesc;
    [Range(1, 99)]
    public float maxHealth;
    public bool doesAttack = true;
    [Range(1, 10)]
    public float strength;

    public Sprite image;

    public float scale = 1;

    public float distanceToHit = 1;
    public float attackTime = 1;

    //[Header("Range")]

    [Header("Melee")]
    public AnimationCurve meleeAttackCurve;
    public float timeDamageDealt = 0;
}

public enum EnemyType { MELEE, RANGE };
