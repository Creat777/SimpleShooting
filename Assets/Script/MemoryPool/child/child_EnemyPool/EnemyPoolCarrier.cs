using UnityEngine;
using System.Collections.Generic;
using System.Buffers;

// �̱��� : ���� ������ 1���� �����ؾ� �ϴ� ��ü
public class EnemyPoolCarrier : EnemyPool
{

    public static EnemyPoolCarrier Instance { get; private set; }

    protected override void MakeSingleTone()
    {
        // �̱���
        if (Instance == null)
        {
            Instance = this; // ��ũ��Ʈ ������Ʈ
        }
        else
        {   // �̹� �ν��Ͻ�, �� �̱����� �ϼ������� ������ ��ü�� �̱����� �����ϴ� ��ü�̴� �Ҹ��Ŵ
            Destroy(gameObject); // �������÷��Ϳ� �޸� �ݳ�
            return;
        }
        DontDestroyOnLoad(gameObject); // �ٸ� ������ ���� �ı����� ����
    }

    //AudioSource audioSource { get; set; }


    // ������ �÷��� - �޸� ����ȭ ������ ���� �� ����
    // �޸�Ǯ : �����س��� ���� ������ �޸𸮸� ����ϰڴ�.

    // �̱���
    //public static EnemyPoolCarrier Instance { get; private set; }

    //public GameObject enemyPrefab;

    //private Queue<GameObject> enemyPool;

    //void MakeSingleTone()
    //{
    //    // �̱���
    //    if (Instance == null)
    //    {
    //        Instance = this; // ��ũ��Ʈ ������Ʈ
    //    }
    //    else
    //    {   // �̹� �ν��Ͻ�, �� �̱����� �ϼ������� ������ ��ü�� �̱����� �����ϴ� ��ü�̴� �Ҹ��Ŵ
    //        Destroy(gameObject); // �������÷��Ϳ� �޸� �ݳ�
    //        return;
    //    }
    //    DontDestroyOnLoad(gameObject); // �ٸ� ������ ���� �ı����� ����
    //    // ���� �ű涧 ��ü�� �ı����� ������ ���� ������ �����Ҷ� ���ο� ��ü�� ���������.
    //    // �̱��濡�� �� ��� Destroy(gameObject);�� ���簡 �Ǵ� �һ�縦 ���� �� �ִ�.
    //}
    //protected override void Awake()
    //{
    //    MakeSingleTone();
    //    memoryPool = new Queue<GameObject>();
    //    audioSource = GetComponent<AudioSource>();
    //}


    //public void InitializePool(int size = 5)
    //{
    //    enemyPool.Clear();
    //    for (int i = 0; i < size; i++)
    //        CreateNewEnemy();
    //}

    //protected override void CreateNewEnemy()
    //{
    //    base.CreateNewEnemy();

    //    GameObject enemyObject = Instantiate(enemyPrefab);
    //    EnemyMonster enemyScript = enemyObject.GetComponent<EnemyMonster>();
    //    enemyScript.Deactive();
    //    if (enemyPool != null)
    //    {
    //        enemyPool.Enqueue(enemyObject);
    //        //Debug.Log("enemyPool ��ü�� ������");
    //    }
    //    else
    //    {
    //        Debug.LogError("enemyPool ��ü�� �������� ����");
    //    }
    //}

    //public GameObject GetEnemy(Vector3 position)
    //{
    //    if (enemyPool != null)
    //    {
    //        if (enemyPool.Count == 0)
    //        {
    //            CreateNewEnemy();
    //        }
    //        //Debug.Log(enemyPool.Count);
    //        GameObject enemy = enemyPool.Dequeue();
    //        enemy.GetComponent<EnemyMonster>().Initalize(position);
    //        return enemy;
    //    }
    //    else
    //    {
    //        Debug.LogError("enemyPool ��ü�� �������� ����");
    //        return null;
    //    }
    //}

    //public void ReturnEnemy(GameObject obj)
    //{
    //    audioSource.volume = ButtonClickAudio.Instance.LoadVolumeData();
    //    audioSource.Play();
    //    obj.GetComponent<EnemyMonster>().Deactive();
    //    enemyPool.Enqueue(obj);
    //}
}
