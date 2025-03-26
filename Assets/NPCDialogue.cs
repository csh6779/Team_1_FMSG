using UnityEngine;
using TMPro;  // TextMeshPro 사용
using UnityEngine.UI;  // UI 이미지 사용

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;  // 대화 UI 패널
    public TextMeshProUGUI dialogueText; // 대화 텍스트
    public Image portraitImage; // NPC 초상화 이미지
    public Sprite lunaPortrait; // 루나 초상화 이미지

    public string[] dialogueLines;  // 대화 내용 배열
    private int currentLineIndex = 0;  // 현재 대화 진행 상태

    private bool isPlayerNear = false; // 플레이어가 NPC 근처에 있는지 여부

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialoguePanel.activeSelf)
            {
                StartDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        portraitImage.sprite = lunaPortrait; // 초상화 설정
        dialogueText.text = dialogueLines[currentLineIndex]; // 첫 번째 대사 출력
    }

    void NextDialogue()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex]; // 다음 대사 표시
        }
        else
        {
            dialoguePanel.SetActive(false); // 대화 종료
            currentLineIndex = 0;
        }
    }

    // 플레이어가 NPC 근처에 들어왔을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // 플레이어가 NPC 근처에서 멀어졌을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            dialoguePanel.SetActive(false);
            currentLineIndex = 0;
        }
    }
}