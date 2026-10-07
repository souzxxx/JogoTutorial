using UnityEngine;

public class Girar : MonoBehaviour
{
    public float velocidade = 60f;
    public float pulso = 0f;

    private Vector3 escalaInicial;

    void Start()
    {
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(0f, 0f, velocidade * Time.deltaTime);

        if (pulso > 0f)
        {
            transform.localScale = escalaInicial * (1f + Mathf.Sin(Time.time * 4f) * pulso);
        }
    }
}
