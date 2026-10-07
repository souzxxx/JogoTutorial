using UnityEngine;

public class InimigoPerseguidor : MonoBehaviour
{
    public Vector2[] pontos;
    public float velocidadePatrulha = 1.5f;
    public float velocidadePerseguicao = 2.4f;
    public float aumentoPorSegundo = 0.02f;
    public float velocidadeMaxima = 4.2f;
    public float raioVisao = 4.5f;

    private Rigidbody2D rb;
    private Transform jogador;
    private int indice;
    private float fimAtordoado;

    public void Atordoar(float duracao)
    {
        fimAtordoado = Time.time + duracao;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            jogador = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (GameController.Instance.Acabou || Time.time < fimAtordoado)
        {
            return;
        }

        Vector2 alvo;
        float velocidade;

        if (jogador != null && Vector2.Distance(rb.position, jogador.position) < raioVisao)
        {
            alvo = jogador.position;
            velocidade = Mathf.Min(velocidadeMaxima, velocidadePerseguicao + aumentoPorSegundo * GameController.Instance.TempoDecorrido);
        }
        else if (pontos.Length > 0)
        {
            alvo = pontos[indice];
            velocidade = velocidadePatrulha;
            if (Vector2.Distance(rb.position, alvo) < 0.1f)
            {
                indice = (indice + 1) % pontos.Length;
            }
        }
        else
        {
            return;
        }

        Vector2 direcao = alvo - rb.position;
        if (direcao.sqrMagnitude > 0.0001f)
        {
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg + 90f;
            rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, angulo, 360f * Time.fixedDeltaTime));
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, alvo, velocidade * Time.fixedDeltaTime));
    }
}
