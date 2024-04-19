using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] paineis; // Array contendo os pain�is a serem alternados
    public Button next;
    public Button prev;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        // Inicialmente, esconde todos os pain�is, exceto o primeiro
        for (int i = 1; i < paineis.Length; i++)
        {
            paineis[i].SetActive(false);
        }
    }

    // M�todo chamado quando o valor do slider � alterado
    public void Panel1()
    {
        audioManager.PlaySFX(audioManager.buttonSound);
        paineis[0].SetActive(true);
        paineis[1].SetActive(false);
    }

    public void Panel2()
    {
        audioManager.PlaySFX(audioManager.buttonSound);
        paineis[1].SetActive(true);
        paineis[0].SetActive(false);
    }


    // M�todo para iniciar o jogo
    public void Jogar()
    {
        audioManager.PlaySFX(audioManager.buttonSound);
        SceneManager.LoadScene("Jogo"); // Carrega a cena do jogo
    }

    public void Voltar()
    {
        audioManager.PlaySFX(audioManager.buttonSound);
        SceneManager.LoadScene("Menu");
    }

    public void Tutorial()
    {
        audioManager.PlaySFX(audioManager.buttonSound);
        SceneManager.LoadScene("Tutorial");
    }

    // M�todo para sair do jogo
    public void Sair()
    {
        audioManager.PlaySFX(audioManager.buttonSound);
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
