using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CsvManager
{
    public string csvFileName;
    public List<List<float>> positionCsv;
    public bool isDone;
    public float doneDelay;
   

    public CsvManager(string csvName = null, float delay = 1)
    {
        positionCsv = new List<List<float>>();
        isDone = false;

        csvFileName = csvName;
        doneDelay = delay;
    }
}

public class GameManager : MonoBehaviour
{
    // edit���� ����
    public int gameSpeed;
    [SerializeField] private float MaxTime;
    public float MaxTime_ { get => MaxTime; private set => MaxTime = value; }

    //[SerializeField] private float MaxTime;
    //public float MaxTime_
    //{
    //    get { return MaxTime; }
    //    private set { MaxTime = value; }
    //}

    // ��ũ��Ʈ���� ����
    public int defualtGameSpeed;
    public float gameTime { get; set; }
    public bool isGameStart = false;
    public bool isGamePause = false;
    public bool isBossExist;
    private bool isItemDeley {  get; set; }

    public int gameDifficulty { get; set; }
    public string gameDifficulyKey { get; set; }
    public string BackgroundVolumeKey { get; set; }
    public string EffctVolumeKey { get; set; }
    public string[] playerRankKeys {  get; set; }
    CsvManager[] EnemyCsvManagerArr; // csv����(enemy) ������ ���� ��ü
    CsvManager[] ItemCsvManagerArr; // csv����(item) ������ ���� ��ü
    public string playerID;

    public GameObject EnemyBoss;

    Dictionary<string, float> rankerMap;
    

    // MainInitializerSet���� ����
    public GameObject canvas;
    public GameObject scorePanel;

    //�̱��� ��ü
    public static GameManager Instance { get; private set; }
    void MakeSingleTone()
    {
        if (Instance == null)
        {
            Instance = this; // ��ũ��Ʈ ������Ʈ
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        // �⺻ �Ӽ� �ʱ�ȭ
        gameTime = 0;
        gameSpeed = defualtGameSpeed;
        isGameStart = true;
        isGamePause = false;

        if(canvas  != null)
        { 
            canvas.GetComponent<MainCanvas>().menuButton.SetActive(false);
            canvas.GetComponent<MainCanvas>().UI_Box.SetActive(true);
        }
        else
        {
            Debug.LogError("canvas ������ �ȵ���");
        }
    }

    public void LoadPlayerSetting()
    {
        // ���� ���̵� �ҷ�����
        int loadDifficulty = PlayerInfoManager.Instance.LoadData(gameDifficulyKey, 3);
        gameDifficulty = loadDifficulty;
    }

    public void GameOver()
    {
        isGameStart = false;
        canvas.GetComponent<MainCanvas>().menuButton.SetActive(true);
        canvas.GetComponent<MainCanvas>().UI_Box.SetActive(false);

        RankScore();
    }

    public List<string> GetOrderedRanker(float newScore)
    {
        // ���� ��Ͽ� ���� ������ �߰�
        string[] RankersID = PlayerInfoManager.Instance.GetRankersID(playerRankKeys);
        float[] RankerScores = PlayerInfoManager.Instance.GetRankScores(RankersID);
        
        // dict �ʱ�ȭ �� ��Ŀ�� ����
        rankerMap.Clear();
        for (int i = 0; i < RankersID.Length; i++)
        {
            if(i< RankerScores.Length)
            {
                rankerMap.TryAdd(RankersID[i], RankerScores[i]);
            }
            else
            {
                Debug.LogError("RankerScores out of range");
            }
            
        }

        if(rankerMap.ContainsKey(playerID))// ���� ��Ŀ�� �ش� ���̵� �ִ� ���
        {
            if (rankerMap[playerID] < newScore)  //���ο� ������ �� ũ�� �ش� ������ ����
                rankerMap[playerID] = newScore;
        }
        else // ���� ��Ŀ�� �ش� ���̵� ���� ���
        {
            // ���� ��Ͽ� ���ο� ���� �߰�
            rankerMap.Add(playerID, newScore);
        }
        

        // �������� ���ĵ� Ű�� ã��
        List<string> sortedKeys = rankerMap
            .OrderBy(pair => pair.Value) // ��(value)�� �������� ����
            .Select(pair => pair.Key)   // ���ĵ� Ű(key)�� ������
            .ToList();

        sortedKeys.RemoveAt(0); // ���� ���� �� ����
        sortedKeys.Reverse(); // �������� ����

        return sortedKeys;
    }

    public void RankScore()
    {
        // ���� ��Ͽ� ���ο� ���� �߰�
        float totalScore = scorePanel.GetComponent<Score>().totalScore;
        

        // ���� �ְ����� ����
        if (PlayerInfoManager.Instance.LoadData(playerID, 0f) < totalScore)
        {
            PlayerInfoManager.Instance.SaveData(playerID, totalScore);
        }

        // ��Ŀ ������Ʈ
        string[] newRankersID = GetOrderedRanker(totalScore).ToArray<string>();

        // ����� ��Ŀ���� ����� ���ʴ�� ����
        PlayerInfoManager.Instance.ResetAllRank(playerRankKeys);
        PlayerInfoManager.Instance.SetNewRank(playerRankKeys, newRankersID);

        //����� ����Ǿ����� Ȯ��
        float[] rankScore = PlayerInfoManager.Instance.GetRankScores(newRankersID);
        for (int i = 0; i < rankScore.Length; i++)
        {
            Debug.Log($"{playerRankKeys[i]}�� ���� : {rankScore[i]}");
        }
    }
    private void Awake()
    {
        MakeSingleTone();

        // �ִ�ð� ����
        MaxTime_ = 60;

        // ���� ����
        isBossExist = false;

        // ��� �������� ��ġ�� �ʵ��� �����̸� ����
        isItemDeley = false;

        // ����Ʈ �ӵ��� ����
        defualtGameSpeed = gameSpeed = 8;
        
        // ���� Ű ����
        gameDifficulyKey = "option_GameDifficulty";
        BackgroundVolumeKey = "option_BackgroundVolumeKey";
        EffctVolumeKey = "option_EffctVolumeKey";
        playerRankKeys = new string[3];
        rankerMap = new Dictionary<string, float>();
        for (int i = 0; i < 3; i++)
        {
            playerRankKeys[i] = $"PlayerRank{i}";
        }
    }

    void Start()
    {
        LoadPlayerSetting();
    }

    public void InitCsvManager()
    {
        EnemyCsvManagerArr = new CsvManager[2];
        EnemyCsvManagerArr[0] = new CsvManager("Enemy_Carrier", 3.0f);
        EnemyCsvManagerArr[1] = new CsvManager("Enemy_Shooter", 5.0f);

        foreach (var CsvManager in EnemyCsvManagerArr)
        {
            TextAsset csvFile = Resources.Load<TextAsset>(CsvManager.csvFileName);
            PositionCsvLoading(csvFile, CsvManager.positionCsv);
            //CheckCsv(enemyCsvManager);
        }


        ItemCsvManagerArr = new CsvManager[3];
        ItemCsvManagerArr[0] = new CsvManager("Item_Coin_1", 5.0f+3.0f);
        ItemCsvManagerArr[1] = new CsvManager("Item_Coin_2", 5.0f);
        ItemCsvManagerArr[2] = new CsvManager("Item_Coin_3", 5.0f + 3.0f);

        foreach (var CsvManager in ItemCsvManagerArr)
        {
            TextAsset csvFile = Resources.Load<TextAsset>(CsvManager.csvFileName);
            PositionCsvLoading(csvFile, CsvManager.positionCsv);
        }
    }


    private void PositionCsvLoading(TextAsset csvFile, List<List<float>> positionCsv)
    {
        if (csvFile != null)
        {
            // csv���� x��ǥ �������� �ο�
            float randomValue = UnityEngine.Random.Range(-2f, 3f);

            // csv������ \n ���ڸ� �������� ����
            string[] rows = csvFile.text.Split('\n');
            foreach (string row in rows) // ������ floatList�� �ٲ㼭 csv ������ ����
            {
                string[] fileds = row.Split(','); // ,�� �������� ����
                List<float> posCsv = new List<float>(fileds.Length); // ũ�⸦ �̸� ����

                foreach (string filed in fileds) // �� ���Ҵ� x, y, z ����
                {
                    try
                    {
                        if(filed == "random") // x��ǥ���� ����
                        {
                            posCsv.Add(randomValue);
                        }
                        else
                        {
                            posCsv.Add(float.Parse(filed));
                        }
                    }
                    catch (FormatException)
                    {
                        Debug.LogError($"format�� ���� ����: {filed}"); // ���ܰ� �߻��� ��� ��ŵ
                    }
                }
                positionCsv.Add(posCsv);
                //Debug.Log("csv���� Load����");
            }
        }
        else
        { 
            Debug.Log("csv���� Load����");
        }
    }

    private void CheckCsv(CsvManager csvManager)
    {
        int row_num = 0;
        Debug.Log(csvManager.csvFileName + " : ");
        foreach (var row in csvManager.positionCsv)
        {
            Debug.Log($"[ {row_num + 1} ]��");
            int field_num = 0;
            foreach (var field in row)
            {
                Debug.Log($"{++field_num}�� " + field);
            }
            row_num++;
        }
    }

    private Vector3 FloatListToVector3(List<float> list)
    {
        Vector3 position = Vector3.zero;
        if (list.Count == 3)
        {
            position = new Vector3(list[0], list[1], list[2]);
            //Debug.Log(position);
        }
        else
        {
            Debug.Log("list to position ����");
        }
        return position;
    }

    void Update()
    {
        if (isGameStart && isGamePause == false)
        {
            gameTime += Time.deltaTime; // ���ӽð� üũ

            if(gameTime < MaxTime/3)
                ManageEnemyCsvObject(); // csv������ ����ϴ� ��ü�� ����

            // �������� ��ġ�� �ʰ� ����
            if(isItemDeley == false && gameTime > 5f)
                StartCoroutine(ManageItemCsvObject());

            if(isBossExist == false && gameTime > MaxTime / 3)
            {
                EnemyBoss.SetActive(true);
                // ��ŸƮ ������ �ڷ�ƾ
                isBossExist = true;
            }
        }
        

        if (gameTime > MaxTime && isGameStart)
        {
            Debug.LogWarning($"gameTime : {gameTime} > MaxTime : {MaxTime}");
            GameOver();
        }
            
    }

    private void ManageEnemyCsvObject()
    { 
        // ���ʹ� ���ĵ� �������
        foreach (var csvManager in EnemyCsvManagerArr)
        {
            if (!csvManager.isDone)
            {
                // ���� �ð����� ��ȯ
                StartCoroutine(CsvCount(csvManager));
            }
        }

        
    }

    IEnumerator ManageItemCsvObject()
    {
        isItemDeley = true;

        // ������ ���� ����
        int size = ItemCsvManagerArr.Length;
        int index = UnityEngine.Random.Range(0, size);
        if (ItemCsvManagerArr[index].isDone == false) // �������� �������� ������
        {
            StartCoroutine(CsvCount(ItemCsvManagerArr[index]));
            yield return new WaitForSeconds(ItemCsvManagerArr[index].doneDelay);
        }
        isItemDeley = false;
    }

    IEnumerator CsvCount(CsvManager CsvManager)
    {
        string choice = CsvManager.csvFileName.Split('_')[0];
        if(choice == "Enemy")
        {
            CsvManager.isDone = true;
            PositioningCsv(CsvManager);
            float delayDifficulty = (1.0f / 2.0f) * (gameDifficulty - 1) + 1; // ���� ���� {1.0, 1.5, 2.0, 2.5, 3.0}�� ����
            yield return new WaitForSeconds(CsvManager.doneDelay / delayDifficulty); // ���̵��� �������� �� ���� ��ȯ��
            Debug.Log($"���̵��� {gameDifficulty}�� �� ���� ��ȯ ������ : " + CsvManager.doneDelay / delayDifficulty);
            CsvManager.isDone = false;
        }
        else if (choice == "Item")
        {
            PositioningCsv(CsvManager);
        }
    }




    private void PositioningCsv(CsvManager CsvManager)
    {
        if(CsvManager == null)
        {
            Debug.LogError("CsvManager ��ü�� �������� ����");
            return;
        }
        
        if (CsvManager.positionCsv.Count != 0)
        {
            string fileFirstName = CsvManager.csvFileName.Split("_")[0];

            // Enemy �����̸�
            if (fileFirstName == "Enemy") 
            {
                int spawnIndex = UnityEngine.Random.Range(0, CsvManager.positionCsv.Count);
                Vector3 Position = FloatListToVector3(CsvManager.positionCsv[spawnIndex]);
                GameObject enemy;
                if (CsvManager.csvFileName == "Enemy_Carrier")
                {
                    enemy = EnemyPoolCarrier.Instance.GetObject(Position);
                }
                else if (CsvManager.csvFileName == "Enemy_Shooter")
                {
                    enemy = EnemyPoolShooter.Instance.GetObject(Position);
                }
                else // ã�� �̸��� ���� ��
                {
                    Debug.LogWarning("EnemydefaultName");
                }
                //Debug.Log($"Spawned {enemy.name} at {Position}");
            }

            // Item�����̸�
            else if(fileFirstName == "Item")
            {
                GameObject item;
                if (CsvManager.csvFileName == "Item_Coin_1" ||
                    CsvManager.csvFileName == "Item_Coin_2" ||
                    CsvManager.csvFileName == "Item_Coin_3")
                {
                    for (int i = 0; i < CsvManager.positionCsv.Count; i++)
                    {
                        Vector3 position = FloatListToVector3(CsvManager.positionCsv[i]);
                        item = CoinMemoryPool.Instance.GetObject(position);
                    }
                }
                else // ã�� �̸��� ���� ��
                {
                    Debug.LogWarning("defaultName");
                }
                //Debug.Log($"Spawned {enemy.name} at {spawnPosition}");
            }



        }
            
    }

}
