using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour {

    public Tool tool;
    public TextMeshPro toolNameText;
    public TextMeshPro toolDescText;
    public TextMeshPro strengthText;
    public SpriteRenderer spriteRenderer;

    public void Change() {
        if(tool != null) {
            gameObject.SetActive(true);
        } else {
            return;
        }
        toolNameText.text = tool.toolName;
        toolDescText.text = tool.toolDesc;
        strengthText.text = tool.toolType == ToolType.MANAGAINER ? "M" : (tool.toolType == ToolType.HEALTHGAINER ? "H" : tool.strength.ToString("F0"));
        spriteRenderer.sprite = tool.image;

        transform.GetComponentInChildren<Renderer>().material = RarityController.instance.GetRarityMaterial(tool.rarity);
    }

    public void Change(Tool t) {
        tool = t;
        if (tool != null) {
            gameObject.SetActive(true);
        } else {
            return;
        }
        toolNameText.text = tool.toolName;
        toolDescText.text = tool.toolDesc;
        strengthText.text = tool.toolType == ToolType.MANAGAINER ? "M" : (tool.toolType == ToolType.HEALTHGAINER ? "H" : tool.strength.ToString("F0"));
        spriteRenderer.sprite = tool.image;

        transform.GetComponentInChildren<Renderer>().material = RarityController.instance.GetRarityMaterial(tool.rarity);
    }
}
