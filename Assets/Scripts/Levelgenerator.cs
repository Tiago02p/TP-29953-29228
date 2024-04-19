using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Levelgenerator : MonoBehaviour
{
    public GameObject[] Linha_1;
    public GameObject[] Linha_2;
    public GameObject[] Linha_3;
    public GameObject[] Linha_4;
    public GameObject[] Linha_5;
    public GameObject[] Linha_6;
    public GameObject[] tiposTijolos; // Array contendo os diferentes tipos de tijolos

    private Vector3[][] posicoesOriginais; // Armazena as posições originais dos tijolos

    public TextMeshProUGUI mais10pontosPrefab;


    void Awake()
    {
        // Inicializa o array de posições originais
        posicoesOriginais = new Vector3[6][];

        // Preenche o array com as posições originais das linhas de tijolos
        posicoesOriginais[0] = ObterPosicoesOriginais(Linha_1);
        posicoesOriginais[1] = ObterPosicoesOriginais(Linha_2);
        posicoesOriginais[2] = ObterPosicoesOriginais(Linha_3);
        posicoesOriginais[3] = ObterPosicoesOriginais(Linha_4);
        posicoesOriginais[4] = ObterPosicoesOriginais(Linha_5);
        posicoesOriginais[5] = ObterPosicoesOriginais(Linha_6);
        
    }


    public void LimparMapa()
    {
        LimparLinha(Linha_1);
        LimparLinha(Linha_2);
        LimparLinha(Linha_3);
        LimparLinha(Linha_4);
        LimparLinha(Linha_5);
        LimparLinha(Linha_6);
    }

    private void LimparLinha(GameObject[] linha)
    {
        // Itera sobre cada tijolo na linha e destroi-o
        foreach (GameObject tijolo in linha)
        {
            Destroy(tijolo);
        }
    }

    private Vector3[] ObterPosicoesOriginais(GameObject[] linha)
    {
        // Array para armazenar as posições originais
        Vector3[] posicoes = new Vector3[linha.Length];

        // Itera sobre cada tijolo na linha e obtém sua posição original
        for (int i = 0; i < linha.Length; i++)
        {
            posicoes[i] = linha[i].transform.position;
        }

        return posicoes;
    }

    public void GerarNovoMapa()
    {
        // Regenera o mapa com base nas posições originais dos tijolos
        SubstituirPorTijolosAleatorios(Linha_1, posicoesOriginais[0]);
        SubstituirPorTijolosAleatorios(Linha_2, posicoesOriginais[1]);
        SubstituirPorTijolosAleatorios(Linha_3, posicoesOriginais[2]);
        SubstituirPorTijolosAleatorios(Linha_4, posicoesOriginais[3]);
        SubstituirPorTijolosAleatorios(Linha_5, posicoesOriginais[4]);
        SubstituirPorTijolosAleatorios(Linha_6, posicoesOriginais[5]);
    }

    private void SubstituirPorTijolosAleatorios(GameObject[] linha, Vector3[] posicoesOriginais)
    {
        // Itera sobre cada tijolo na linha
        for (int i = 0; i < linha.Length; i++)
        {
            // Gera um número aleatório entre 0 e 1
            float randomValue = Random.value;

            // Define as faixas de probabilidades para cada tipo de tijolo
            float[] probabilityRanges = { 0.35f, 0.6f, 0.8f, 0.95f, 0.995f, 1.0f };

            // Escolhe o tipo de tijolo com base no número aleatório gerado
            GameObject tijoloPrefab;
            if (randomValue <= probabilityRanges[0])
            {
                tijoloPrefab = tiposTijolos[0];
            }
            else if (randomValue <= probabilityRanges[1])
            {
                tijoloPrefab = tiposTijolos[1];
            }
            else if (randomValue <= probabilityRanges[2])
            {
                tijoloPrefab = tiposTijolos[2];
            }
            else if (randomValue <= probabilityRanges[3])
            {
                tijoloPrefab = tiposTijolos[3];
            }
            else if (randomValue <= probabilityRanges[4])
            {
                tijoloPrefab = tiposTijolos[4];
            }
            else
            {
                tijoloPrefab = tiposTijolos[5];
            }

            // Instancia o tijolo na posição original adequada
            GameObject novoTijolo = Instantiate(tijoloPrefab, posicoesOriginais[i], Quaternion.identity);

            // Remove o GameObject antigo da cena
            Destroy(linha[i]);

            // Atribui o novo tijolo à posição na linha
            linha[i] = novoTijolo;
        }
    }
}



