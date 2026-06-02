using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapaManager_1 : MonoBehaviour
{
    public bool isBarrier = false;

    [Header("Transição de Cena")]
    public Animator transition;
    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Corrigido: adicionado os parênteses ()
        StartCoroutine(RotinaTrocarCaminhos());
    }

    private IEnumerator RotinaTrocarCaminhos()
    {
        // 1. Faz a transição de cena e espera
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        // 2. Checa o nome da cena e carrega a próxima
        string cenaAtual = SceneManager.GetActiveScene().name;

        if (cenaAtual == "PF_1")
        {
            SceneManager.LoadScene("Adm");
        }
        else if (cenaAtual == "Adm")
        {
            SceneManager.LoadScene("Adm1");
        }
        else if (cenaAtual == "Adm1")
        {
            SceneManager.LoadScene("FimDeJogo");
        }
    }
}