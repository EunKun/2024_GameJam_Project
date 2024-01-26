using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip voice_start;
    public AudioClip voice_attack;
    public AudioClip voice_die;
    public AudioClip voice_winner;
    public AudioClip voice_wait;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        GetComponent<Animator>().Play("idle");
        GetComponent<AudioSource>().PlayOneShot(voice_start);
    }
}
