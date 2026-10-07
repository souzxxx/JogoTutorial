using UnityEngine;

public class InimigoPatrulha : MonoBehaviour
{
    public Vector2[] pontos;
    public float velocidade = 2.5f;
    public float giro = 120f;

    private Rigidbody2D rb;
    private int indice;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (pontos.Length == 0 || GameController.Instance.Acabou)
        {
            return;
        }

        Vector2 alvo = pontos[indice];
        Vector2 novaPosicao = Vector2.MoveTowards(rb.position, alvo, velocidade * Time.fixedDeltaTime);
        rb.MovePosition(novaPosicao);
        rb.MoveRotation(rb.rotation + giro * Time.fixedDeltaTime);

        if (Vector2.Distance(novaPosicao, alvo) < 0.05f)
        {
            indice = (indice + 1) % pontos.Length;
        }
    }
}
