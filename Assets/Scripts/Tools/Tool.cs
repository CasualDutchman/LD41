using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Tool")]
public class Tool : ScriptableObject {

    public string toolName;
    public ToolType toolType;
    public string toolDesc;
    [Range(1, 10)]
    public int strength;
    public float distance;

    public Sprite image;

    public float useCoolOff;

    [Header("Ranged")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
}

public enum ToolType { MELEE, RANGE, MANAGAINER };
