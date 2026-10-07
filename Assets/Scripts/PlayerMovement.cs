using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float velocidadeDash = 13f;
    public float duracaoDash = 0.15f;
    public float staminaMaxima = 100f;
    public float custoDash = 40f;
    public float recuperacaoStamina = 22f;
    public float tempoInvencivel = 1.5f;
    public float forcaEmpurrao = 9f;

    public AudioClip somColeta;
    public AudioClip somRaio;
    public AudioClip somDano;
    public AudioClip somDash;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer sprite;
    private InputAction moveAction;
    private InputAction dashAction;
    private Vector2 movement;
    private Vector2 direcaoDash;
    private Vector2 direcaoEmpurrao;
    private float stamina;
    private float fimDash;
    private float fimEmpurrao;
    private float fimInvencivel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        moveAction = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Jump");
        stamina = staminaMaxima;
        direcaoDash = Vector2.up;
    }

    void Update()
    {
        if (Bloqueado())
        {
            movement = Vector2.zero;
            return;
        }

        movement = moveAction.ReadValue<Vector2>();
        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }

        if (movement.sqrMagnitude > 0.01f)
        {
            direcaoDash = movement.normalized;
        }

        if (dashAction.WasPressedThisFrame() && stamina >= custoDash)
        {
            stamina -= custoDash;
            fimDash = Time.time + duracaoDash;
            audioSource.PlayOneShot(somDash);
        }

        if (Time.time >= fimDash)
        {
            stamina = Mathf.Min(staminaMaxima, stamina + recuperacaoStamina * Time.deltaTime);
        }

        GameController.Instance.AtualizarStamina(stamina / staminaMaxima);

        bool invencivel = Time.time < fimInvencivel;
        sprite.enabled = !invencivel || Mathf.Repeat(Time.time * 10f, 1f) > 0.5f;
    }

    void FixedUpdate()
    {
        Vector2 velocidade = movement * speed;

        if (Time.time < fimDash)
        {
            velocidade = direcaoDash * velocidadeDash;
        }

        if (Time.time < fimEmpurrao)
        {
            velocidade = direcaoEmpurrao * forcaEmpurrao;
        }

        rb.MovePosition(rb.position + velocidade * Time.fixedDeltaTime);
        rb.angularVelocity = 0f;

        if (velocidade.sqrMagnitude > 0.01f)
        {
            float angulo = Mathf.Atan2(velocidade.y, velocidade.x) * Mathf.Rad2Deg - 90f;
            rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, angulo, 720f * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            audioSource.PlayOneShot(somColeta);
            Destroy(other.gameObject);
            GameController.Instance.ColetarEstrela();
        }
        else if (other.CompareTag("Bonus"))
        {
            audioSource.PlayOneShot(somRaio);
            stamina = staminaMaxima;
            Destroy(other.gameObject);
            GameController.Instance.ColetarRaio();
        }
        else
        {
            VerificarInimigo(other);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        VerificarInimigo(other);
    }

    void VerificarInimigo(Collider2D other)
    {
        if (!other.CompareTag("Inimigo") || Time.time < fimInvencivel || GameController.Instance.Acabou)
        {
            return;
        }

        fimInvencivel = Time.time + tempoInvencivel;
        fimEmpurrao = Time.time + 0.15f;
        direcaoEmpurrao = (rb.position - (Vector2)other.transform.position).normalized;

        InimigoPerseguidor cacador = other.GetComponent<InimigoPerseguidor>();
        if (cacador != null)
        {
            cacador.Atordoar(tempoInvencivel);
        }
        audioSource.PlayOneShot(somDano);
        GameController.Instance.PerderVida();
    }

    bool Bloqueado()
    {
        return GameController.Instance.Acabou || GameController.Instance.Pausado;
    }
}
