using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ChoiceSet
{
    public int dialogueIndex; // 이 선택지가 해당하는 대사의 인덱스
    public string[] choicesArray;  // 선택지 배열
    public int[] nextDialogueIndex;  // 각 선택지가 선택되었을 때 이동할 대사 인덱스 배열
}

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public Sprite NPCPortrait;

    public GameObject choicePanel;
    public Button[] choiceButtons;
    public string[] dialogueLines; // 일반 대사 배열

    public List<ChoiceSet> choices; // 선택지 관리
    public GameObject shopPanel; // 상점 UI 패널

    private int currentLineIndex = 0;
    private bool isPlayerNear = false;
    private bool isChoosing = false; // 선택지를 고를 때 E 키 비활성화

    // 대화 종료가 필요한 대사 인덱스를 관리하는 리스트
    public List<int> endDialogueIndices = new List<int>();

    void Start()
    {
        choicePanel.SetActive(false);

        foreach (Button button in choiceButtons)
        {
            if (button != null)
            {
                ColorBlock colors = button.colors;
                colors.highlightedColor = new Color(0.8f, 0.8f, 0.8f);
                button.colors = colors;

                button.interactable = true;
                button.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("[Start] choiceButtons 배열 내의 버튼이 null입니다!");
            }
        }
    }

    void Update()
    {
        // E 키로 대화 진행하기
        if (isPlayerNear && !isChoosing && Input.GetKeyDown(KeyCode.E)) // 선택지 화면에서는 E 키를 비활성화
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

    // 대화 시작
    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        portraitImage.sprite = NPCPortrait;
        ShowDialogue();
    }

    // 대사 보여주기
    void ShowDialogue()
    {
        if (currentLineIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueLines[currentLineIndex];

        // 현재 대사에 선택지가 있는지 확인 후 출력
        ChoiceSet choiceSet = choices.Find(c => c.dialogueIndex == currentLineIndex);

        if (choiceSet != null && choiceSet.choicesArray.Length > 0)
        {
            ShowChoices(choiceSet.choicesArray);
        }
        else
        {
            choicePanel.SetActive(false); // 선택지가 없으면 선택지 패널 비활성화
            isChoosing = false; // 선택지가 없으면 E 키로 대화 가능
        }
    }
    // ⏩ 대화 진행 (E 키로 다음 대사 출력)
void NextDialogue()
{
    if (endDialogueIndices.Contains(currentLineIndex))
    {
        Debug.Log($"[NextDialogue] {currentLineIndex}번 대사에서 E 입력으로 대화 종료");
        EndDialogue();
        return;
    }

    if (currentLineIndex + 1 >= dialogueLines.Length)
    {
        EndDialogue();
        return;
    }

    currentLineIndex++;
    ShowDialogue();
}
    // 대화 종료
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        currentLineIndex = 0;
        isChoosing = false; // 대화 종료 시 E키 활성화
    }

    // 선택지 출력
    void ShowChoices(string[] choiceTexts)
    {
        if (choicePanel == null || choiceButtons == null || choiceTexts == null)
        {
            Debug.LogError("[ShowChoices] 필수 요소가 누락되었습니다!");
            return;
        }

        choicePanel.SetActive(true);
        isChoosing = true; // 선택지가 보이는 동안 E키 비활성화

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choiceTexts.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = choiceTexts[i];
                    buttonText.color = Color.white;
                }

                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => SelectChoice(index));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // 선택된 선택지 처리
    void SelectChoice(int choiceIndex)
    {
        choicePanel.SetActive(false);

        ChoiceSet choiceSet = choices.Find(c => c.dialogueIndex == currentLineIndex);
        if (choiceSet == null || choiceIndex < 0 || choiceIndex >= choiceSet.choicesArray.Length)
        {
            Debug.LogWarning($"[SelectChoice] 선택지 인덱스({choiceIndex})가 범위를 벗어났습니다. 자동으로 다음 대사 진행.");
            return;
        }

        Debug.Log($"[SelectChoice] 선택된 선택지 인덱스: {choiceIndex}");
        Debug.Log($"[SelectChoice] 선택된 선택지 텍스트: {choiceSet.choicesArray[choiceIndex]}");

        // 선택한 대사의 인덱스 가져오기
        int nextIndex = (choiceSet.nextDialogueIndex.Length > choiceIndex) ? choiceSet.nextDialogueIndex[choiceIndex] : -1;

        if (choiceSet.choicesArray[choiceIndex] == "상점 열기") // 상점 열기 선택지 처리
        {
            OpenShop();  // 상점 열기 함수 호출
            return; // 대화 진행을 멈추고 상점만 엽니다.
        }

        if (nextIndex >= 0 && nextIndex < dialogueLines.Length)
        {
            // 선택한 **답변을 먼저 출력**
            currentLineIndex = nextIndex;
            ShowDialogue();

            // 🔹 이 상태에서 E를 누르면 종료되도록 설정
            if (endDialogueIndices.Contains(currentLineIndex))
            {
                Debug.Log($"[SelectChoice] {currentLineIndex}번 대사 이후 E 입력 시 종료 설정");
                return; // 다음 대사로 넘어가지 않음
            }
        }
        else
        {
            EndDialogue();
        }

        isChoosing = false;
    }
    
    // 상점 열기
    void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
            Debug.Log("[OpenShop] 상점 UI를 엽니다.");
        }
        else
        {
            Debug.LogError("[OpenShop] shopPanel이 할당되지 않았습니다!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            EndDialogue();
        }
    }
}