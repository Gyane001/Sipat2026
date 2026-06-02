using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 5f;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator animator;
    
    // --- NOVO: Referência para o som dos passos ---
    public AudioSource audioPassos;

    // Variáveis de Controle
    private float moveInput = 0f;
    private float buttonInput = 0f;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // Se você esqueceu de arrastar no Inspector, ele tenta pegar o do próprio objeto
        if (audioPassos == null)
            audioPassos = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ... (Lógica de bloqueio do diálogo continua aqui) ...
        if (DialogueManager.GetInstance() != null && DialogueManager.GetInstance().dialogueIsPlaying)
        {
            moveInput = 0f;
            rb.velocity = Vector2.zero;
            if (animator != null) animator.SetFloat("Speed", 0);
            
            // NOVO: Se entrar em diálogo, para o som imediatamente
            if (audioPassos.isPlaying) audioPassos.Stop();
            
            return;
        }

        // LER INPUTS
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        if (keyboardInput != 0)
            moveInput = keyboardInput;
        else
            moveInput = buttonInput;

        // VIRAR (Flip)
        if (moveInput > 0 && !isFacingRight) Flip();
        else if (moveInput < 0 && isFacingRight) Flip();

        // ANIMAÇÃO
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        // --- NOVA LÓGICA DE SOM ---
        ManageFootsteps();
    }

    void FixedUpdate()
    {
        if (DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    // --- NOVA FUNÇÃO PARA GERENCIAR O SOM ---
    void ManageFootsteps()
    {
        // Verifica se o jogador está se movendo (input diferente de 0)
        if (moveInput != 0)
        {
            // Se está se movendo e o som NÃO está tocando, dá Play
            if (!audioPassos.isPlaying)
            {
                audioPassos.Play();
            }
        }
        else
        {
            // Se parou de mover (input é 0), dá Stop
            audioPassos.Stop();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // --- FUNÇÕES UI ---
    public void OnPointerDownLeft() { buttonInput = -1f; }
    public void OnPointerDownRight() { buttonInput = 1f; }
    public void OnPointerUp() { buttonInput = 0f; }
}