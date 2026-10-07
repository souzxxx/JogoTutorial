using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject painelPrincipal;
    public GameObject painelComoJogar;
    public GameObject botaoJogar;
    public GameObject botaoVoltar;
    public GameObject botaoSair;
    public AudioSource sfx;
    public AudioClip somClique;

    void Start()
    {
        Time.timeScale = 1f;
        painelPrincipal.SetActive(true);
        painelComoJogar.SetActive(false);

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            botaoSair.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(botaoJogar);
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void ComoJogar()
    {
        sfx.PlayOneShot(somClique);
        painelPrincipal.SetActive(false);
        painelComoJogar.SetActive(true);
        EventSystem.current.SetSelectedGameObject(botaoVoltar);
    }

    public void Voltar()
    {
        sfx.PlayOneShot(somClique);
        painelComoJogar.SetActive(false);
        painelPrincipal.SetActive(true);
        EventSystem.current.SetSelectedGameObject(botaoJogar);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
