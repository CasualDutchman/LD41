using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager instance;

    List<Transform> enemiesInScene = new List<Transform>();

    Transform[] spawnPoints;

    public GameObject enemyCardPrefab;
    public GameObject toolCardPrefab;

    public int maxEnemies = 30;
    public float waitTime;
    float timer;

    public Transform player;

    void Awake() {
        instance = this;
    }

	void Start () {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++) {
            spawnPoints[i] = transform.GetChild(i);
        }

        SpawnEnemy(FindSpot());
        SpawnEnemy(FindSpot());
    }
	
	void Update () {
        if (enemiesInScene.Count < maxEnemies) {
            timer += Time.deltaTime;
            if (timer >= waitTime) {
                timer = 0;
                SpawnEnemy(FindSpot());
            }
        }
	}

    Vector3 FindSpot() {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < 10; i++) {
            int ranIndex = Random.Range(0, spawnPoints.Length);
            if (Vector3.Distance(player.position, spawnPoints[ranIndex].position) > 10) {
                pos =  spawnPoints[ranIndex].position;
                break;
            }
        }
        return pos;
    }

    void SpawnEnemy(Vector3 pos) {
        if (pos.Equals(Vector3.zero))
            return;

        GameObject go = Instantiate(enemyCardPrefab, pos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        go.GetComponent<EnemyController>().enemy = RarityController.instance.GetRarityEnemy();
        RegisterEnemy(go.transform);
    }

    void RegisterEnemy(Transform t) {
        enemiesInScene.Add(t);
    }

    public void DeregisterEnemy(Transform t) {
        enemiesInScene.Remove(t);
    }
}
