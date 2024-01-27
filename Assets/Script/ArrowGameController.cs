using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace arrowgame
{
    public class ArrowGameController : MonoBehaviour
    {
        private int score = 0;
        [SerializeField] PlayerAttack attackScript;
        [SerializeField] PlayerController playerController;
        [SerializeField] EnemySpawner enemySpawner;

        public Text ScoreText;
        // Start is called before the first frame update
        void Start()
        {
            score = 0;
            //ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            attackScript = GameObject.Find("PlayerAttack").GetComponent<PlayerAttack>();
            playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StopArrowGame()
        {
            attackScript.enabled = false;
            playerController.enabled = false;
            enemySpawner.enabled = false;
            ScoreText.text = score.ToString();
        }
        public void addScore(int value)
        {
            this.score += value;
            //ScoreText.text = "Score : " + score.ToString();
        }

    }

}