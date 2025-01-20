using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.ReorderableList;
using System.Collections;
using UnityEngine.Audio;

public class EnemyBoss : EnemyMonster
{
    TextAsset csvFile;
    List<List<float>> positionCsv;
    public AudioSource audioSource;
    public AudioClip[] clips;

    // ���� ������ ��������
    float moveDelay;
    float nextMoveDelay;
    int maxHp;
    int MaxExplosionCount;

    // ���Ǻ���(����)
    int explosionCountDown;
    bool isNextMoveReady;
    bool isExplosion;

    private void Awake()
    {
        NormalVarInit();
        ConditionVarInit();
        gameObject.SetActive(false);
    }

    private void NormalVarInit()
    {
        float level = GameManager.Instance.gameDifficulty;
        maxHp = hp = (int)(500 * ((level - 1 / 2) + 1)); // 1�� ~ 3��
        damage = 20;
        defeatScore = 1000;
        MaxExplosionCount = 20;

        moveDelay = 1.0f;
        nextMoveDelay = 3.0f;
    }

    private void ConditionVarInit()
    {
        // ���Ǻ��� �ʱ�ȭ
        isNextMoveReady = false;
        isExplosion = false;
        explosionCountDown = MaxExplosionCount;
    }

    protected override void OnEnable()
    {
        ConditionVarInit();

        // �⺻ó��
        base.OnEnable();

        //csv���� ó��
        CsvManageFromGameManager();

        //���� ���ó��
        Vector3 appearancePosition = GameManager.Instance.FloatListToVector3(positionCsv[0]);
        StartCoroutine(moving(appearancePosition));
    }

    protected override void Update()
    {
        
        if (explosionCountDown == MaxExplosionCount && hp < maxHp * ((float)2 / 3))
        {
            explosionCountDown--;
            Vector3 effactPosition = transform.position + Vector3.left;
            AudioProcess();
            BombEffactPool.Instance.GetObject(effactPosition);
            BombEffactPool.Instance.GetObject(effactPosition + Vector3.up * 0.5f);
            BombEffactPool.Instance.GetObject(effactPosition + Vector3.left * 0.5f);
            
        }
        else if (explosionCountDown == MaxExplosionCount-1 && hp < maxHp * ((float)1 / 3))
        {
            explosionCountDown--;
            Vector3 effactPosition = transform.position + Vector3.right;
            AudioProcess();
            BombEffactPool.Instance.GetObject(effactPosition);
            BombEffactPool.Instance.GetObject(effactPosition + Vector3.up * 0.5f);
            BombEffactPool.Instance.GetObject(effactPosition + Vector3.right * 0.5f);
            
        }
        else if (hp <= 0) // ���� ����� ����ó���ϰ� ���� �����
        {
            // ����ó���� ó����
            if(explosionCountDown == MaxExplosionCount - 2)
            {
                MonsterDefeated();
            }

            // ���� ����
            GameManager.Instance.isGamePause = true;

            // ����Ʈ �� ����� ó��
            if(isExplosion == false)
            {
                AudioProcess();
                StartCoroutine(BossDefeatEffact(transform.position, 1));
            }

            // ����Ʈ ó���� ������ ���� ����
            if(explosionCountDown == 0)
            {
                OutOfScreen();
            }
        }

        // ���� �����
        if (GameManager.Instance.isGameStart == false)
        {
            OutOfScreen();
        }

        // ���� ����� �ٸ� Enemy�� �ٸ��� �۵���
        if (isNextMoveReady && GameManager.Instance.isGamePause == false && hp>0)
        {
            Move();
        }

        // IdlE ����
        animationCount -= Time.deltaTime;
        if (animationCount < 0)
        {
            animator.Play("EnemyIdle");
        }
    }

    public override void MonsterDefeated()
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
    }

    IEnumerator BossDefeatEffact(Vector3 center, float radius)
    {
        isExplosion = true;

        // ���� ��� (45���� ȸ��)
        float angle = explosionCountDown * (360 / 8);
        float radian = angle * Mathf.Deg2Rad;  // ������ �������� ��ȯ

        // X, Y ��ǥ ���
        float x = center.x + radius * Mathf.Cos(radian); // ����*cos@
        float y = center.y + radius * Mathf.Sin(radian); // ����*sin@

        // ���� �� ���
        Vector3 point = new Vector3(x, y, center.z); // z���� �״�� ����
        BombEffactPool.Instance.GetObject(point);

        yield return new WaitForSeconds(0.1f);

        explosionCountDown--;
        isExplosion = false;
    }

    public void AudioProcess()
    {
        int index = explosionCountDown % 3;
        audioSource.volume = ButtonClickAudio.Instance.LoadVolumeData();
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    public override void Move()
    {
        int randomIndex = Random.Range(0, positionCsv.Count);
        Vector3 movePosition = GameManager.Instance.FloatListToVector3(positionCsv[randomIndex]);
        if(transform.position != movePosition) 
        {
            StartCoroutine(moving(movePosition));
        }
    }

    IEnumerator moving(Vector3 movePosition)
    {
        isNextMoveReady = false;
        gameObject.transform.DOMove(movePosition, moveDelay);
        yield return new WaitForSeconds(nextMoveDelay);
        isNextMoveReady = true;
    }

    private void CsvManageFromGameManager()
    {
        csvFile = Resources.Load<TextAsset>("Enemy_Boss_1");
        positionCsv = new List<List<float>>();
        GameManager.Instance.PositionCsvLoading(csvFile, positionCsv);
    }


    public override void OutOfScreen()
    {
        StartCoroutine(StartDelay(3.0f));
    }

    IEnumerator StartDelay(float delay)
    {
        Vector3 position = GameManager.Instance.FloatListToVector3(positionCsv[0]);
        
        // ������ ��ġ�� �̵��� ����Ʈ ǥ��
        
        yield return new WaitForSeconds(delay / 2);

        // ������ ��ġ�� ����
        transform.DOMove(position + Vector3.up * 3, delay/2);
        yield return new WaitForSeconds(delay / 2);

        // ���������� ���� ����
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

    //public override void WhenDamaged(GameObject bullet)
    //{
    //    if (bullet.tag == "MainBullet" || bullet.tag == "SubBullet")
    //        HpDown(bullet.GetComponent<Bullet>().damage);

    //    if (animator.GetBool("isDamaged") == false && hp > 0)
    //        AnimeChange();
    //}
}
