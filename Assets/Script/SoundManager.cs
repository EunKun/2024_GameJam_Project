using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager ins;

    public readonly float _lowToneVoice = 0.5f;
    public readonly float _normalToneVoice = 0.38f;
    public readonly float _bgm = 0.35f;
    public readonly float _sfx = 1f;

    public void PlaySound(float _vol, AudioClip _clip)
    {
        GetComponent<AudioSource>().volume = _vol;
        GetComponent<AudioSource>().PlayOneShot(_clip);
    }

    public void PlaySound(bool _player, AudioClip _clip)
    {
        if (_player)
            PlaySound(_normalToneVoice, _clip);
        else
            PlaySound(_lowToneVoice, _clip);
    }

    private void Awake()
    {
        if (ins == null)
            ins = this;
        else
            Destroy(this);
    }
}
