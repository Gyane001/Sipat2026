using UnityEngine;

public class ConfiguracaoCena : MonoBehaviour
{
    [Header("Qual música deve tocar AQUI?")]
    [Tooltip("Arraste a música desta fase. Deixe VAZIO se quiser silêncio.")]
    public AudioClip musicaDestaCena;

    void Start()
    {
        // Ao iniciar a cena, manda o DJ tocar a música escolhida (ou parar)
        if (MusicManager.instancia != null)
        {
            MusicManager.instancia.TocarMusica(musicaDestaCena);
        }
    }
}