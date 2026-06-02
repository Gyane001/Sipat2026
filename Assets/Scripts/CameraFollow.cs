using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 1. Crie um campo público para arrastar o Jogador
    public Transform target; // "Transform" guarda a Posição, Rotação e Escala

    // 2. Crie uma variável para suavizar o movimento
    public float smoothing = 5f;

    // 3. Crie uma variável para guardar a "distância" (offset)
    private Vector3 offset;

    // Start é chamado no primeiro frame
    void Start()
    {
        // Calcula a distância inicial entre a câmera e o jogador
        // Posição da Câmera - Posição do Alvo (Player)
        offset = transform.position - target.position;
    }

    // FixedUpdate é melhor para seguir física (como o Rigidbody do Player)
    void FixedUpdate()
    {
        // 1. Descobre a posição ONDE a câmera DEVERIA estar
        //    (Posição atual do Alvo + a distância inicial)
        Vector3 targetCamPos = target.position + offset;

        // 2. Suaviza o movimento da câmera
        //    Interpola linearmente da Posição Atual (A) para a Posição Alvo (B)
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}