using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim;
    public Animator btnmoveanim;
    public Animator btnturneanim;
    public SpriteRenderer spriteRender;
    private Vector3 startPosition;
    private Vector3 oldPosition;
    private bool isTurn = false;

    public SliderExample sliderex;

    private int moveCnt = 0;
    private int turnCnt = 0;
    private int spawnCount = 0;

    public  bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Init();
    }
    private void Init()
    {
        anim.SetBool("Die", false);
        transform.position = startPosition;
        oldPosition = startPosition;
        moveCnt = 0;
        spawnCount = 0;
        turnCnt = 0;
        isTurn = false;
        spriteRender.flipX = isTurn;
        isDie = false;
        sliderex.slider.value = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CharTurn();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CharMove();
        }
    }
    public void CharMove()
    {
        btnmoveanim.SetTrigger("Click");
        sliderex.slider.value = 1;
        if (isDie)
            return;
        moveCnt++;
        AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1f), 1);
        MoveDirection();
        if (isFailTurn())//»ç¸Á
        {
            CharDie();
            return;
        }
        if (moveCnt > 7)
        {
            RespawnStair();
        }
        GameManager.Instance.AddScore();
    }
    public void CharTurn()
    {
        btnturneanim.SetTrigger("Click");
        sliderex.slider.value = 1;
        isTurn = isTurn == true ? false : true;

        spriteRender.flipX = isTurn;

        if (isDie)
            return;
        moveCnt++;
        AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1f), 1);
        MoveDirection();
        if (isFailTurn())//»ç¸Á
        {
            CharDie();
            return;
        }
        if (moveCnt > 7)
        {
            RespawnStair();
        }
        GameManager.Instance.AddScore();
    }

   private void MoveDirection()
    {
        if (isTurn)
        {
            oldPosition += new Vector3(-0.75f, 0.5f, 0);
        }
        else
        {
            oldPosition += new Vector3(0.75f, 0.5f, 0);

        }

        transform.position = oldPosition;
        anim.SetTrigger("Move");
    }
   private bool isFailTurn()
    {
        bool resurt = false;

        if (GameManager.Instance.isTurn[turnCnt] != isTurn)
        {
            return true;
        }
        turnCnt++;

        if (turnCnt >  GameManager.Instance.staris.Length -1)
        {
            turnCnt = 0;
        }
        return resurt;
    }
    private void RespawnStair()
    {
        GameManager.Instance.SpawnStair(spawnCount);
        spawnCount++;

        if (spawnCount > GameManager.Instance.staris.Length -1)
        {
            spawnCount = 0;
        }
    }
    public void CharDie()
    {
        if (isDie)
            return;
        GameManager.Instance.GameOver();
        anim.SetBool("Die", true);
        isDie = true;
        AudioManager.instance.PlaySound(transform.position, 1, Random.Range(1f, 1f), 1);

    }
    public void ButtonRestart()
    {
        Init();
        GameManager.Instance.Init();
        GameManager.Instance.InitStairs();
        GameManager.Instance.audiosource.Play();
    }
}
