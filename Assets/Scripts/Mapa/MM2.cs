using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM2 : MonoBehaviour
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

        if (cenaAtual == "PF_2")
        {
            SceneManager.LoadScene("Fabrica");
        }
        else if (cenaAtual == "Fabrica")
        {
            SceneManager.LoadScene("FimDeJogo");
        }
    }
}