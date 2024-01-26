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
        [SerializeField] private Text arrowVisual;
        //speed
        [SerializeField] private float speed = 1f;
        public void setSpeed(float speed)
        {
            this.speed = speed;
        }

        // Start is called before the first frame update
        void Start()
        {
            //arrowVisual = transform.Find("ArrowVisualText");
            arrowState = (Arrows)Random.Range(0, 4);
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
            if (arrowState == Arrows.up) arrowVisual.text = "up";
            else if (arrowState == Arrows.down) arrowVisual.text = "down";
            else if (arrowState == Arrows.left) arrowVisual.text = "left";
            else if (arrowState == Arrows.right) arrowVisual.text = "right";
        }

        public Arrows GetArrowData()
        {
            return arrowState;
        }
    }

}