using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialog : MonoBehaviour
{
    public RectTransform arrowImageTrans;
    public Text dialogueText;
    public float typingSpeed;
    private string currentDialogue;
    private string[] TextArr; // �ؽ�Ʈ���Ͽ��� �ε�
    private int TextIndex;
    bool isTyping;

    // csv�� ��� ������ �غ���

    public void TextLoading()
    {
        TextAsset Text = Resources.Load<TextAsset>("PopupScript");
        if (Text != null)
        {
            TextArr = Text.text.Split('\n');
        }
        else
        {
            Debug.LogError("Text���� �ε��� �����߽��ϴ�.");
        }
    }

    public void Button_NextDialogue()
    {
        ButtonClickAudio.Instance.PlayClip();
        // Ÿ����
        if (isTyping == false)
        {
            if (TextIndex < TextArr.Length)
            { 
                // �ؽ�Ʈ ��ȯ
                currentDialogue = TextArr[TextIndex++];

                // �ؽ�Ʈ ���������� ���̰���
                StartCoroutine(TypeDialogue(currentDialogue));

                // ������ ��ȭ���� ���� ��ȭ �̹��� ����
                if(TextIndex == TextArr.Length)
                    arrowImageTrans.gameObject.SetActive(false);
                return;
            }
            else
            {
                //Debug.Log("�ؽ�Ʈ ��� �Ϸ��");
                closeBtn();
            }
        }
        else
        {
            isTyping = false;
        }
    }

    

    void Start()
    {
        isTyping = false;
        TextIndex = 0;
        TextLoading();

        // ȭ��ǥ �̹��� �ִϸ��̼�
        ArrowDoTween();

        // ��ȭâ ����
        currentDialogue = TextArr[TextIndex++];
        StartCoroutine(TypeDialogue(currentDialogue));
    }

    

    private void ArrowDoTween()
    {
        // doTween������ ���� ����
        Vector3 targetScale = Vector3.one * 1.2f; // Ŀ���� ũ��
        float duration = 0.5f; // �ִϸ��̼� ���� �ð�

        // DOTween Sequence ����
        Sequence sequence = DOTween.Sequence();

        // ���� ũ�� ����
        Vector3 originalScale = arrowImageTrans.localScale;
        sequence.Append(arrowImageTrans.DOScale(targetScale, duration)) // Ŀ���� �ִϸ��̼�
                .Append(arrowImageTrans.DOScale(originalScale, duration)) // ���� �ִϸ��̼�
                .SetLoops(-1); // ���� �ݺ�
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach(char letter in dialogue.ToCharArray())
        {
            if (isTyping == true) // �ٽ� ��ư�� �ȴ������� ���ڰ� �ϳ��� ��Ÿ��
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(1 / typingSpeed);
            }
            else // Ÿ���� ���� ȭ���� Ŭ���ϸ� Ÿ������ ���߰� ��ü ������ ������
            {
                dialogueText.text = dialogue;
                break;
            }
        }
        isTyping = false;
    }

    public void closeBtn()
    {
        ButtonClickAudio.Instance.PlayClip();
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

}
