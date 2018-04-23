using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerEnemies : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) {
            other.GetComponent<EnemyController>().target = transform.parent;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) {
            other.GetComponent<EnemyController>().target = null;
        }
    }
}
