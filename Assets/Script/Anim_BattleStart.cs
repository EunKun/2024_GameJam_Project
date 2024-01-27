using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim_BattleStart : MonoBehaviour
{
    public Text tm;
    public Text tm_start;

    public AudioClip[] countdown_voice;

    private void OnEnable()
    {
        if (tm == null)
        {
            tm = GetComponent<Text>();
        }
        
        tm_start.gameObject.SetActive(false);
    }

    public IEnumerator Start_Countdown()
    {
        gameObject.SetActive(true);
        Animator ani = GetComponent<Animator>();

        for (int i = countdown_voice.Length; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.ins.PlaySound(SoundManager.ins._lowToneVoice + 0.1f, countdown_voice[i - 1]);

            if(i > 1)
            {
                tm.text = (i - 1).ToString();
                ani.Play("GameOne_Start");
            }
            else
            {
                tm.gameObject.SetActive(false);
                tm_start.gameObject.SetActive(true);
            }
        }

        yield return new WaitForSeconds(1f);

        GameManager.ins.selectObj.SetActive(true);
        tm_start.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
