using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
[Header("Transição de Cena")]
    public Animator transition;
    public float transitionTime = 1f;

    public void PlayGame(string nomeDaFase)
    {
        
        Debug.Log("Clique registrado: " + Time.realtimeSinceStartup);
        // Correção: Chama a corrotina passando o nome da fase
        StartCoroutine(CarregarCena(nomeDaFase));
    }

public void VoltarParaMenu()
    {
        // Certifique-se de que a classe GameData existe para isso não dar erro
        GameData.totalWins = 0; 
        Debug.Log("Game retornando ao menu!");
        StartCoroutine(CarregarCena("Menu"));
    }

    private IEnumerator CarregarCena(string nomeDaFase)
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(nomeDaFase);
    }
}