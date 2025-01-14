using UnityEngine;

public class EnemySuicide : EnemyMonster
{
    public override void InitBundle()
    {
        base.InitBundle();
        damage = 1;
        hp = 3;
        moveSpeed = 0.3f;
        defeatScore = 1;
    }

    protected override void Update()
    {
        //�⺻ ����
        base.Update();
    }

    public override void OnTriggerInit(Collider2D collision)
    {
        base.OnTriggerInit(collision);
        if (collision.gameObject == player)
        {
            EnemyPoolSuicideBomber.Instance.ReturnObject(gameObject);
        }

    }

    public override void Move()
    {
        Vector3 pos = transform.position;
        Vector3 direction = (player.transform.position - pos).normalized; // �÷��̾������ ��������
        pos += direction * moveSpeed * Time.deltaTime * GameManager.Instance.gameSpeed;
        gameObject.transform.position = pos;
    }

    public override void WhenDamaged(GameObject bullet)
    {
        if (bullet.tag == "MainBullet")
            HpDown(bullet.GetComponent<MainBullet>().damage);
        else if (bullet.tag == "SubBullet")
            HpDown(bullet.GetComponent<SubBullet>().damage);
    }

    public override void OutOfScreen()
    {
        EnemyPoolSuicideBomber.Instance.ReturnObject(gameObject);
    }
}
