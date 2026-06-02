using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [Header("Parâmetros de Diálogo")]
    [SerializeField] private float typingSpeed = 0.01f;

    private bool canContinueToNextLine = false;

    [Header("UI do Diálogo")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject continueIcon;

    [Header("UI de Escolhas")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private Text[] choicesText;

    private Story currentStory;
    private DialogueTrigger npcAtual;

    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string WIN_TAG = "win";
    public int scoreTotal = 0;

    private void Awake()
    {
        // CORREÇÃO: Se já existe uma instância de outra cena, destrói ESTA (a nova/duplicada)
        // e mantém a antiga. MAS como cada cena tem seu próprio painel de UI,
        // o correto é destruir a ANTIGA e ficar com a nova.
        if (instance != null && instance != this)
        {
            // A instância antiga pertence a uma cena destruída — descarta ela.
            // Isso garante que sempre usamos o DialogueManager da cena atual,
            // que tem referências válidas para o painel de UI correto.
            Destroy(instance.gameObject);
        }

        instance = this;

        // CORREÇÃO: Garante que dialogueIsPlaying começa como false na nova cena,
        // independente do estado da cena anterior.
        dialogueIsPlaying = false;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        HideChoices();
        if (continueIcon != null) continueIcon.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying) return;

        if (canContinueToNextLine
            && currentStory.currentChoices.Count == 0
            && (Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, DialogueTrigger npc)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        if (dialoguePanel != null) dialoguePanel.SetActive(true);

        npcAtual = npc;

        if (portraitAnimator != null) portraitAnimator.Play("default");

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (dialogueText != null) dialogueText.text = "";
        if (continueIcon != null) continueIcon.SetActive(false);
        HideChoices();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        if (dialogueText != null) dialogueText.text = "";
        HideChoices();
        if (continueIcon != null) continueIcon.SetActive(false);
        canContinueToNextLine = false;

        foreach (char letter in line.ToCharArray())
        {
            if (dialogueText != null) dialogueText.text += letter;

            if (typingSpeed > 0)
                yield return new WaitForSeconds(typingSpeed);
            else
                yield return null;
        }

        canContinueToNextLine = true;

        if (currentStory.currentChoices.Count > 0)
            DisplayChoices();
        else
            if (continueIcon != null) continueIcon.SetActive(true);
    }

    private void HideChoices()
    {
        foreach (GameObject choiceBtn in choices)
        {
            choiceBtn.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string cleanTag = tag.Trim();

            if (cleanTag == WIN_TAG)
            {
                if (npcAtual != null && !npcAtual.acertouQuiz)
                {
                    npcAtual.acertouQuiz = true;
                    GameData.totalWins++;
                }
                continue;
            }

            string[] splitTag = cleanTag.Split(':');
            if (splitTag.Length != 2) continue;

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case PORTRAIT_TAG:
                    if (portraitAnimator != null) portraitAnimator.Play(tagValue);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Erro: Mais escolhas no Ink do que botões na UI.");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;

            int choiceIndex = index;
            Button btn = choices[index].GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => MakeChoice(choiceIndex));

            index++;
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        if (choices.Length > 0 && choices[0].activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }
}
