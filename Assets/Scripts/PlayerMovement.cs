using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*Velocidade e Movimento Horizontal da Barra*/
    public float velocidade; // Velocidade de movimento da barra
    float movimentoHorizontal; // Valor do movimento horizontal

    public Transform defaultPosition;
    public GameObject pressPanel;
    public GameObject GameOverPanel;

    /*Limite do Campo do Jogo*/
    private readonly float limiteX = 11.0f; // Limite horizontal do campo de jogo
    private readonly float limiteXMegaBar = 8f; // Limite horizontal do campo de jogo

    // Variáveis para controle do tamanho da barra
    float tamanhoNormal = 1.0f; // Tamanho normal da barra
    float tamanhoAumentado = 2f; // Valor aumentado para o tamanho da barra
    bool barraAumentada = false; // Flag para verificar se a barra está aumentada

    // Método para aumentar o tamanho da barra
    public void AumentarTamanhoBarra()
    {
        if (!barraAumentada) // Verifica se a barra já não está aumentada
        {
            // Aumenta temporariamente o tamanho da barra
            Vector3 newScale = transform.localScale;
            newScale.x *= tamanhoAumentado / tamanhoNormal;
            newScale.z = tamanhoNormal; // Mantém o tamanho no eixo Z
            transform.localScale = newScale;

            barraAumentada = true;

            // Inicia uma coroutine para desativar o efeito após um período de tempo
            StartCoroutine(DesativarBarraMaior());
        }
    }

    // Coroutine para desativar o efeito de Barra Maior após um período de tempo
    IEnumerator DesativarBarraMaior()
    {
        yield return new WaitForSeconds(10f); // Aguarda 10 segundos

        // Reduz o tamanho da barra de volta ao tamanho normal
        Vector3 newScale = transform.localScale;
        newScale.x *= tamanhoNormal / tamanhoAumentado;
        newScale.z = tamanhoNormal; // Mantém o tamanho no eixo Z
        transform.localScale = newScale;
        barraAumentada = false;
    }

    void Update()
    {
        if (GameOverPanel.activeSelf) {
            transform.position = defaultPosition.position;
        }else
        {
            if (pressPanel.activeSelf)
            {
                transform.position = defaultPosition.position;
            }
            else { movimentoHorizontal = Input.GetAxis("Horizontal"); }
        }

        if (barraAumentada)
        {
            if ((movimentoHorizontal > 0 && transform.position.x < limiteXMegaBar) || (movimentoHorizontal < 0 && transform.position.x > -limiteXMegaBar))
            {
                transform.position = transform.position + (Vector3.right * movimentoHorizontal * velocidade * Time.deltaTime);
            }

        }
        else {
            if ((movimentoHorizontal > 0 && transform.position.x < limiteX) || (movimentoHorizontal < 0 && transform.position.x > -limiteX))
            {
                transform.position = transform.position + (Vector3.right * movimentoHorizontal * velocidade * Time.deltaTime);
            }
        }
    }
}

