using UnityEngine;

public class CoinMemoryPool : MemoryPool
{
    public static CoinMemoryPool Instance { get; private set; }
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
        // ���� �ű涧 ��ü�� �ı����� ������ ���� ������ �����Ҷ� ���ο� ��ü�� ���������.
        // �̱��濡�� �� ��� Destroy(gameObject);�� ���簡 �Ǵ� �һ�縦 ���� �� �ִ�.
    }

    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public override void ReturnObject(GameObject obj)
    {
        audioSource.volume = ButtonClickAudio.Instance.LoadVolumeData();
        audioSource.Play();
        obj.SetActive(false);
        memoryPool.Enqueue(obj);
    }
}
