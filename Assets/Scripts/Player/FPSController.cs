using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSController : MonoBehaviour {

    public Slot slot1, slot2, slot3, slot4;
    public int toolIndex = 0;
    public Transform r1, r2, r3, r4;
    Image reference;
    public Transform camera;

    public Slot holdingCard;

    Vector3 localHoldCard;

	void Start () {
        reference = r2.GetComponent<Image>();
        localHoldCard = holdingCard.transform.localPosition;
        ManaStart();
        HealthStart();
	}
	
	void Update () {
        PickUpUpdate();
        UpdateHolding();
        HitUpdate();
        UpdateMana();
	}

    #region pickup
    void PickUpUpdate() {
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 7, LayerMask.GetMask("Tool"))) {
            if (Input.GetKeyDown(KeyCode.E)) {
                PickUp(hit.collider.GetComponent<ToolController>().tool, hit.collider.gameObject);
            }
            if (reference.color.r > 0.5f) {
                Color col = new Color(64/255f, 128/255f, 1);
                r1.GetComponent<Image>().color = col;
                r2.GetComponent<Image>().color = col;
                r3.GetComponent<Image>().color = col;
                r4.GetComponent<Image>().color = col;
            }
        } else if (reference.color.r < 0.5f) {
            r1.GetComponent<Image>().color = Color.white;
            r2.GetComponent<Image>().color = Color.white;
            r3.GetComponent<Image>().color = Color.white;
            r4.GetComponent<Image>().color = Color.white;
        }
    }

    void PickUp(Tool t, GameObject go) {
        if(slot1.tool == null) {
            slot1.Change(t);
            ChangeHoldingIndex(0);
        } else if (slot2.tool == null) {
            slot2.Change(t);
            ChangeHoldingIndex(1);
        } else if (slot3.tool == null) {
            slot3.Change(t);
            ChangeHoldingIndex(2);
        } else if (slot4.tool == null) {
            slot4.Change(t);
            ChangeHoldingIndex(3);
        } else {
            if (toolIndex == 0) {
                ChangeHoldingIndex(0);
            } else if (toolIndex == 1) {
                ChangeHoldingIndex(1);
            } else if (toolIndex == 2) {
                ChangeHoldingIndex(2);
            } else if (toolIndex == 3) {
                ChangeHoldingIndex(3);
            }
        }

        Destroy(go);
    }

    void UpdateHolding() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeHoldingIndex(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeHoldingIndex(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeHoldingIndex(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ChangeHoldingIndex(3);
        }else if (Input.GetKeyDown(KeyCode.Q)) {
            ChangeHoldingIndex(toolIndex + 1 > 3 ? 0 : toolIndex + 1);
        }
    }

    void ChangeHoldingIndex(int newIndex) {
        switch (newIndex) {
            case 0: ChangeHoldingCard(slot1); break;
            case 1: ChangeHoldingCard(slot2); break;
            case 2: ChangeHoldingCard(slot3); break;
            case 3: ChangeHoldingCard(slot4); break;
        }
        RotateSlot(toolIndex, 0);
        toolIndex = newIndex;
        RotateSlot(toolIndex, 180);
    }

    void ChangeHoldingCard(Slot t) {
        if(t.tool != null) {
            if (!holdingCard.gameObject.activeSelf) {
                holdingCard.gameObject.SetActive(true);
            }
            holdingCard.Change(t.tool);

        } else {
            holdingCard.gameObject.SetActive(false);
        }
    }

    void RotateSlot(int index, float rotation) {
        switch (toolIndex) {
            case 0: slot1.transform.GetChild(0).localEulerAngles = new Vector3(0, rotation, 0); break;
            case 1: slot2.transform.GetChild(0).localEulerAngles = new Vector3(0, rotation, 0); break;
            case 2: slot3.transform.GetChild(0).localEulerAngles = new Vector3(0, rotation, 0); break;
            case 3: slot4.transform.GetChild(0).localEulerAngles = new Vector3(0, rotation, 0); break;
        }
    }

    Tool GetCurrentTool() {
        if (toolIndex == 0) {
            return slot1.tool;
        } else if (toolIndex == 1) {
            return slot2.tool;
        } else if (toolIndex == 2) {
            return slot3.tool;
        } else {
            return slot4.tool;
        }
    }
    #endregion

    #region mana
    public float manaIncrement = 1;
    float manaTimer;
    int mana = 0;
    public TextMeshProUGUI manaText;

    void ManaStart() {
        manaText.text = mana.ToString();
    }

    void UpdateMana() {
        if(mana < 10) {
            manaTimer += Time.deltaTime;
            float incerement = manaIncrement;

            Tool t = GetCurrentTool();
            if(t != null && t.toolType == ToolType.MANAGAINER) {
                incerement = 0.5f;
            }

            if (manaTimer >= incerement) {
                manaTimer = 0;
                mana += 1;
                manaText.text = mana.ToString();
            }
        }
    }

    bool HasMana(int amount) {
        return mana >= amount;
    }

    void DepleteMana(int amount) {
        mana -= amount;
        manaText.text = mana.ToString();
    }
    #endregion

    #region health
    public float health = 100;
    public TextMeshProUGUI healthText;

    void HealthStart() {
        healthText.text = health.ToString();
    }

    public void Damage(float amount) {
        health -= amount;
        healthText.text = health.ToString();
        if (health <= 0) {
            Debug.Log("DEAD");
        }
    }
    #endregion

    #region UseTool
    float useTimer = 0;

    void HitUpdate() {
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            Tool t = GetCurrentTool();
            if (t != null && useTimer <= 0 && HasMana(t.strength)) {
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enemy"))) {
                    if (Vector3.Distance(transform.position, hit.collider.transform.position) < t.distance) {
                        if (t.toolType == ToolType.MELEE)
                            Melee(t, hit.collider.transform);  
                    }
                }
                if (t.toolType == ToolType.RANGE)
                    Range(t);

                DepleteMana(t.strength);
                useTimer = t.useCoolOff;
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            holdingCard.transform.localPosition = localHoldCard + Vector3.up * 0.6f;
        }
        else if (Input.GetMouseButtonUp(1)) {
            holdingCard.transform.localPosition = localHoldCard;
        }

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enemy"))) {
            Tool t = GetCurrentTool();
            bool inRange = t != null && Vector3.Distance(transform.position, hit.collider.transform.position) < t.distance;
            if (t != null && t.distance <= 0)
                inRange = true;
            r1.GetComponent<Image>().color = inRange || t == null ? Color.red : Color.white;

            if (reference.color.b > 0.5f || (inRange && r1.GetComponent<Image>().color.r > 0.5f)) {
                //r1.GetComponent<Image>().color = inRange || t == null ? col : Color.white;
                r2.GetComponent<Image>().color = Color.red;
                r3.GetComponent<Image>().color = Color.red;
                r4.GetComponent<Image>().color = Color.red;
            }
        } else if (reference.color.b < 0.5f) {
            r1.GetComponent<Image>().color = Color.white;
            r2.GetComponent<Image>().color = Color.white;
            r3.GetComponent<Image>().color = Color.white;
            r4.GetComponent<Image>().color = Color.white;
        }

        if (useTimer > 0) {
            useTimer -= Time.deltaTime;
            if (useTimer <= 0) {
                useTimer = 0;
            }
        }
    }

    void Melee(Tool t, Transform enemy) {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Damage(t.strength);
    }

    void Range(Tool t) {
        //EnemyController enemyController = enemy.GetComponent<EnemyController>();
        //enemyController.Damage(t.strength);
        GameObject projectile = Instantiate(t.projectilePrefab, camera.position - Vector3.up * 0.2f, camera.rotation);
        projectile.GetComponent<Projectile>().Shoot(camera.forward, t.projectileSpeed, t.strength);
    }
#endregion
}
