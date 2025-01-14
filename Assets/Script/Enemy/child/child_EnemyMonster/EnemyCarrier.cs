using System.Drawing;
using UnityEngine;

public class EnemyCarrier : EnemyMonster
{
    private Camera mainCamera;
    //float screenBottom;

    // ����� �� ��ȯ�� suicideBomber�� ��ġ�� �����ϱ� ���� ������
    public float radius; // ���� ������

    public override void InitBundle()
    {
        base.InitBundle();
        damage = 10;
        hp = 30;
        moveSpeed = 0.1f;
        defeatScore = 10;
        radius = 1f;

        mainCamera = Camera.main;
        //screenBottom = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y; // ī�޶��� �ϴ� ��ǥ
        //Debug.Log("carrier screen bottom : " + screenBottom);
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

    protected override void Update()
    {
        // �⺻ ����
        base.Update();

        // �������� �������� ��
        if (player.transform.position.y > gameObject.transform.position.y)
        {
            OutOfScreen();
        }
    }

    public void CallSuicideBomber(Vector3 center, float radius)
    {
        int numOfSuicideBomber = 8;
        // 8���� ���� ���ϱ� ���� �ݺ���
        for (int i = 0; i < numOfSuicideBomber; i++)
        {
            // ���� ��� (45���� ȸ��)
            float angle = i * (360 / numOfSuicideBomber);
            float radian = angle * Mathf.Deg2Rad;  // ������ �������� ��ȯ

            // X, Y ��ǥ ���
            float x = center.x + radius * Mathf.Cos(radian); // ����*cos@
            float y = center.y + radius * Mathf.Sin(radian); // ����*sin@

            // ���� �� ���
            Vector3 point = new Vector3(x, y, center.z); // z���� �״�� ����
            //Debug.Log("Point " + i + ": " + point);
            EnemyPoolSuicideBomber.Instance.GetObject(point);
        }
        
    }

    public override void Move()
    {
        Vector3 pos = transform.position;
        pos.y -= Time.deltaTime * moveSpeed * GameManager.Instance.gameSpeed;
        gameObject.transform.position = pos;
    }

    public override void OutOfScreen()
    {
        CallSuicideBomber(transform.position, radius);
        EnemyPoolCarrier.Instance.ReturnObject(gameObject);
    }
}
