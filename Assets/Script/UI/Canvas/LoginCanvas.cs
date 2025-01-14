using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LoginCanvas : MonoBehaviour
{
    // �ǳ�
    public GameObject LoginPanel;
    public GameObject ID_GeneratePanel;

    // �α��� ȭ��
    public Text StateOfLogin;
    public InputField inputID;
    public InputField inputPassword;

    // �������� ȭ��
    public Text StateOfGenerate;
    public InputField generateID;
    public InputField generatePassword;
    public InputField checkPassword;

    void Start()
    {
        BackToLoginPanel();
    }

    public void Button_BackToLoginPanel()
    {
        ButtonClickAudio.Instance.PlayClip();
        BackToLoginPanel();
    }

    private void BackToLoginPanel()
    {
        LoginPanel.SetActive(true);
        ID_GeneratePanel.SetActive(false);
        StateOfLogin.text = "���ӿ� �����Ͻ� ���� ȯ���մϴ�!";
        inputID.text = "";
        inputPassword.text = "";
    }

    public void Button_SwitchGeneratePanel()
    {
        ButtonClickAudio.Instance.PlayClip();
        LoginPanel.SetActive(false);
        ID_GeneratePanel.SetActive(true);
        StateOfGenerate.text = "���ϴ� ID�� ��й�ȣ�� �Է����ּ���";
        generateID.text = "";
        generatePassword.text = "";
        checkPassword.text = "";
    }

    public void Button_Login()
    {
        ButtonClickAudio.Instance.PlayClip();
        string password = PlayerInfoManager.Instance.LoadData(inputID.text, "nullPassword");

        if (password == "nullPassword")
        {
            StateOfLogin.text = "�������� �ʴ� ID�Դϴ�.";
        }
        else if (password == inputPassword.text)
        {
            // �α��� ����
            GameManager.Instance.playerID = $"ID_{inputID.text}";
            SceneManager.Instance.Button_GoToMenu();
        }
        else
        {
            StateOfLogin.text = "�α��ο� �����Ͽ����ϴ�. ��й�ȣ�� Ȯ�����ּ���";
        }
    }

    public void Button_GenerateID()
    {
        ButtonClickAudio.Instance.PlayClip();
        bool checkId = PlayerInfoManager.Instance.LoadData(generateID.text, "nullPassword") == "nullPassword";
        if(checkId) // ���� ���̵� ����
        {
            Debug.Log($"�Է� ID : {generateID.text}");
            Debug.Log($"�Է� PW : {generatePassword.text}");
            Debug.Log($"Ȯ�� PW : {checkPassword.text}");
            if (generatePassword.text == checkPassword.text)
            {
                PlayerInfoManager.Instance.SaveData(generateID.text, generatePassword.text);
                BackToLoginPanel();
            }
            else
            {
                StateOfGenerate.text = "��й�ȣ�� �ٽ� Ȯ�����ּ���";
            }
        }
        else
        {
            StateOfGenerate.text = "�̹� �����ϴ� ID�Դϴ�.";
        }
    }

    public void Button_QuitGame()
    {
        // ���� ����
        Application.Quit();

        // �����Ϳ����� ���� ���� ��� ���� ��� ����
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
