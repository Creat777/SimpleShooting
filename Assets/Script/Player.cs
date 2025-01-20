using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    public Animator animator;
    public RectTransform uiHpBar;
    private Slider hpBarSlider;
    public Sprite plyaerSprite;
    private float playerHeight;
    public int hp;
    public int hpMax;

    public Joystick joystick;

    private void Awake()
    {
        //makeSingleTone();
    }
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        playerHeight = plyaerSprite.bounds.size.y;
        hpMax = 100;
        hpBarSlider = uiHpBar.GetComponent<Slider>();
    }

    void Update()
    {
        PlyaerAnimation();
        //UpDownClamp();
        PlyaerMove();
        PlayerHpUi();
        if(hp<0 && GameManager.Instance.isGameStart == true)
        {
            GameManager.Instance.GameOver();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            PlayerBulletPool.Instance.EnableBullets();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy.tag == "Enemy" || enemy.tag == "EnemyBullet")
        {
            OnDamaged(enemy);
            if (enemy.tag == "EnemyBullet")
                enemy.GetComponent<EnemyBullet>().OutOfScreen();
        }
    }

    public void OnDamaged(GameObject obj)
    {
        hp -= obj.GetComponent<Enemy>().damage;
    }
    public void PlayerHpUi()
    {
        // �÷��̾��� world ��ǥ�� UI ��ǥ�� ��ȯ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, playerHeight, 0));

        uiHpBar.position = screenPos;
        hpBarSlider.value = (float)hp/hpMax;
    }

    void PlyaerMove()
    {
        // ����
        float moveX, moveY;
        Vector2 moveDirection;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        { 
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
        }
        else
        {
            moveDirection = joystick.Direction;
        }
        // ����
        rigid.AddForce(moveDirection * GameManager.Instance.gameSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    void PlyaerAnimation()
    {
        if(joystick.Horizontal < 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //Debug.Log("���� �Է�");
            animator.SetInteger("direction_int", -1);
        }
        else if (joystick.Horizontal > 0 || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //Debug.Log("������ �Է�");
            animator.SetInteger("direction_int", 1);
        }
        else animator.SetInteger("direction_int", 0);
    }

    //void UpDownClamp()
    //{
    //    Vector3 pos = transform.position;

    //    // ȭ���� ��� �ϴ�
    //    float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y; // ī�޶��� ��� ��ǥ
    //    float screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y; // ī�޶��� �ϴ� ��ǥ

    //    // ��ġ ���� (ȭ�� ������ ������ �ʰ� ����)
    //    pos.y = Mathf.Clamp(pos.y, screenBottom, screenTop);

    //    transform.position = pos;
    //}
    
}
