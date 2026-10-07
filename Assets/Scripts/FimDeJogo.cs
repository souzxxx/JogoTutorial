using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FimDeJogo : MonoBehaviour
{
    public Text tituloText;
    public Text resumoText;
    public GameObject botaoJogarNovamente;
    public AudioSource audioSource;
    public AudioClip somVitoria;
    public AudioClip somDerrota;

    void Start()
    {
        Time.timeScale = 1f;

        if (GameManager.Vitoria)
        {
            tituloText.text = "MISSÃO CUMPRIDA!";
            tituloText.color = new Color(1f, 0.85f, 0.2f);
            audioSource.PlayOneShot(somVitoria);
        }
        else
        {
            tituloText.text = GameManager.VidasRestantes <= 0 ? "NAVE DESTRUÍDA" : "OXIGÊNIO ESGOTADO";
            tituloText.color = new Color(1f, 0.4f, 0.4f);
            audioSource.PlayOneShot(somDerrota);
        }

        resumoText.text =
            "PONTOS: " + GameManager.Pontos + "\n" +
            "ESTRELAS: " + GameManager.Estrelas + "/" + GameManager.TotalEstrelas + "\n" +
            "TEMPO FINAL: " + GameManager.FormatarTempo(GameManager.TempoFinal);

        EventSystem.current.SetSelectedGameObject(botaoJogarNovamente);
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}
