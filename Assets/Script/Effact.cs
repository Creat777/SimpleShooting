using UnityEngine;
using UnityEngine.Audio;

public class Effact : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnEnable()
    {

    }

    public void EffectEnd()
    {
        //Debug.Log("����Ʈ ����");
        EffactPoolManager.Instance.ReturnObject(gameObject);
    }

    public void SetAble(Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos;
    }
}
