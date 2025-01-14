using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour
{
    public GameObject ScorePanel;
    public GameObject button_BackToMenu;
    public GameObject button_ScoreClear;
    public GameObject[] ScoreTexts;
    
    
    float panelHeight;
    
    void Start()
    {
        //PanelPositioning();
        ScoreExpression();
    }

    public void ScoreExpression()
    {
        string[] RankersIDs = PlayerInfoManager.Instance.GetRankersID(GameManager.Instance.playerRankKeys);
        float[] RankersScores = PlayerInfoManager.Instance.GetRankScores(RankersIDs);
        // �����ǿ� ������ �Է�
        for (int i = 0; i < ScoreTexts.Length; i++)
        {
            Text textComp = ScoreTexts[i].GetComponentInChildren<Text>();
            if (textComp != null)
            {
                // �÷��̾� ���̵𿡼� �ʿ��� �͸� ����
                string playerName = "";
                if (RankersIDs[i].Split('_').Length >=2)
                {
                    playerName = RankersIDs[i].Split('_')[1];
                }
                else
                {
                    foreach (var ID in RankersIDs)
                    {
                        playerName = "NonePlayer";
                        Debug.LogError($"ID : {ID}");
                    }
                }

                if(playerName == "NonePlayer")
                {
                    textComp.text = $"{i + 1}��\n{playerName}";
                }
                else
                {
                    // ~�� Name(����)
                    textComp.text = $"{playerName}\nscore : {RankersScores[i].ToString("0")}";
                }
                
            }
            else
            {
                Debug.LogError("�������� ã�� �� ����");
            }
        }
    }

    public void ButtonScoreClear()
    {
        ButtonClickAudio.Instance.PlayClip();
        PlayerInfoManager.Instance.ResetAllRank(GameManager.Instance.playerRankKeys);
        ScoreExpression();
    }

    public void ButtonBackToMenu()
    {
        ButtonClickAudio.Instance.PlayClip();
        SceneManager.Instance.Button_GoToMenu();
    }
}
