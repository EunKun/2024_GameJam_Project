using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
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
        SoundManager.ins.PlaySound(SoundManager.ins._normalToneVoice -0.15f, voice_start);
    }
}
