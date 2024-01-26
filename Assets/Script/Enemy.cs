using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public AudioClip voice_start;
    public AudioClip voice_attack;
    public AudioClip voice_die;
    public AudioClip voice_winner;
    public AudioClip voice_wait;

    public Sprite _sprite;
    public Image resultImg;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        StartCoroutine(StartVoice(0.5f));
    }

    IEnumerator StartVoice(float _waitTime)
    {
        resultImg.sprite = _sprite;
        GetComponent<Animator>().Play("idle");

        yield return new WaitForSeconds(_waitTime);
        GetComponent<AudioSource>().PlayOneShot(voice_start);
    }
}
