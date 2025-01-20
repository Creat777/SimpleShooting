using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class EnemyPool : MemoryPool
{
    protected AudioSource audioSource { get; set; }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void CreateNewObject()
    {
        GameObject enemy = Instantiate(Prefab);
        
        if (memoryPool != null)
        {
            // 메모리풀에 속하는 객체는 직접적인 삭제가 있지 않는 이상 보존함
            DontDestroyOnLoad(enemy);

            EnemyMonster enemyScript = enemy.GetComponent<EnemyMonster>();
            if(enemyScript != null)
            {
                enemyScript.Deactive();
            }
            else
            {
                Debug.LogError("enemyScript를 찾을 수 없음");
            }

            memoryPool.Enqueue(enemy);
            //Debug.Log("enemyPool 객체가 존재함");
        }
        else
        {
            Debug.LogError("enemyPool 객체가 존재하지 않음");
        }
    }

    public override GameObject GetObject(Vector3 position)
    {
        if (memoryPool != null)
        {
            if (memoryPool.Count == 0)
            {
                CreateNewObject();
            }
            //Debug.Log(enemyPool.Count);
            GameObject enemy = memoryPool.Dequeue();
            enemy.GetComponent<EnemyMonster>().Initalize(position);
            return enemy;
        }
        else
        {
            Debug.LogError("enemyPool 객체가 존재하지 않음");
            return null;
        }
    }

    public override void ReturnObject(GameObject obj)
    {
        BombEffactPool.Instance.GetObject(obj.transform.position);
        audioSource.volume = ButtonClickAudio.Instance.LoadVolumeData();
        audioSource.Play();
        obj.GetComponent<EnemyMonster>().Deactive();
        memoryPool.Enqueue(obj);
    }

}
