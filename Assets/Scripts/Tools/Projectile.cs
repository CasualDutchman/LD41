using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    Vector3 velocity = new Vector3();

    float damage;
    float alive;

    public void Shoot(Vector3 facing, float spe, float dam) {
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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) {
            EnemyController controller = other.GetComponent<EnemyController>();
            controller.Damage(damage);
            Destroy(gameObject);
        }
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("First Person"))) {
            velocity = Vector3.zero;
            transform.position += transform.forward * 0.1f;
            Debug.Log(other.gameObject.name);
        }
    }
}
