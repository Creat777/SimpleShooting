using UnityEngine;
using UnityEngine.UI;

// Main������ ��ȯ �Ǹ� ��ü�� ��������鼭 ��� �ʱ�ȭ�� �����
public class MainInitializerSet : MonoBehaviour
{
    public GameObject canvas;
    public GameObject menuButton;
    public GameObject player;
    public GameObject scorePanel;
    public GameObject enemyBoss;

    void Start()
    {
        // �޸�Pool �ʱ�ȭ
        EffactPoolManager.Instance.InitializePool();
        EnemyPoolCarrier.Instance.InitializePool();
        EnemyPoolShooter.Instance.InitializePool();
        EnemyPoolSuicideBomber.Instance.InitializePool();
        CoinMemoryPool.Instance.InitializePool();
        BulletFactory.Instance.InitializePool();
        EnemyBulletFactory.Instance.InitializePool();

        // ��ư �ݹ��Լ� �߰�
        menuButton.GetComponent<Button>().onClick.AddListener(SceneManager.Instance.Button_GoToMenu);
        GameManager.Instance.EnemyBoss = enemyBoss;

        GameManagerInit();
        BulletFactoryInit();
    }
    
    private void GameManagerInit()
    {
        // GameManager �� canvas ����
        GameManager.Instance.canvas = canvas;
        if (GameManager.Instance.canvas != null)
        {
            //Debug.Log("GameManager�� canvas ���� ����");
        }
        else
        {
            Debug.LogError("GameManager�� canvas ���� ����");
        }
        // GameManager�� scorePanel ���� 
        GameManager.Instance.scorePanel = scorePanel;

        // GameManager ���� �ִ� Ŭ���� �ʱ�ȭ
        GameManager.Instance.InitCsvManager();
    }

    private void BulletFactoryInit()
    {
        // BulletFactory�� player ����
        BulletFactory.Instance.player = player;

        // BulletFactory�� playerHeight�� playerWidth �ʱ�ȭ
        Bounds bounds = player.gameObject.GetComponent<SpriteRenderer>().sprite.bounds;
        if (bounds != null)
        {
            BulletFactory.Instance.playerHeight = bounds.size.y;
            BulletFactory.Instance.playerWidth = bounds.size.x;
        }
        else
        {
            Debug.LogError("player ��������Ʈ ��ã����");
        }
    }
    void Update()
    {
        
    }
}
