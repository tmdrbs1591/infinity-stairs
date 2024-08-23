using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("°è´Ü")]
    [Space(10)]
    public GameObject[] staris;
    public bool[] isTurn;
    [Header("UI")]
    [Space(10)]
    public GameObject UI_GameOver;
    public TextMeshProUGUI textMaxScore;
    public TextMeshProUGUI textNowScore;
    public TextMeshProUGUI textShowScore;
    private int maxScroe = 0;
    private int nowScroe = 0;


    public AudioSource audiosource;
    public AudioClip audio;
    private enum State
    {
        Start,Left,Right
    }

    private State state;
    private Vector3 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        audiosource.clip = audio;
        Instance = this;
        Init();
        InitStairs();
       
    }

   public void Init()
    {
        state = State.Start;
        oldPosition = Vector3.zero;

        isTurn = new bool[staris.Length];

        for (int i = 0; i < staris.Length; i++)
        {
            staris[i].transform.position = Vector3.zero;
            isTurn[i] = false;
        }
        nowScroe = 0;

        textShowScore.text = nowScroe.ToString();

        UI_GameOver.SetActive(false);
    }
    public void InitStairs()
    {
        for (int i = 0; i < staris.Length; i++)
        {
            switch (state)
            {
                case State.Start:
                    staris[i].transform.position = new Vector3(0,-2.8f,0);
                    state = State.Right;
                    break;
                case State.Left:
                    staris[i].transform.position = oldPosition + new Vector3(-0.75f, 0.5f, 0);
                    isTurn[i] = true;
                    break;
                case State.Right:
                    staris[i].transform.position = oldPosition + new Vector3(0.75f, 0.5f, 0);
                    isTurn[i] = false;
                    break;
            }
            oldPosition = staris[i].transform.position;


            if (i != 0)
            {
                int ran = Random.Range(0,5);
                if (ran < 2 && i< staris.Length -1)
                {
                    state = state == State.Left ? State.Right : State.Left;
                }
            }
        }
    }
    public void SpawnStair(int cnt)
    {
        int ran = Random.Range(0, 5);
        if (ran < 2 )
        {
            state = state == State.Left ? State.Right : State.Left;
        }
        switch (state)
        {
            case State.Left:
                staris[cnt].transform.position = oldPosition + new Vector3(-0.75f, 0.5f, 0);
                isTurn[cnt] = true;
                break;
            case State.Right:
                staris[cnt].transform.position = oldPosition + new Vector3(0.75f, 0.5f, 0);
                isTurn[cnt] = false;
                break;
        }
        oldPosition = staris[cnt].transform.position;
    }
    public void GameOver()
    {
        StartCoroutine(ShowGameOver());
    }
    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1f);

        UI_GameOver.SetActive(true);
        audiosource.Stop();
        AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1f, 1f), 1);

        if (nowScroe > maxScroe)
        {
            maxScroe = nowScroe;
        }

        textMaxScore.text = maxScroe.ToString();
        textNowScore.text = nowScroe.ToString();

    }

    public void AddScore()
    {
        nowScroe += 1 ;
        textShowScore.text = nowScroe.ToString();
    }
}
