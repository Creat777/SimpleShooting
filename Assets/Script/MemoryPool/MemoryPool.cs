using System.Collections.Generic;
using UnityEngine;

public abstract class MemoryPool : MonoBehaviour
{
    //public static MemoryPool Instance { get; private set; }
    protected abstract void MakeSingleTone();
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

    public GameObject Prefab;

    protected Queue<GameObject> memoryPool;

    
    protected virtual void Awake()
    {
        MakeSingleTone();
        memoryPool = new Queue<GameObject>();
    }


    public virtual void InitializePool(int size = 10)
    {
        memoryPool.Clear();
        for (int i = 0; i < size; i++)
            CreateNewObject();
    }

    protected virtual void CreateNewObject()
    {
        GameObject obj = Instantiate(Prefab);
        if (obj != null)
        {
            obj.SetActive(false);

            if (memoryPool != null)
            { 
                memoryPool.Enqueue(obj); 
            }
            else
            {
                Debug.LogError("�޸�Ǯ(Queue) �ʱ�ȭ �ȵ���");
            }
        }
        else
        {
            Debug.LogError("�������� ����");
        }
        
    }

    public virtual GameObject GetObject(Vector3 position)
    {
        if (memoryPool != null)
        {
            if (memoryPool.Count == 0)
            {
                CreateNewObject();
            }

            GameObject obj = memoryPool.Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            return obj;
        }
        else
        {
            Debug.LogError("memoryPool == null");
            return null;
        }
    }


    public virtual void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        memoryPool.Enqueue(obj);
    }
}
