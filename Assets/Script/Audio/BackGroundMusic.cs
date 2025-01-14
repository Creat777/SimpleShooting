using UnityEngine;
using UnityEngine.Audio;

public class BackGroundMusic : GameAudio
{

    public static BackGroundMusic Instance { get; private set; }

    void MakeSingleTone()
    {
        if (Instance == null)
        {
            Instance = this; // ��ũ��Ʈ ������Ʈ
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Awake()
    {
        MakeSingleTone();
        volumeKey = "BackGroundMusic";
    }
    void Start()
    {
        if (audioSource != null)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
            else
            {
                Debug.Log("������� ������");
            }
        }
        else
        {
            Debug.LogError("audioSource == null");
        }

    }


    void Update()
    {
        
    }
}
