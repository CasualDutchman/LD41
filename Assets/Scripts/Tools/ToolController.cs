using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolController : MonoBehaviour {

    public Tool tool;
    public TextMeshPro toolNameText;
    public TextMeshPro toolDescText;
    public TextMeshPro strengthText;
    public SpriteRenderer spriteRenderer;

    public AnimationCurve bobbing;
    public float bobSpeed = 10;
    float timer;
    public float rotationSpeed = 5;

    public float coolDown;

    Vector3 newWorldPos;

    float despawnTimer;

    void Start() {
        toolNameText.text = tool.toolName;
        toolDescText.text = tool.toolDesc;
        strengthText.text = tool.toolType == ToolType.MANAGAINER ? "M" : tool.strength.ToString("F0");
        spriteRenderer.sprite = tool.image;

        transform.localScale = Vector3.one * 0.3f;

        Ray ray = new Ray(transform.position + Vector3.up * 5, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Water"))) {
            newWorldPos = hit.point;
        }

        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = RarityController.instance.GetRarityMaterial(tool.rarity);
    }

    void Update () {
        timer += Time.deltaTime * (1 / bobSpeed);
        if (timer >= 1) {
            timer = 0;
        }
        transform.position = newWorldPos + new Vector3(0, 0.2f + bobbing.Evaluate(timer) * 0.5f, 0);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        despawnTimer += Time.deltaTime;
        if (despawnTimer >= 60) {
            Destroy(gameObject);
        }
    }
}
