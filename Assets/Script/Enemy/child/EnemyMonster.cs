using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System;
using Unity.VisualScripting;
using System.Threading;
using UnityEngine.Audio;

public abstract class EnemyMonster : Enemy
{
    [SerializeField] protected int hp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float defeatScore;
    [SerializeField] public GameObject scorePanel;
    [SerializeField] public Animator animator;
    float animationDelay;
    private void Start()
    {
        InitBundle();
    }

    private void OnEnable()
    {
        InitBundle();
    }
    public virtual void InitBundle()
    {
        player = GameObject.FindWithTag("Player");
        scorePanel = GameObject.Find("ScorePanel");
        animator = gameObject.GetComponent<Animator>();
        animationDelay = 1.0f;
    }

    public void Initalize(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    public void HpDown(int damage)
    {
        hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerInit(collision);
    }
    public virtual void OnTriggerInit(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainBullet")
        {
            WhenDamaged(collision.gameObject);
        }
        else if (collision.gameObject.tag == "SubBullet")
        {
            WhenDamaged(collision.gameObject);
        }
    }
   

    public void AnimeChange()
    {
        StartCoroutine(SwitchAnimation());
    }

    IEnumerator SwitchAnimation()
    {
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(animationDelay); // WaitForSeconds�� �ڷ�ƾ������ ��� ����
        // ������ �ð�(animationDelay)�� ���� �� �� �������� �ٽ� �ڷ�ƾ ����
        animator.SetBool("isDamaged", false);
    }


    public void MonsterDefeated()
    {
        if (scorePanel != null)
        {
            scorePanel.GetComponent<Score>().bonusScore += defeatScore;
            //Debug.Log($"���� �߰� : {defeatScore}");
        }
        else
        {
            Debug.LogError("scorePanel ���� ����");
        }
        OutOfScreen();
    }

    protected virtual void Update()
    {
        Move();

        // ���� ��� ��
        if (hp < 0)
        {
            MonsterDefeated();
        }

        // ���� �����
        if(GameManager.Instance.isGameStart == false)
        {
            OutOfScreen();
        }
    }

    public abstract void Move();
    
    public abstract void WhenDamaged(GameObject obj);
    



}
