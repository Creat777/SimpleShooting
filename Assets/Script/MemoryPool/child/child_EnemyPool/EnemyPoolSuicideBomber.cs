using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


// �̱��� : ���� ������ 1���� �����ؾ� �ϴ� ��ü
public class EnemyPoolSuicideBomber : EnemyPool 

{
    public static EnemyPoolSuicideBomber Instance { get; private set; }
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

    //// ������ �÷��� - �޸� ����ȭ ������ ���� �� ����
    //// �޸�Ǯ : �����س��� ���� ������ �޸𸮸� ����ϰڴ�.

    //// �̱���
    //public static EnemyPoolSuicideBomber Instance { get; private set; }

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
    //private void Awake()
    //{
    //    MakeSingleTone();
    //    enemyPool = new Queue<GameObject>();
    //}


    //public void InitializePool(int size = 10)
    //{
    //    enemyPool.Clear();
    //    for (int i = 0; i < size; i++)
    //        CreateNewEnemy();
    //}

    //private void CreateNewEnemy()
    //{
    //    GameObject enemyObject = Instantiate(enemyPrefab);
    //    EnemyMonster enemyScript = enemyObject.GetComponent<EnemyMonster>();
    //    enemyScript.Deactive();
    //    enemyPool.Enqueue(enemyObject);
    //}

    //public GameObject GetEnemy(Vector3 position)
    //{
    //    if(enemyPool.Count == 0)
    //    {
    //        CreateNewEnemy();
    //    }

    //    GameObject enemy = enemyPool.Dequeue();
    //    enemy.GetComponent<EnemyMonster>().Initalize(position);
    //    return enemy;
    //}

    //public void ReturnEnemy(GameObject obj, AudioClip clip)
    //{
    //    EffactPoolManager.instance.GetBombEffact(obj.transform.position, clip);
    //    enemyPool.Enqueue(obj);
    //    obj.GetComponent<EnemyMonster>().Deactive();
    //}
}
