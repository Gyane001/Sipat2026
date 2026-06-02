using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class BotaoMapa : MonoBehaviour
{
    [Header("Configuração")]
    public string nomeDaCena;
    [Header("Transição de Cena")]
    public Animator transition;
    public float transitionTime = 1f;

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(TrocarCena);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Corrigido: adicionado os parênteses ()
        StartCoroutine(IrParaMapas());
    }

    private IEnumerator IrParaMapas()
    {
        // 1. Faz a transição de cena e espera
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
        SceneManager.LoadScene(nomeDaCena);
    }


    public void TrocarCena()
    {
        // Inicia a corrotina
        StartCoroutine(RotinaTrocarCena());
    }

    private IEnumerator RotinaTrocarCena()
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(nomeDaCena);
    }
}