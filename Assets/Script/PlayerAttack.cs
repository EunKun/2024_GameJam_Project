using arrowgame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace arrowgame
{
    public class PlayerAttack : MonoBehaviour
    {
        private BoxCollider boxCollider;
        private GameObject inBoxObject;

        Arrows correctArrow;

        public Animator playerAnimatorController;

        //Enemy Prefab
        GameObject enemyPrefab;

        [SerializeField] AudioSource playerAudioSource;
        [SerializeField] AudioClip attackAudioClip;
        [SerializeField] AudioClip startAudioClip;

        [SerializeField] ParticleSystem attackEffect;
        // Start is called before the first frame update
        void Start()
        {
            playerAudioSource.PlayOneShot(startAudioClip);
            enemyPrefab = Resources.Load<GameObject>("Prefabs/EnemyPrefab");
            boxCollider  = GetComponent<BoxCollider>();
            //playerAnimatorController = transform.Find("PlayerCharacter").GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if(inBoxObject != null)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && correctArrow == Arrows.up)
                {
                    playerAnimatorController.SetTrigger("isAttack");
                    DestroyEnemy(inBoxObject);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && correctArrow == Arrows.down)
                {
                    playerAnimatorController.SetTrigger("isAttack");
                    DestroyEnemy(inBoxObject);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && correctArrow == Arrows.left)
                {
                    playerAnimatorController.SetTrigger("isAttack");
                    DestroyEnemy(inBoxObject);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && correctArrow == Arrows.right)
                {
                    playerAnimatorController.SetTrigger("isAttack");
                    DestroyEnemy(inBoxObject);
                }
            }
        }

        private void DestroyEnemy(GameObject enemy)
        {
            attackEffect.Play();
            playerAudioSource.pitch = Random.Range(0.9f, 1.1f);
            playerAudioSource.PlayOneShot(attackAudioClip) ;
            inBoxObject = null;
            enemyPrefab.GetComponent<ArrowGameEnemy>().AddSpeed(0.1f);
            enemy.GetComponent<ArrowGameEnemy>().DeathEvent();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                inBoxObject = other.gameObject;
                correctArrow = other.GetComponent<ArrowGameEnemy>().GetArrowData();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            inBoxObject = null;
        }
        
    }

}
