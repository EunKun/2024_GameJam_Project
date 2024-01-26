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

    [Header("---캐릭터")]
    public GameObject player;
    public GameObject enemy;
    public GameObject countdownObj;

    public Sprite[] rockPaperScissors;
    public Image[] img_player_rockPaperScissors;


    [SerializeField] AudioSource _audio;
    public AudioClip _swordswing;
    public AudioClip _bgmSource;

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
        yield return new WaitForSeconds(4f);

        if (status == Status.Play)
        {
            GetComponent<AudioSource>().PlayOneShot(player.GetComponent<Player>().voice_wait);
            yield return new WaitForSeconds(1f);
            GetComponent<AudioSource>().PlayOneShot(enemy.GetComponent<Enemy>().voice_wait);
            yield return new WaitForSeconds(1f);
        }

        if (status != Status.Result)
            StartCoroutine(Play_WaitVoice());
    }

    IEnumerator CharaAttack(bool _win, GameObject _attacker, GameObject _taker)
    {
        Vector3 _pos;
        _pos = _attacker.transform.position;

        AudioClip _atk_voice;
        AudioClip _atk_winner;
        
        status = Status.Result;

        if (_win)
        {
            _attacker.transform.position = _taker.transform.position + new Vector3(-1.4f, 0, 0);
            _atk_voice = player.GetComponent<Player>().voice_attack;
            _atk_winner = player.GetComponent<Player>().voice_winner;
        }
        else
        {
            _attacker.transform.position = _taker.transform.position + new Vector3(1.4f, 0, 0);
            _atk_voice = enemy.GetComponent<Enemy>().voice_attack;
            _atk_winner = enemy.GetComponent<Enemy>().voice_winner;
        }

        _attacker.GetComponent<Animator>().Play("attack");
        _audio.PlayOneShot(_swordswing);
        _audio.PlayOneShot(_atk_voice);
        yield return new WaitForSeconds(_attacker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 0.7f);

        _taker.GetComponent<Animator>().Play("die");
        yield return new WaitForSeconds(_attacker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 0.3f);

        tm_result.gameObject.SetActive(true);
        _attacker.transform.position = _pos;
        _audio.PlayOneShot(_atk_winner);
    }

    public void Btn_CheckResult(int _num)
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
                }
                break;
        }

        enemy.GetComponent<Enemy>().resultImg.sprite = rockPaperScissors[(int)enemyAnswer];

        if (isWin)
        {
            tm_result.text = "승리!";
            StartCoroutine(CharaAttack(isWin, player, enemy));
        }
        else
        {
            tm_result.text = "패배!";
            StartCoroutine(CharaAttack(isWin, enemy, player));
        }
    }

    public void Init()
    {
        status = Status.Play;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        enemyAnswer = (AnswerStates)Random.Range(0, 2);
        enemy.GetComponent<Enemy>().Init();
        player.GetComponent<Player>().Init();
        tm_result.gameObject.SetActive(false);
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
