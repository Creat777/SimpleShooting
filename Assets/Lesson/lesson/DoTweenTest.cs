using UnityEngine;
using DG.Tweening;

public class DoTweenTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -490), 3f);
        //GetComponent<RectTransform>().DOScale(3.0f, 7.0f); // �����ϸ�
    }

    private void OnEnable()
    {
        // ȭ�� ������ �̵�
        GetComponent<RectTransform>().localPosition = new Vector2(0, -900);

        // ȭ�� �ۿ��� ������ �������� �̵�
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -490), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
