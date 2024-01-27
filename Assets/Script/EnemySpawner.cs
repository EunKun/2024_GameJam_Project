using arrowgame;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace arrowgame
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField][Range(0f, 10f)] private float spawnInterval = 1f;
        private Transform spawnPos;
        [SerializeField] private GameObject enemyPrefab;

        bool canSpawn;




        // Start is called before the first frame update
        void Start()
        {
            enemyPrefab = Resources.Load<GameObject>("Prefabs/EnemyPrefab");
            enemyPrefab.GetComponent<ArrowGameEnemy>().SetSpeed(2f);
            canSpawn = true;

        }

        // Update is called once per frame
        void Update()
        {
            if (canSpawn)
            {
                StartCoroutine(spawnEnemy());
            }
        }


        IEnumerator spawnEnemy()
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            canSpawn = false;
            yield return new WaitForSeconds(spawnInterval);
            canSpawn = true;
        }
    }

}