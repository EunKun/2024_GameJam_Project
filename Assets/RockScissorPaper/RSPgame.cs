using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
public enum AnswerStates
{
    Rock,
    Scissors,
    Paper
}
public class RSPgame : MonoBehaviour
{

    AnswerStates playerAnswer;
    AnswerStates enemyAnswer;

    bool isMatching;
    bool isChosen;

    //result
    private bool isWin;

    //animator controllers
    private Animator playerAnimator;
    private Animator enemyAnimator;
    [SerializeField] private AnimationClip attackClip;

    [SerializeField] private Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("playerCharacter").GetComponent<Animator>();
        enemyAnimator = GameObject.Find("enemyCharacter").GetComponent<Animator>();
        attackClip = Resources.Load<AnimationClip>("3d/attack_anim");
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void CheckResult()
    {
        resultText.text = "";
        setEnemyAnswer();

        //attack animation start and EndEventInvoke
        {
            playerAnimator.SetTrigger("isAttack");
            enemyAnimator.SetTrigger("isAttack");
            Invoke("EndEvent", attackClip.length);
        }

    }

    public void EndEvent()
    {
        if (playerAnswer == enemyAnswer)
        {

            resultText.text = "Your Answer: " + playerAnswer.ToString() + "\nEnemyAnswer: " + enemyAnswer.ToString() + "\nDraw";
            Debug.Log("¹«½ÂºÎ");
        }
        else
        {
            //check win or loose
            if (playerAnswer == AnswerStates.Rock)
            {
                switch (enemyAnswer)
                {
                    case AnswerStates.Scissors:
                        isWin = true;
                        break;
                    case AnswerStates.Paper:
                        isWin = false;
                        break;
                }
            }
            else if (playerAnswer == AnswerStates.Scissors)
            {
                switch (enemyAnswer)
                {
                    case AnswerStates.Rock:
                        isWin = false;
                        break;
                    case AnswerStates.Paper:
                        isWin = true;
                        break;
                }
            }
            else if (playerAnswer == AnswerStates.Paper)
            {
                switch (enemyAnswer)
                {
                    case AnswerStates.Rock:
                        isWin = true;
                        break;
                    case AnswerStates.Scissors:
                        isWin = false;
                        break;
                }
            }
            else
            {
                Debug.LogError("Result Error!!!");
            }

            //Print Win or loose
            if (isWin)
            {

                resultText.text = "Your Answer: " + playerAnswer.ToString() + "\nEnemyAnswer: " + enemyAnswer.ToString() + "\nyou won";
                Debug.Log("you won");
            }
            else
            {
                resultText.text = "Your Answer: " + playerAnswer.ToString() + "\nEnemyAnswer: " + enemyAnswer.ToString() + "\nyou failed";
                Debug.Log("you failed");
            }
        }

        if(isWin)
        {
            //playerAnimator.SetTrigger("isAttack");
            enemyAnimator.SetTrigger("isDead");
        }
        else if(!isWin)
        {
            playerAnimator.SetTrigger("isDead");
            //enemyAnimator.SetTrigger("isAttack");
        }
        else
        {
            Debug.Log("What?");
        }
    }

    void Proceed()
    {

    }

    private void setEnemyAnswer()
    {

        enemyAnswer = (AnswerStates)Random.Range(0, 2);
    }

    //set PlayerAnswer
    public void ChooseRock()
    {
        playerAnswer = AnswerStates.Rock;
        Debug.Log("Rock Choosed " + (int)playerAnswer);
    }
    public void ChooseScissors()
    {
        playerAnswer = AnswerStates.Scissors;
        Debug.Log("Scissors Choosed " + (int)playerAnswer);
    }
    public void ChoosePaper()
    {
        playerAnswer = AnswerStates.Paper;
        Debug.Log("Paper Choosed " + (int)playerAnswer);
    }
}
