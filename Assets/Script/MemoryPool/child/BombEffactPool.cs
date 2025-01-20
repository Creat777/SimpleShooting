using UnityEngine;

public class BombEffactPool : MemoryPool
{
    public static BombEffactPool Instance {  get; private set; } // �̱���
    //public GameObject bombEffactPrefab;
    //private Queue<GameObject> BombEffactQueue;

    protected override void MakeSingleTone()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    //protected void Awake()
    //{
    //    MakeSingleTone();
    //    BombEffactQueue = new Queue<GameObject>();
    //}

    //public void Initialsized(int size = 10)
    //{
    //    BombEffactQueue.Clear();
    //    for (int i = 0; i < size; i++)
    //    {
    //        CreateBombEffact();
    //    }
    //}

    //public void CreateBombEffact()
    //{
    //    GameObject bombEffact = Instantiate(bombEffactPrefab);
    //    bombEffact.SetActive(false);
    //    if (BombEffactQueue != null)
    //    {
    //        BombEffactQueue.Enqueue(bombEffact);
    //    }
    //    else
    //    {
    //        Debug.LogError("BombEffactQueue�� null�Դϴ�.");
    //    }
    //}

    public override GameObject GetObject(Vector3 position)
    {
        if (memoryPool != null)
        {
            if (memoryPool.Count == 0)
            {
                CreateNewObject();
            }
            GameObject bombEffact = memoryPool.Dequeue();
            bombEffact.SetActive(true);
            bombEffact.transform.position = position;
            Effact effact = bombEffact.GetComponent<Effact>();
            if (effact != null)
            {
                effact.SetAble(position);
            }
            else
            {
                Debug.LogError("bombEffact.GetComponent<Effact>() ����");
            }
            return bombEffact;
        }
        else
        {
            Debug.LogError("BombEffactmemoryPool == null");
            return null;
        }
    }
    //public GameObject GetBombEffact(Vector3 pos)
    //{
    //    if (BombEffactQueue != null) { 
    //        if (BombEffactQueue.Count == 0)
    //        {
    //            CreateBombEffact();
    //        }
    //        GameObject bombEffact = BombEffactQueue.Dequeue();
    //        bombEffact.GetComponent<Effact>().SetAble(pos);
    //        return bombEffact;
    //    }
    //    else
    //    {
    //        Debug.LogError("BombEffactQueue�� null�Դϴ�.");
    //        return null;
    //    }
    //}

    //public void ReturnBombEffact(GameObject obj)
    //{
    //    obj.SetActive(false);
    //    BombEffactQueue.Enqueue(obj);
    //}
}
