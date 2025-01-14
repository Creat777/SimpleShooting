using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider BackgroundVolumeSlider;
    public Slider EffactVolumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadSliderData();
    }

    public virtual void LoadSliderData()
    {
        GameManager.Instance.LoadPlayerSetting();
        BackgroundVolumeSlider.value = BackGroundMusic.Instance.LoadVolumeData();
        EffactVolumeSlider.value = ButtonClickAudio.Instance.LoadVolumeData();
        
    }

    public void ButtonBackToMenu()
    {
        ButtonClickAudio.Instance.PlayClip();
        SceneManager.Instance.Button_GoToMenu();
    }

    public virtual void ButtonOptionReset()
    {
        ButtonClickAudio.Instance.PlayClip();
        // �� value�� �⺻������ �ʱ�ȭ
        BackgroundVolumeSlider.value = 0.8f;
        EffactVolumeSlider.value = 1.0f;
    }

    // �����̴����� ���� ���� �� ����
    public void BackgroundVolumeUpdate()
    {
        if (BackgroundVolumeSlider != null)
        {
            BackGroundMusic.Instance.VolumeUpdate(BackgroundVolumeSlider.value);
        }
        else
        {
            Debug.LogError("BackgroundVolumeSlider == null");
        }
    }

    // �����̴����� ���� ���� �� ����
    public void EffactVolumeUpdate()
    {

        if (EffactVolumeSlider != null)
        {
            ButtonClickAudio.Instance.VolumeUpdate(EffactVolumeSlider.value);
        }
        else
        {
            Debug.LogError("EffactVolumeSlider == null");
        }
    }

    // �����̴����� ���콺�� ��� �� ����
    public void EffactVolumeChanged()
    {
        ButtonClickAudio.Instance.PlayClip();
    }
}
