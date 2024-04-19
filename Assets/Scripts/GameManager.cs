using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI multiplierText;

    public GameObject pressPanel;

    public GameObject Pausepanel;

    public TextMeshProUGUI velocidadeJogoTexto;

    public void Awake()
    {
        velocidadeJogoTexto.text = "Normal";
        multiplierText.text = "x1.0";
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            Pausepanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 0.6f;
            velocidadeJogoTexto.text = "Slow";
            multiplierText.text = "x0.5";
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 1;
            velocidadeJogoTexto.text = "Normal";
            multiplierText.text = "x1.0";
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 1.5f;
            velocidadeJogoTexto.text = "Fast";
            multiplierText.text = "x2.0";
        }
    }

    // Método chamado quando o jogo termina
    public void GameOver(int score)
    {
        scoreText.text = score.ToString("00000"); // Atualiza a pontuação
        pressPanel.SetActive(false);
        gameOverPanel.SetActive(true); // Ativa o painel de Game Over
    }


    // Método para reiniciar o nível
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena atual
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Chamado pelo botão retomar
    public void ResumeGame()
    {
        Pausepanel.SetActive(false);
        Time.timeScale = 1;
    }
}
