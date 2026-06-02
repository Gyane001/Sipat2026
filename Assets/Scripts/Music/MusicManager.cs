using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;
    private AudioSource audioSource;

    private void Awake()
    {
        // Padrão Singleton: Garante que só existe UM gerenciador no jogo inteiro
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Não destrói ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Se já existir um, destrói o novo para não duplicar
            return;
        }

        // Pega o componente de áudio anexado a este objeto
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // GARANTIA DE LOOP:
        // Configura o AudioSource para repetir automaticamente assim que a música acabar
        if (audioSource != null)
        {
            audioSource.loop = true;
        }
    }

    public void TocarMusica(AudioClip novaMusica)
    {
        // Se a música pedida for nula, para o som (silêncio)
        if (novaMusica == null)
        {
            audioSource.Stop();
            return;
        }

        // Se a música pedida for a MESMA que já está tocando, não faz nada
        // (Isso impede que a música reinicie do zero ao trocar de cena)
        if (audioSource.clip == novaMusica && audioSource.isPlaying)
        {
            return;
        }

        // Se for uma música diferente, troca e toca
        audioSource.Stop();
        audioSource.clip = novaMusica;

        // Reforça o loop na hora do play
        audioSource.loop = true;
        audioSource.Play();
    }
}