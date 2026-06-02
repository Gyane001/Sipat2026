using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Configuração da Barreira")]
    [Tooltip("Marque se este objeto for a barreira final.")]
    public bool isBarrier = false;
    [Tooltip("Nome da cena para carregar se TODOS os NPCs forem vencidos.")]
    public string sceneToLoad;

    [Header("Configuração do NPC")]
    [Tooltip("Marque isso se este NPC faz parte do desafio (o jogador precisa acertar o quiz dele para passar).")]
    public bool fazParteDoQuiz = false;

    [Header("Status (Automático)")]
    public bool acertouQuiz = false;

    private bool playerInRange;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = true;

            if (isBarrier)
            {
                if (VerificarTodosOsQuizzes())
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    StartDialogue();
                }
            }
            else
            {
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void StartDialogue()
    {
        // CORREÇÃO: Null check antes de usar o DialogueManager
        var dm = DialogueManager.GetInstance();
        if (dm == null) return;
        if (dm.dialogueIsPlaying) return;

        dm.EnterDialogueMode(inkJSON, this);

        if (fazParteDoQuiz)
            StartCoroutine(DesativarAposDialogo());
    }

    private IEnumerator DesativarAposDialogo()
    {
        // CORREÇÃO: Null check dentro da condição do WaitUntil também
        yield return new WaitUntil(() =>
        {
            var dm = DialogueManager.GetInstance();
            return dm == null || !dm.dialogueIsPlaying;
        });

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        gameObject.SetActive(false);

    }

    // No topo da classe — lista compartilhada entre todos os NPCs
    public static List<DialogueTrigger> todosOsNPCs = new List<DialogueTrigger>();

    private void OnEnable() { todosOsNPCs.Add(this); }
    private void OnDisable() { todosOsNPCs.Remove(this); }

    private bool VerificarTodosOsQuizzes()
    {
        foreach (DialogueTrigger npc in todosOsNPCs)
        {
            if (npc == this) continue;
            if (npc.fazParteDoQuiz && !npc.acertouQuiz) return false;
        }
        return true;
    }
}
