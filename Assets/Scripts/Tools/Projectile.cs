using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileHit { Enemy, Player }

public class Projectile : MonoBehaviour {

    ProjectileHit hitType;
    Vector3 velocity = new Vector3();

    float damage;
    float alive;

    public void Shoot(Vector3 facing, float spe, float dam, ProjectileHit hit) {
        hitType = hit;
        velocity = facing * spe;
        damage = dam;
    }
	
	void Update () {
        transform.Translate(velocity * Time.deltaTime, Space.World);

        alive += Time.deltaTime;
        if (alive > 10) {
            Destroy(gameObject);
        }
	}

    bool dead = false;

    private void OnTriggerEnter(Collider other) {
        if (dead)
            return;

        if (hitType == ProjectileHit.Enemy) {
            if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")) || other.GetComponent<EnemyController>()) {
                EnemyController controller = other.GetComponent<EnemyController>();
                controller.Damage(damage);
                Destroy(gameObject);
                dead = true;
            } else if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) && !other.gameObject.layer.Equals(LayerMask.NameToLayer("First Person")) && !other.gameObject.layer.Equals(LayerMask.NameToLayer("Tool"))) {
                velocity = Vector3.zero;
                transform.position += transform.forward * 0.1f;
            }
        }else {
            if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) || other.GetComponent<FPSController>()) {
                FPSController controller = other.GetComponent<FPSController>();
                controller.Damage(damage);
                Destroy(gameObject);
                dead = true;
            } else if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("First Person")) && !other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")) && !other.gameObject.layer.Equals(LayerMask.NameToLayer("Tool"))) {
                velocity = Vector3.zero;
                transform.position += transform.forward * 0.1f;
            }
        }
    }
}
