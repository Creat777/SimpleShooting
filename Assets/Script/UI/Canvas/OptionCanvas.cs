using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : Option
{
    public Text difficulty_ImageText;
    public Slider difficultySlider;

    void Start()
    {
        LoadSliderData();
    }

    public override void LoadSliderData()
    {
        base.LoadSliderData();
        // ���� ���� ���̵� �����̴��� ����
        if(difficultySlider!= null)
        {
            difficultySlider.value = GameManager.Instance.gameDifficulty;
        }
        
    }

    public override void ButtonOptionReset()
    {
        base.ButtonOptionReset();
        difficultySlider.value = 3.0f;
    }

    // �����̴����� ���� ���� �� ����
    public void DifficultyUpdate()
    {
        // �����̴������� ���� ���̵� �ݿ� �� ����
        int difficultyValue = (int)difficultySlider.value;
        difficulty_ImageText.text = $"���� ���̵� : {difficultyValue.ToString()}";
        GameManager.Instance.gameDifficulty = difficultyValue;
        PlayerInfoManager.Instance.SaveData(GameManager.Instance.gameDifficulyKey, difficultyValue);
        Debug.Log("gameDifficulty : " + difficultyValue);
    }
}
