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
        // Start is called before the first frame update
        void Start()
        {
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
        public void addToPlayerHealth(float value)
        {
            playerHealth += value;
            if (playerHealth < 0f)
            {
                playerDeath();
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        private void playerDeath()
        {
            playerAnimator.SetTrigger("isDead");
        }
    }

}