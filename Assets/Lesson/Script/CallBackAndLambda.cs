using UnityEngine;

public class CallBackAndLambda : MonoBehaviour
{
    void Start()
    {
        // �ݹ� �Լ�
        PerformCalculation1(
            10,
            12,
            OnPlayCallback1
            );

        // �ݹ� ���ν���
        PerformCalculation2(
            10,
            12,
            OnPlayCallback2
            );

        // �ݹ� ���ٽ�
        PerformCalculation3(
            10,
            12,
            (res1, res2) =>
            {
                return 0.0f;
            }
            );
    }


    // �Լ�
    void PerformCalculation1(int a, int b, System.Func<int,int, float> callback) // System.Action : �ݹ��Լ� Ÿ��
    {
        float c = callback?.Invoke(a, b) ?? 0f; // �Ű������� �Ѿ�� �ݹ��Լ��� ȣ���� �� ����.
        Debug.Log("�ݹ��Լ��� ��ȯ�� : " + c);

        // �Ǵ�
        if (callback != null)
        {
            c = callback.Invoke(a, b);
        }
        else
        {
            c = 0f;
        }
        Debug.Log("�ݹ��Լ��� ��ȯ�� : " + c);
    }
    float OnPlayCallback1(int res, int res2)
    {
        Debug.Log("�ݹ��Լ�ȣ��");
        return 0.5f;
    }


    //���ν���
    void PerformCalculation2(int a, int b, System.Action<int, int> callback) // System.Action : �ݹ��Լ� Ÿ��
    {
        callback?.Invoke(a, b); // �Ű������� �Ѿ�� �ݹ��Լ��� ȣ���� �� ����.
        int result = a + b;
    }
    void OnPlayCallback2(int res, int res2)
    {
        Debug.Log("�ݹ����ν���ȣ��");
    }

    void PerformCalculation3(int a, int b, System.Func<int, int, float> callback) // System.Action : �ݹ��Լ� Ÿ��
    {
        float c = callback?.Invoke(a, b) ?? 0f; // �Ű������� �Ѿ�� �ݹ��Լ��� ȣ���� �� ����.
        Debug.Log("�ݹ��Լ��� ��ȯ�� : " + c);

        // �Ǵ�
        if (callback != null)
        {
            c = callback.Invoke(a, b);
        }
        else
        {
            c = 0f;
        }
        Debug.Log("�ݹ��Լ��� ��ȯ�� : " + c);
    }

}
