using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace arrowgame
{
    public enum Arrows
    {
        up, down, left, right
    }

    public class Enemy : MonoBehaviour
    {
        //arrow
        [SerializeField] private Arrows arrowState;
        [SerializeField] private Image arrowImage;
        //speed
        [SerializeField] private float speed = 1f;
        public void setSpeed(float speed)
        {
            this.speed = speed;
        }

        //animator
        [SerializeField] private Animator animator;
        private AnimationClip deadAnimationClip;
        private AnimationClip attackAnimationClip;
        // Start is called before the first frame update
        void Start()
        {
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
            if (arrowState == Arrows.down) arrowImage.rectTransform.Rotate(0f,0f,180f);
            else if (arrowState == Arrows.left) arrowImage.rectTransform.Rotate(0f, 0f, 90f);
            else if (arrowState == Arrows.right) arrowImage.rectTransform.Rotate(0f, 0f, -90f);
        }
        private void disableArrow()
        {
            arrowImage.enabled = false;
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