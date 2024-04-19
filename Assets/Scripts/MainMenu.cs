using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] paineis; // Array contendo os painéis a serem alternados
    public Button next;
    public Button prev;

    private void Start()
    {
        // Inicialmente, esconde todos os painéis, exceto o primeiro
        for (int i = 1; i < paineis.Length; i++)
        {
            paineis[i].SetActive(false);
        }
    }

    // Método chamado quando o valor do slider é alterado
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


    // Método para iniciar o jogo
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

    // Método para sair do jogo
    public void Sair()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
