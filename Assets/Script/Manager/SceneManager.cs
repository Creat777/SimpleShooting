using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    // InitializerSet���� ����
    //public GameObject canvas;

    private void MakeSingleTone()
    {
        if (Instance == null)
        { 
            Instance = this;
            //Debug.Log($"singleTone��ü : {gameObject.name}");
        }
        else
        {
            Destroy(gameObject);
            //Debug.LogError($"��ü�� 2�� �̻��� �Ǿ� ���� ��ü�� �ı� : {gameObject.name}");
            return;
        }
        DontDestroyOnLoad(Instance);
    }
    private void Awake()
    {
        MakeSingleTone();
    }
    private void Start()
    {
        
    }

    
    
    public void Button_GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        BackGroundMusic.Instance.DefaultPlay();
    }

    public void Button_GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        BackGroundMusic.Instance.LetsPlay();
    }


    public void Button_CheckScore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Score");
        BackGroundMusic.Instance.DefaultPlay();
    }

    public void Button_Option()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Option");
        BackGroundMusic.Instance.DefaultPlay();
    }

    public void Button_LoginScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
        BackGroundMusic.Instance.DefaultPlay();
    }
}
