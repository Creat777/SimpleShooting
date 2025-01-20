using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    //public EnemyShooter shooter;

    // 에디터 편집
    public GameObject EnemyBulletPrefab;
    public Sprite shooterSprite;

    // 스크립트 편집
    private Queue<GameObject> shooterBulletPool;
    private float shooterHeight;

    public static EnemyBulletPool Instance { get; private set; }
    void MakeSingleTone()
    {
        if (Instance == null)
        {
            Instance = this; // 스크립트 컴포넌트
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Awake()
    {
        MakeSingleTone();
        shooterHeight = shooterSprite.bounds.size.y;
        shooterBulletPool = new Queue<GameObject>();
    }
    void Start()
    {
        
    }

    public void InitializePool(int size = 5)
    {
        shooterBulletPool.Clear();
        for (int i = 0; i < size; i++)
        {
            CreateShooterBullet();
        }
    }

    private void CreateShooterBullet()
    {
        GameObject bulletObject = Instantiate(EnemyBulletPrefab);
        bulletObject.GetComponent<EnemyBullet>().SetDeActive();
        shooterBulletPool.Enqueue(bulletObject);
    }


    public void EnableShooterBullet(Vector3 pos)
    {
        if(shooterBulletPool.Count == 0)
        {
            CreateShooterBullet();
        }
        GameObject bulletObject = shooterBulletPool.Dequeue();
        bulletObject.GetComponent<EnemyBullet>().SetActive();
        bulletObject.transform.position = pos + new Vector3(0, -shooterHeight / 2, 0);
    }


    public void ReturnShooterBullet(GameObject obj)
    {
        obj.SetActive(false);
        shooterBulletPool.Enqueue(obj);
    }

    void Update()
    {

    }
}
