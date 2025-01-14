using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class EnemyShooter : EnemyMonster
{
    float delay;
    bool isDelayed;

    public override void InitBundle()
    {
        base.InitBundle();
        damage = 5;
        hp = 30;
        moveSpeed = 0.1f;
        defeatScore = 10;
        delay = 3.0f;
        isDelayed = false;
    }

    public override void WhenDamaged(GameObject bullet)
    {
        if (bullet.tag == "MainBullet")
            HpDown(bullet.GetComponent<MainBullet>().damage);
        else if (bullet.tag == "SubBullet")
            HpDown(bullet.GetComponent<SubBullet>().damage);
        if (animator.GetBool("isDamaged") == false)
            AnimeChange();
    }

    public override void Move()
    {
        Vector3 pos = transform.position;
        if (pos.y > 3)
        {
            pos.y -= moveSpeed * Time.deltaTime * GameManager.Instance.gameSpeed;
        }

        float direction = Mathf.Sign(player.transform.position.x - pos.x); // �÷��̾���ġ�� x�� ���⼺�� ����ȭ
        pos.x += direction * moveSpeed * Time.deltaTime * GameManager.Instance.gameSpeed; // �÷��̾�� ���� x������ �̵�
        gameObject.transform.position = pos;
    }


    protected override void Update()
    {
        //�⺻ ����
        base.Update();

        // �Ѿ� �߻� ����
        if (isDelayed == false && GameManager.Instance.gameTime > 3.0f)
        {
            try { StartCoroutine(BulletAttack()); }
            catch { Debug.LogError("��ü����"); }
        }
    }

    IEnumerator BulletAttack()
    {
        isDelayed = true;
        for (int i = 0; i < GameManager.Instance.gameDifficulty; i++)
        {
            EnemyBulletFactory.Instance.EnableShooterBullet(transform.position);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(delay);
        isDelayed = false;
    }

    public override void OutOfScreen()
    {
        EnemyPoolShooter.Instance.ReturnObject(gameObject);
    }
}
