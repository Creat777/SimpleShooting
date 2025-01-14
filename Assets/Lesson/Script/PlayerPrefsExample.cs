using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    private void Start()
    {
        string path = string.Empty;

        path = $"{Application.persistentDataPath}"; // ���� ���
        Debug.Log($"path : {path}");

        SaveData("PlayerName", "UnityPlayer");
        SaveData("HighScore", 12345);
        // ���� �Ǵ� ������Ʈ������ Ȯ�� ����
        // ������Ʈ�� ������ -> HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\SimpleShooting


        string PlayerName = LoadData("PlayerName", "DefaultName");
        Debug.Log($"Player Name : {PlayerName}");
    }

    public void SaveData(string key, object value)
    {
        if(value is int)
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if(value is float)
        {
            PlayerPrefs.SetFloat(key, (float)value);
        }
        else if (value is string)
        {
            PlayerPrefs.SetString(key, (string)value);
        }
        else
        {
            Debug.LogError("�������� �ʴ� ������ Ÿ���Դϴ�.");
            return;
        }

        PlayerPrefs.Save();
        Debug.Log($"Data saved : {key} = {value}");
    }

    public int LoadData(string key ,int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public float LoadData(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public string LoadData(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    // Ư�� Ű�� �ش��ϴ� �����͸� �����ϴ� �Լ�
    public void DeleteData(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"Data Deleted : {key}");
        }
        else
        {
            Debug.Log($"No data found for key : {key}");
        }
    }

    // ��� �����͸� �����ϴ� �Լ�
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Succes : Reset all data");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            ResetAllData();

    }
}
