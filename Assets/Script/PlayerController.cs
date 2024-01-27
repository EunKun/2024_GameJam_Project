using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arrowgame
{
    public class PlayerController : MonoBehaviour
    {
        BoxCollider playerCollider;
        [SerializeField] private float playerHealth = 3.0f;
        Animator playerAnimator;

        private AudioSource audioSource;
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioClip hitSound;
        //GameManager
        ArrowGameController arrowGameController;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            arrowGameController = GameObject.Find("ArrowGameController").GetComponent<ArrowGameController>();
            arrowGameController.setProcedureBoard();
            playerAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void setPlayerHealth(float value)
        {
            playerHealth = value;
        }
        public float getPlayerHealth()
        {
            return playerHealth;
        }
        public void addToPlayerHealth(float value)
        {
            playerHealth += value;
            audioSource.PlayOneShot(hitSound);
            arrowGameController.setProcedureBoard();
            if (playerHealth < 1f)
            {
                playerDeath();
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        private void playerDeath()
        {
            audioSource.PlayOneShot(deathSound);
            playerAnimator.SetTrigger("isDead");
            arrowGameController.StopArrowGame();
        }

        
    }

}