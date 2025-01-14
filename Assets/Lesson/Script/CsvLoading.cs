using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CsvLoading : MonoBehaviour
{
    private List<List<string>> csvData;
    void Start()
    {
        csvData = new List<List<string>>();
        TextAsset csvFile = Resources.Load<TextAsset>("example");
        if(csvFile != null)
        {
            // '\n' �� ���� ����
            // ','���� �� ���� ���Ҹ� ����
            string[] rows = csvFile.text.Split('\n');
            foreach (string row in rows)
            {
                string[]fields = row.Split(',');
                List<string> rowData = new List<string>(fields);
                csvData.Add(rowData);
            }

            // ���� Ȯ�� �Ǵ� ���
            int row_num = 0;
            foreach(List<string> row in csvData)
            {
                Debug.Log($"[ {row_num + 1} ]��");
                int field_num = 0;
                foreach(string field in row)
                {
                    switch(field_num)
                    {
                        case 0: Debug.Log($"{++field_num}�� " + int.Parse(field)); break;
                        case 1: Debug.Log($"{++field_num}�� " + int.Parse(field)); break;
                        case 2: Debug.Log($"{++field_num}�� " + float.Parse(field)); break;
                        case 3: Debug.Log($"{++field_num}�� " + float.Parse(field)); break;
                    }
                }
                row_num++;
            }
            
        }
        else
        {
            Debug.Log("������ �������� �ʽ��ϴ�.");
        }
    }

    void Update()
    {
        
    }
}
