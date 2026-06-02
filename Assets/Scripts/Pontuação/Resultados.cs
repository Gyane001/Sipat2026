using UnityEngine;
using TMPro; // Importante para o texto
using UnityEngine.SceneManagement;

public class Resultados : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI textoTitulo;
    [SerializeField] private TextMeshProUGUI textoDescricao;
    [SerializeField] private TextMeshProUGUI textoDesafio;
    [SerializeField] private TextMeshProUGUI textoContador;

    private void Start()
    {
        ExibirResultado();
    }

    public void ExibirResultado()
    {
        int acertos = GameData.totalWins;
        textoContador.text = $"Você acertou {acertos} questões de 9!";

        // Lógica de filtragem de perfil
        if (acertos <= 3)
        {
            textoTitulo.text = "Perfil Ruim";
            textoDescricao.text = "Você demonstrou que ainda existem lacunas importantes no seu conhecimento e nas suas atitudes relacionadas à segurança. Isso não significa falta de capacidade, mas sim uma oportunidade clara de aprendizado e mudança de comportamento.";
            textoDescricao.text += "\n\n Desafio: Reflita sobre suas respostas, busque aprender com os erros e transforme conhecimento em prática. Segurança é um hábito que pode (e deve) ser construído.";
        }

        else if (acertos <= 8)
        {
            textoTitulo.text = "Perfil Bom";
            textoDescricao.text = "Você demonstrou um bom nível de conhecimento e consciência em segurança, aplicando corretamente muitos conceitos importantes no seu dia a dia. Suas escolhas indicam responsabilidade e atenção aos riscos.";
            textoDescricao.text += "\n\n\nDesafio: Continue praticando e seja um exemplo positivo para quem está ao seu redor. \nSegurança é um compromisso contínuo.";
        }
        else
        {
            textoTitulo.text = "Perfil Especialista";
            textoDescricao.text = "Parabéns! Você acertou todas as respostas. Com isso você apresentou um nível superior de conhecimento, atitude e responsabilidade em segurança. Suas respostas mostram que você entende os riscos, age preventivamente e valoriza o cuidado com sua própria vida e com a dos outros.";
            textoDescricao.text += "\n\nDesafio: Continue sendo referência, compartilhe boas práticas e incentive a cultura de segurança. Quem cuida bem, inspira outros a cuidarem também.";
        }
    }
    
    public void ReiniciarJogo()
    {
        GameData.totalWins = 0; // Resetamos os pontos para começar de novo
        SceneManager.LoadScene("Menu"); // Substitua pelo nome da sua cena de menu
    }
}