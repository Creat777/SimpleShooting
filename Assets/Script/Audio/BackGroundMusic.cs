using UnityEngine;
using UnityEngine.Audio;

public class BackGroundMusic : GameAudio
{
    public AudioClip defaultMusic;
    public AudioClip bossMusic;
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
                audioSource.clip = defaultMusic;
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

    public void BossEnter()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = bossMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("audioSource == null");
        }
    }

    public void BossExit()
    {
        
        if (audioSource != null)
        {
            // 보스를 만난적 없으면 함수실행 취소
            if (audioSource.clip == defaultMusic) return;

            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = defaultMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("audioSource == null");
        }
    }
}
