using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyController : MonoBehaviour {

    NavMeshAgent agent;

    public bool move = true;

    public Enemy enemy;
    public TextMeshPro enemyNameText;
    public TextMeshPro enemyDescText;
    public TextMeshPro healthText;
    public TextMeshPro strengthText;
    public GameObject strengthGem;
    public SpriteRenderer spriteRenderer;

    public Transform target;

    public float health;

    bool attacking = false;
    float attackTimer = 0;

    float idleTimer = 0;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        transform.localScale = Vector3.one * enemy.scale;

        enemyNameText.text = enemy.enemyName;
        enemyDescText.text = enemy.enemyDesc;
        healthText.text = enemy.maxHealth.ToString("F0");
        if (enemy.doesAttack) {
            strengthText.text = enemy.strength.ToString("F0");
        } else {
            Destroy(strengthText.gameObject);
            Destroy(strengthGem);
        }
        spriteRenderer.sprite = enemy.image;

        health = enemy.maxHealth;

        idleTimer = Random.Range(3f, 7f);
    }

    void Update() {
        if (target != null) {
            if (!attacking) {
                agent.SetDestination(target.position);

                if(Vector3.Distance(transform.position, target.position) <= enemy.distanceToHit) {
                    Attack(target);
                }
            }else {
                attackTimer += Time.deltaTime;
                if (attackTimer >= enemy.attackTime) {
                    attacking = false;
                    attackTimer = 0;
                }
            }
        } else {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0) {
                Vector2 v2 = Random.insideUnitCircle * 3;
                Vector2 v22 = Random.insideUnitCircle;
                Vector3 v3 = transform.position + new Vector3(v2.x + v22.x, 0, v2.y + v22.y);
                agent.SetDestination(v3);
                idleTimer = Random.Range(3f, 7f);
            }
        }


    }

    public void Attack(Transform target) {
        if (enemy.enemyType == EnemyType.MELEE) {
            //target.GetComponent<FPSController>().Damage(enemy.strength);
            StartCoroutine(MeleeAttackAnim());
        }

        attacking = true;
        agent.ResetPath();
    }

    IEnumerator MeleeAttackAnim() {
        float animtimer = 0;
        bool anim = true;
        bool hit = false;

        while (anim) {
            animtimer += Time.deltaTime * (1 / enemy.attackTime);

            if (animtimer >= enemy.timeDamageDealt && target != null && !hit) {
                target.GetComponent<FPSController>().Damage(enemy.strength);
                hit = true;
            }

            transform.GetChild(0).localEulerAngles = new Vector3(enemy.meleeAttackCurve.Evaluate(animtimer) * 15, 0, 0);

            if (animtimer >= 1) {
                anim = false;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    public void Damage(float amount) {
        health -= amount;
        if(health <= 0) {
            Destroy(gameObject);
        }
        healthText.text = health.ToString("F0");
    }
}
