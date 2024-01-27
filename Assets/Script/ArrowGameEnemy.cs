using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace arrowgame
{
    public enum Arrows
    {
        up, down, left, right
    }

    public class ArrowGameEnemy : MonoBehaviour
    {
        //arrow
        [SerializeField] private Arrows arrowState;
        [SerializeField] private GameObject ArrowObject;
        //speed
        [SerializeField] private float speed = 1f;
        public void AddSpeed(float speed)
        {
            this.speed += speed;
        }
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
        //animator
        [SerializeField] private Animator animator;
        private AnimationClip deadAnimationClip;
        private AnimationClip attackAnimationClip;


        //GameManager
        ArrowGameController arrowGameController;

        // Start is called before the first frame update
        void Start()
        {
            arrowGameController = GameObject.Find("ArrowGameController").GetComponent<ArrowGameController>();

            //arrowVisual = transform.Find("ArrowVisualText");
            arrowState = (Arrows)Random.Range(0, 4);
            animator = GetComponent<Animator>();

            deadAnimationClip = Resources.Load<AnimationClip>("3d/die_anim");
            attackAnimationClip = Resources.Load<AnimationClip>("3d/attack_anim");
            SetArrow();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        //set arrow
        private void SetArrow()
        {
            if (arrowState == Arrows.down) ArrowObject.transform.Rotate(180f, 0f, 0f);
            else if (arrowState == Arrows.left) ArrowObject.transform.Rotate(90f, 0f, 0f);
            else if (arrowState == Arrows.right) ArrowObject.transform.Rotate(-90f, 0f, 0f);
        }
        private void disableArrow()
        {
            ArrowObject.SetActive(false);
        }

        public Arrows GetArrowData()
        {
            return arrowState;
        }

        public void DeathEvent()
        {
            disableArrow();
            speed = 0f;
            animator.SetTrigger("isDead");
            arrowGameController.addScore(1);
            Destroy(gameObject,deadAnimationClip.length);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(AttackEvent());
                collision.gameObject.GetComponent<PlayerController>().addToPlayerHealth(-1.0f);
            }
        }

        IEnumerator AttackEvent()
        {
            disableArrow();
            Attack();
            yield return new WaitForSeconds(attackAnimationClip.length);
            Destroy(gameObject);
        }
        public void Attack()
        {
            speed = 0f;
            animator.SetTrigger("isAttack");
            gameObject.GetComponent<BoxCollider>().enabled = false;

        }

    }

}