using UnityEngine;

public class EnemyBullet : Enemy
{

    void Awake()
    {
        damage = 5;
        //Debug.Log(screenBottom);
    }

    private void Start()
    {

    }

    void Update()
    {
        
        Vector2 pos = transform.position;
        pos.y -= GameManager.Instance.gameSpeed * Time.deltaTime;
        transform.position = pos;

        // ���� ����ÿ��� �Ѿ� ����
        if(GameManager.Instance.isGameStart == false)
        {
            OutOfScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            OutOfScreen();
        }
    }

    public void PlayerHit()
    {
        Debug.Log("�÷��̾� �浹!");
        OutOfScreen();
    }

    public override void OutOfScreen()
    {
        EnemyBulletPool.Instance.ReturnShooterBullet(gameObject);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
    public void SetDeActive()
    {
        gameObject.SetActive(false);
    }
}
