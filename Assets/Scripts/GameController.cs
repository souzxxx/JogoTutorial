using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float tempoInicial = 60f;
    public float bonusPorEstrela = 2f;
    public float bonusPorRaio = 8f;
    public int pontosPorEstrela = 100;
    public int pontosPorRaio = 50;
    public int pontosPorSegundoRestante = 10;
    public int vidasMaximas = 3;

    public Text pontosText;
    public Text tempoText;
    public Text estrelasText;
    public Image[] iconesVida;
    public Image barraStamina;
    public GameObject painelPausa;
    public GameObject botaoContinuar;
    public AudioSource sfx;
    public AudioClip somAlerta;

    public bool Acabou { get; private set; }
    public bool Pausado { get; private set; }
    public float TempoDecorrido { get; private set; }

    private float tempoRestante;
    private int vidas;
    private int ultimoAlerta = -1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        GameManager.Reiniciar();
        GameManager.TotalEstrelas = GameObject.FindGameObjectsWithTag("Coletavel").Length;
        tempoRestante = tempoInicial;
        vidas = vidasMaximas;
        painelPausa.SetActive(false);
        AtualizarHUD();
    }

    void Update()
    {
        if (Acabou)
        {
            return;
        }

        if (PausaPressionada())
        {
            if (Pausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }

        if (Pausado)
        {
            return;
        }

        tempoRestante -= Time.deltaTime;
        TempoDecorrido += Time.deltaTime;

        int segundo = Mathf.CeilToInt(tempoRestante);
        if (segundo <= 10 && segundo > 0 && segundo != ultimoAlerta)
        {
            ultimoAlerta = segundo;
            sfx.PlayOneShot(somAlerta);
        }

        if (tempoRestante <= 0f)
        {
            tempoRestante = 0f;
            Encerrar(false);
        }

        AtualizarHUD();
    }

    public void ColetarEstrela()
    {
        if (Acabou)
        {
            return;
        }

        GameManager.Estrelas++;
        GameManager.Pontos += pontosPorEstrela;
        tempoRestante += bonusPorEstrela;
        ultimoAlerta = -1;
        AtualizarHUD();

        if (GameManager.Estrelas >= GameManager.TotalEstrelas)
        {
            Encerrar(true);
        }
    }

    public void ColetarRaio()
    {
        if (Acabou)
        {
            return;
        }

        GameManager.Pontos += pontosPorRaio;
        tempoRestante += bonusPorRaio;
        ultimoAlerta = -1;
        AtualizarHUD();
    }

    public void PerderVida()
    {
        if (Acabou)
        {
            return;
        }

        vidas--;
        AtualizarHUD();

        if (vidas <= 0)
        {
            Encerrar(false);
        }
    }

    public void AtualizarStamina(float proporcao)
    {
        barraStamina.fillAmount = proporcao;
    }

    public void Pausar()
    {
        Pausado = true;
        Time.timeScale = 0f;
        painelPausa.SetActive(true);
        EventSystem.current.SetSelectedGameObject(botaoContinuar);
    }

    public void Continuar()
    {
        Pausado = false;
        Time.timeScale = 1f;
        painelPausa.SetActive(false);
    }

    public void IrParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }

    void Encerrar(bool vitoria)
    {
        Acabou = true;
        GameManager.Vitoria = vitoria;
        GameManager.TempoFinal = TempoDecorrido;
        GameManager.VidasRestantes = Mathf.Max(0, vidas);

        if (vitoria)
        {
            GameManager.Pontos += Mathf.CeilToInt(tempoRestante) * pontosPorSegundoRestante;
        }

        Invoke(nameof(CarregarFim), 0.8f);
    }

    void CarregarFim()
    {
        SceneManager.LoadScene("FimDeJogo");
    }

    void AtualizarHUD()
    {
        pontosText.text = "PONTOS " + GameManager.Pontos;
        tempoText.text = GameManager.FormatarTempo(tempoRestante);
        tempoText.color = tempoRestante <= 10f ? new Color(1f, 0.35f, 0.35f) : Color.white;
        estrelasText.text = "ESTRELAS " + GameManager.Estrelas + "/" + GameManager.TotalEstrelas;

        for (int i = 0; i < iconesVida.Length; i++)
        {
            iconesVida[i].enabled = i < vidas;
        }
    }

    bool PausaPressionada()
    {
        bool teclado = Keyboard.current != null && (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame);
        bool controle = Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame;
        return teclado || controle;
    }
}
