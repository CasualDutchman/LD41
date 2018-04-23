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

    public bool onGround = true;
    public AnimationCurve bobbing;
    public float bobSpeed = 10;
    float timer;
    public float rotationSpeed = 5;

    public float coolDown;

    void Start() {
        toolNameText.text = tool.toolName;
        toolDescText.text = tool.toolDesc;
        strengthText.text = tool.toolType == ToolType.MANAGAINER ? "M" : tool.strength.ToString("F0");
        spriteRenderer.sprite = tool.image;

        if(onGround)
            PlaceOnGround();
    }

    void Update () {
        if (onGround) {
            timer += Time.deltaTime * (1 / bobSpeed);
            if (timer >= 1) {
                timer = 0;
            }
            transform.GetChild(0).localPosition = new Vector3(0, bobbing.Evaluate(timer), 0);
            transform.GetChild(0).Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }
	}

    public void PlaceOnGround() {
        onGround = true;
        transform.localScale = Vector3.one * 0.3f;
        timer = 0;
    }

    public void OnHand() {
        onGround = false;
    }
}
