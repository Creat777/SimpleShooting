using UnityEngine;
using System.Collections.Generic;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager Instance { get; private set; }

    private void MakeSingleTone()
    {
        if(Instance == null)
        { 
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Awake()
    {
        MakeSingleTone();
    }

    public void SaveData(string key, object value)
    {
        // �ش� key ����
        //PlayerPrefs.DeleteKey(key); 

        // �ش� key�� Value�� ����
        // ���� Ű�� ���δٸ� �ڷ����� �����ϴ� ��� �������� ����� �����͸� ��ȿ��
        if (value is int)
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if (value is float)
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
        //Debug.Log($"Data saved : {key} = {value}");
    }

    public int LoadData(string key, int defaultValue)
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
        if (PlayerPrefs.HasKey(key))
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

    public void ResetAllRank(string[] RankKeys)
    {
        foreach(var key in RankKeys)
        {
            DeleteData(key);
        }
    }

    public void SetNewRank(string[] RankKeys, string[] RankersID)
    {
        Debug.LogWarning("RankKeys�� ũ�� : "+RankKeys.Length);
        Debug.LogWarning("scoreValueList ũ�� : " + RankersID.Length);
        for (int i = 0; i < RankKeys.Length; i++)
        {
            // ���� �������� ����
            if (i<RankersID.Length) 
                SaveData(RankKeys[i], RankersID[i]);
        }
    }

    public float[] GetRankScores(string[] RankersIDs)
    {
        float[] rankScores = new float[RankersIDs.Length];
        for (int i = 0; i < RankersIDs.Length; i++)
        {
            rankScores[i] = LoadData(RankersIDs[i], 0f);
        }
        return rankScores;
    }

    public string[] GetRankersID(string[] RankKeys)
    {
        string[] PlayerNameOfRank = new string[RankKeys.Length];
        for(int i = 0;i < RankKeys.Length;i++)
        {
            // �α��� ���� �����ؼ� �÷����� ��ʰ� ������ �����Ⱚ�� ��ȯ�� ���� ����
            // ���� ��ĿŰ�� ������ �÷��̾�ID�� ��ȯ�Ǹ� �̰��� Ű�� Ȱ���� �� ������ ����
            PlayerNameOfRank[i] = PlayerPrefs.GetString(RankKeys[i], $"ID_NonePlayer_{i}");
        }
        return PlayerNameOfRank; ;
        
    }
    void Start()
    {
        
    }
}
