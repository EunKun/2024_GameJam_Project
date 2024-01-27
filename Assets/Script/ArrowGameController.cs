using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
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
        public Text ProcedureText;
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
            setProcedureBoard();
            //ScoreText.text = "Score : " + score.ToString();
        }

        public void setProcedureBoard()
        {
            ProcedureText.text = "처치 수: " + score.ToString() + "\n라이프 : " + playerController.getPlayerHealth().ToString();
        }

        public void PauseGame()
        {
            if(Time.timeScale == 0f)
            {
                attackScript.enabled = true;
                playerController.enabled = true;
                enemySpawner.enabled = true;
                Time.timeScale = 1f;

            }
            else
            {
                attackScript.enabled = false;
                playerController.enabled = false;
                enemySpawner.enabled = false;
                Time.timeScale = 0f;
            }

        }

        public void LoadMain(string _scene)
        {
            if (!string.IsNullOrEmpty(_scene))
                SceneManager.LoadScene(_scene);
        }
    }
}