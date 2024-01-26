using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim_BattleStart : MonoBehaviour
{
    public Text tm;

    public AudioClip[] countdown_voice;

    private void OnEnable()
    {
        if (tm == null)
        {
            tm = GetComponent<Text>();
        }

        tm.text = "3";
    }

    private void Start()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator Start_Countdown()
    {
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(true);
        for (int i = countdown_voice.Length; i > 0; i--)
        {
            GetComponent<AudioSource>().PlayOneShot(countdown_voice[i - 1]);

            if(i > 1)
            {
                tm.text = (i - 1).ToString();
            }
            else
            {
                tm.text = "½ºÅ¸Æ®!";
            }
            GetComponent<Animator>().Play("GameOne_Start");
            yield return new WaitForSeconds(0.5f);
        }

        GameManager.ins.selectObj.SetActive(true);
        gameObject.SetActive(false);
    }
}
