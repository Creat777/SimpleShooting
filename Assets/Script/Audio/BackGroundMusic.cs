using UnityEngine;
using UnityEngine.Audio;

public class BackGroundMusic : GameAudio
{

    public static BackGroundMusic Instance { get; private set; }

    void MakeSingleTone()
    {
        if (Instance == null)
        {
            Instance = this; // 스크립트 컴포넌트
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
                Debug.Log("배경음악 실행중");
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
