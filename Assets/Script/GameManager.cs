using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnswerStates
{
    Rock,
    Scissors,
    Paper
}

public enum Status
{
    Start,
    Play,
    Result
}

public class GameManager : MonoBehaviour
{
    public static GameManager ins;

    public Status status;
    AnswerStates enemyAnswer;
    bool isWin;

    [Header("---某腐磐")]
    public GameObject player;
    public GameObject enemy;

    public GameObject resetObj;
    public GameObject mainBackObj;
    public GameObject selectObj;
    public GameObject countdownObj;

    public Sprite[] rockPaperScissors;
    public Image[] img_player_rockPaperScissors;

    [SerializeField] AudioSource _audio;
    public AudioClip _swordswing;

    public Text tm_result;

    private void Start()
    {
        for (int i = 0; i < img_player_rockPaperScissors.Length; i++)
        {
            img_player_rockPaperScissors[i].sprite = rockPaperScissors[i];
        }

        Init();
    }

    public void Btn_PlayAnim(string _name)
    {
        player.GetComponent<Animator>().Play(_name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Btn_CheckResult(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Btn_CheckResult(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Btn_CheckResult(2);
        }
    }

    IEnumerator Play_WaitVoice()
    {
        yield return new WaitForSeconds(5f);

        if (status == Status.Play)
        {
            SoundManager.ins.PlaySound(SoundManager.ins._normalToneVoice, player.GetComponent<Player>().voice_wait);
            yield return new WaitForSeconds(1f);

            SoundManager.ins.PlaySound(SoundManager.ins._lowToneVoice, enemy.GetComponent<Enemy>().voice_wait);
            yield return new WaitForSeconds(1f);
        }

        if (status != Status.Result)
            StartCoroutine(Play_WaitVoice());
    }

    IEnumerator CharaAttack(bool _win, bool _draw, GameObject _attacker, GameObject _taker)
    {
        Vector3 _pos;
        _pos = _attacker.transform.position;

        AudioClip _atk_voice;
        AudioClip _atk_winner;
        AudioClip _taker_die;
        
        status = Status.Result;

        if(_draw)
        {
            tm_result.gameObject.SetActive(true);
            resetObj.SetActive(true);
            mainBackObj.SetActive(true);

            yield return null;
        }
        else
        {
            if (_win)
            {
                _attacker.transform.position = _taker.transform.position + new Vector3(-1.4f, 0, 0);
                _atk_voice = player.GetComponent<Player>().voice_attack;
                _atk_winner = player.GetComponent<Player>().voice_winner;
                _taker_die = enemy.GetComponent<Enemy>().voice_die;
            }
            else
            {
                _attacker.transform.position = _taker.transform.position + new Vector3(1.4f, 0, 0);
                _atk_voice = enemy.GetComponent<Enemy>().voice_attack;
                _atk_winner = enemy.GetComponent<Enemy>().voice_winner;
                _taker_die = player.GetComponent<Player>().voice_die;
            }

            _attacker.GetComponent<Animator>().Play("attack");
            SoundManager.ins.PlaySound(SoundManager.ins._sfx, _swordswing);
            SoundManager.ins.PlaySound(_win, _atk_voice);

            yield return new WaitForSeconds(_attacker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 0.5f);

            _taker.GetComponent<Animator>().Play("die");
            SoundManager.ins.PlaySound(_win, _taker_die);

            yield return new WaitForSeconds(_attacker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 0.5f);

            tm_result.gameObject.SetActive(true);
            _attacker.transform.position = _pos;

            _attacker.GetComponent<Animator>().Play("win");
            SoundManager.ins.PlaySound(_win, _atk_winner);
            resetObj.SetActive(true);
            mainBackObj.SetActive(true);
        }
    }

    public void Btn_CheckResult(int _num)
    {
        enemy.GetComponent<Enemy>().resultImg.gameObject.SetActive(true);
        enemy.GetComponent<Enemy>().resultImg.sprite = rockPaperScissors[(int)enemyAnswer];
        if(status == Status.Play)
        {
            StopCoroutine(Play_WaitVoice());
            switch (_num)
            {
                case 0:
                    switch (enemyAnswer)
                    {
                        case AnswerStates.Scissors:
                            isWin = true;
                            break;
                        case AnswerStates.Paper:
                            isWin = false;
                            break;
                        case AnswerStates.Rock:
                            tm_result.text = "公铰何!";
                            StartCoroutine(CharaAttack(isWin, true, player, enemy));
                            break;
                    }
                    break;
                case 1:
                    switch (enemyAnswer)
                    {
                        case AnswerStates.Rock:
                            isWin = false;
                            break;
                        case AnswerStates.Paper:
                            isWin = true;
                            break;
                        case AnswerStates.Scissors:
                            tm_result.text = "公铰何!";
                            StartCoroutine(CharaAttack(isWin, true, player, enemy));
                            break;
                    }
                    break;
                case 2:
                    switch (enemyAnswer)
                    {
                        case AnswerStates.Rock:
                            isWin = true;
                            break;
                        case AnswerStates.Scissors:
                            isWin = false;
                            break;
                        case AnswerStates.Paper:
                            tm_result.text = "公铰何!";
                            StartCoroutine(CharaAttack(isWin, true, player, enemy));
                            break;
                    }
                    break;
            }

            enemy.GetComponent<Enemy>().resultImg.sprite = rockPaperScissors[(int)enemyAnswer];

            if (isWin)
            {
                tm_result.text = "铰府!";
                StartCoroutine(CharaAttack(isWin, false, player, enemy));
            }
            else
            {
                tm_result.text = "菩硅!";
                StartCoroutine(CharaAttack(isWin, false, enemy, player));
            }
        }
    }

    public void Init()
    {
        status = Status.Play;
        resetObj.SetActive(false);
        selectObj.SetActive(false);
        mainBackObj.SetActive(false);
        tm_result.gameObject.SetActive(false);

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        enemy.GetComponent<Enemy>().Init();
        player.GetComponent<Player>().Init();

        enemyAnswer = (AnswerStates)Random.Range(0, 2);
        countdownObj.SetActive(true);
        StartCoroutine(countdownObj.GetComponent<Anim_BattleStart>().Start_Countdown());
        StartCoroutine(Play_WaitVoice());
    }

    private void Awake()
    {
        if (ins == null)
            ins = this;
        else
            Destroy(this);
    }
}
