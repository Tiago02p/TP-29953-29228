using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] paineis; // Array contendo os pain�is a serem alternados
    public Button next;
    public Button prev;

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
        paineis[0].SetActive(true);
        paineis[1].SetActive(false);
    }

    public void Panel2()
    {
        paineis[1].SetActive(true);
        paineis[0].SetActive(false);
    }


    // M�todo para iniciar o jogo
    public void Jogar()
    {
        SceneManager.LoadScene("Jogo"); // Carrega a cena do jogo
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // M�todo para sair do jogo
    public void Sair()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
