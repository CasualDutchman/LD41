using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Tool")]
public class Tool : ScriptableObject {

    public string toolName;
    public ToolType toolType;
    public string toolDesc;
    public int strength;
    public Rarity rarity;
    public float distance;

    public AudioClip attackClip;

    public Sprite image;

    public float useCoolOff;

    [Header("Ranged")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
}

public enum ToolType { MELEE, RANGE, MANAGAINER, HEALTHGAINER };
