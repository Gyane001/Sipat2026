using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class PauseManager : MonoBehaviour
{
    [Header("Referências")]
    public GameObject painelPause; // Arraste o PainelPause aqui

    // Variável para saber se está pausado
    public static bool isPaused = false;

    void Update()
    {
        // Funcionalidade para PC (Tecla ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // --- Funções que os Botões vão chamar ---

    public void PauseGame()
    {
        painelPause.SetActive(true); // Mostra a tela
        Time.timeScale = 0f;         // CONGELA o tempo
        isPaused = true;
    }

    public void ResumeGame()
    {
        painelPause.SetActive(false); // Esconde a tela
        Time.timeScale = 1f;          // DESCONGELA o tempo
        isPaused = false;
    }

    public void LoadMenu()
    {
        // IMPORTANTE: O tempo precisa voltar ao normal antes de mudar de cena!
        // Se não, o menu principal vai carregar "congelado".
        Time.timeScale = 1f;
        isPaused = false;
        
        // Carrega a cena de índice 0 (que definimos como o Menu Principal)
        SceneManager.LoadScene(0);
    }

    public void LoadMapas()
    {
        Time.timeScale = 1f;
        isPaused = false;
        
        SceneManager.LoadScene(4);
    }
}