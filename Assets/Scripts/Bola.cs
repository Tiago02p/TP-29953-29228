using System.Collections;
using TMPro;
using UnityEngine;

public class Bola : MonoBehaviour
{
    private float limiteBaixo = 0;
    private float Velocidade = 18;
    Rigidbody rb;

    public TextMeshProUGUI velocidadeJogoTexto;
    public TextMeshProUGUI multiplierText;

    public Transform ballPosition;
    public Levelgenerator Levelgenerator;
    public PlayerMovement player;

    private int brickCount;
    public int score = 0;
    private int lives = 5;

    private int[] values = { -1, 1 };
    private int random;

    public TextMeshProUGUI scoreTxt;

    public GameObject tijoloPrefab;
    public GameObject tijololvl1Prefab;
    public GameObject tijololvl2Prefab;
    public GameObject tijololvl3Prefab;
    public GameObject tijololvl4Prefab;

    public GameObject pressPanel;
    public GameObject megaballPanel;
    public GameObject megabarPanel;
    public GameObject BombBallPanel;
    public GameObject SpeedPanel;

    public HealthBarHUDTester healthBarHUD;



    public GameObject giveLife;
    public GameObject stealLife;

    private bool barraMaiorAtiva = false;
    private bool megaBallAtiva = false;
    private bool bolaExplosivaAtiva = false;
    private bool SpeedBallativa = false;

    private int tijolos;
    private int tijolosLvl1;
    private int tijolosLvl2;
    private int tijolosLvl3;

    public float maxScale = 3.0f;
    public UnityEngine.Color BombColor = UnityEngine.Color.black;
    public GameObject BolaBombaPrefab;
    public GameObject explosaoPrefab;

    public UnityEngine.Color SpeedColor = UnityEngine.Color.cyan;
    public GameObject SpeedBallPrefab;

    public GameObject megaballImpact;
    private Vector3 tamanhoOriginal;

    public Material blackMaterial;
    public Material blueMaterial;
    public Material defaultMaterial;
    private Renderer bolaRenderer;

    public enum PowerUpType
    {
        BarraMaior,
        MegaBall,
        BolaExplosiva,
        Rocket
    }

    public enum LifeType
    {
        GiveLife,
        StealLife
    }

    void Start()
    {
        bolaRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        pressPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LancarBola();
            pressPanel.SetActive(false);
        }

        Levelgenerator.LimparMapa();
        Levelgenerator.GerarNovoMapa();
    }

    void Update()
    {
        if (SpeedBallativa)
        {
            rb.velocity = rb.velocity.normalized * (Velocidade + 4);
        }
        else
        {
            rb.velocity = rb.velocity.normalized * Velocidade;
        }


        tijolos = GameObject.FindGameObjectsWithTag("tijolo").Length;
        tijolosLvl1 = GameObject.FindGameObjectsWithTag("tijololvl1").Length;
        tijolosLvl2 = GameObject.FindGameObjectsWithTag("tijololvl2").Length;
        tijolosLvl3 = GameObject.FindGameObjectsWithTag("tijololvl3").Length;

        brickCount = tijolos + tijolosLvl1 + tijolosLvl2 + tijolosLvl3;

        if (lives <= 0)
        {
            transform.position = ballPosition.position;
            rb.velocity = Vector3.zero;
            FindObjectOfType<GameManager>().GameOver(score);
        }
        else
        {

            if (transform.position.y < limiteBaixo)
            {
                pressPanel.SetActive(true);
                transform.position = ballPosition.position;
                rb.velocity = Vector3.zero;
                lives--;
                healthBarHUD.Hurt(1f);
            }

            if (pressPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (SpeedBallativa)
                    {
                        LancarSpeedBall();
                        pressPanel.SetActive(false);
                    }
                    else
                    {
                        LancarBola();
                        pressPanel.SetActive(false);
                    }
                }
            }

            if (brickCount == 0)
            {
                pressPanel.SetActive(true);
                transform.position = ballPosition.position;
                rb.velocity = Vector3.zero;
                Levelgenerator.GerarNovoMapa();
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tijolo") || collision.gameObject.CompareTag("tijololvl1") || collision.gameObject.CompareTag("tijololvl2") || collision.gameObject.CompareTag("tijololvl3"))
        {
            if (bolaExplosivaAtiva)
            {
                Collider[] colliders = Physics.OverlapSphere(collision.contacts[0].point, 2);
                int tijolosDestruidos = 0;

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("tijolo") || collider.CompareTag("tijololvl1") || collider.CompareTag("tijololvl2") || collider.CompareTag("tijololvl3"))
                    {
                        GameObject explosion = Instantiate(explosaoPrefab, collision.transform.position, Quaternion.identity);
                        Destroy(collider.gameObject);
                        tijolosDestruidos++;
                        StartCoroutine(DestruirExplosao(explosion));

                    }
                }

                for (int i = 1; i <= tijolosDestruidos; i++)
                {
                    AtualizarScore();
                }
            }
            else
            {
                if (megaBallAtiva)
                {
                        GameObject explosion = Instantiate(megaballImpact, collision.transform.position, Quaternion.identity);
                        StartCoroutine(DestruirExplosao(explosion));
                        Destroy(collision.gameObject);
                        AtualizarScore();
                    }
                    Destroy(collision.gameObject);
                    AtualizarScore();
                
            }

            if (collision.gameObject.CompareTag("tijololvl1"))
            {
                AtribuirPowerUpAleatorio();
            }

            if (collision.gameObject.CompareTag("tijololvl2"))
            {
                RetirarOuDarVida();
            }

            if (collision.gameObject.CompareTag("tijololvl3"))
            {
                AtivarSpeedBall(6.0f);
            }


            // Exibir a mensagem "+10 pontos" associada ao tijolo destruído
            TextMeshProUGUI mais10pontos = collision.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (mais10pontos != null)
            {
                mais10pontos.gameObject.SetActive(true);
                StartCoroutine(DesativarMais10Pontos(mais10pontos.gameObject));
            }
        }
    }



    private IEnumerator DesativarMais10Pontos(GameObject mais10pontos)
    {
        yield return new WaitForSeconds(1.5f); // Aguarde 1 segundo

        // Verificar se o objeto de texto ainda existe antes de tentar desativá-lo
        if (mais10pontos != null)
        {
            mais10pontos.SetActive(false); // Desative o objeto de texto "+10 pontos"
        }
    }

    public void ResetBallPowers()
    {
        megaBallAtiva = false;
        bolaExplosivaAtiva = false;
        SpeedBallativa = false;
    }

    public void LancarBola()
    {
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        random = values[randomIndex];
        rb.velocity = new Vector3(random, 1, 0) * Velocidade;
    }

    public void LancarSpeedBall()
    {
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        random = values[randomIndex];
        rb.velocity = (new Vector3(random, 1, 0) * Velocidade) * 1.3f;
    }

    void AtualizarScore()
    {
        if (multiplierText.text == "x2.0") {
            score += 20;
        }
        else
        {
            if (multiplierText.text == "x0.5") {
                score += 5;
            }
            else { 
                score += 10; 
            }
        }
        
        scoreTxt.text = score.ToString("00000");
    }

    void RetirarOuDarVida()
    {
        LifeType life = (LifeType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(LifeType)).Length);
        switch (life)
        {
            case LifeType.GiveLife:
                GiveLife();
                break;
            case LifeType.StealLife:
                StealLife();
                break;
        }
    }

    public void GiveLife()
    {
        giveLife.gameObject.SetActive(true);
        StartCoroutine(Esperar());
        lives++;
        if (lives > 5)
        {
            healthBarHUD.AddHealth();
        }
        else
        {
            healthBarHUD.Heal(1f);
        }
    }

    public void StealLife()
    {
        stealLife.gameObject.SetActive(true);
        StartCoroutine(Esperar());
        lives--;
        healthBarHUD.Hurt(1f);
    }

    void AtribuirPowerUpAleatorio()
    {
        PowerUpType powerUp = (PowerUpType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);

        switch (powerUp)
        {
            case PowerUpType.BarraMaior:
                AtivarBarraMaior();
                break;
            case PowerUpType.MegaBall:
                AtivarMegaball(8);
                break;
            case PowerUpType.BolaExplosiva:
                AtivarBolaExplosiva(6);
                break;
        }
    }

    public void AtivarBarraMaior()
    {
        if (!barraMaiorAtiva)
        {
            barraMaiorAtiva = true;
            megabarPanel.gameObject.SetActive(true);
            StartCoroutine(Esperar());

            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.AumentarTamanhoBarra();
            }
        }
    }

    public void AtivarMegaball(float duracao)
    {
        if (!megaBallAtiva)
        {
            if (!SpeedBallativa)
            {
                if (!bolaExplosivaAtiva)
                {
                    if (transform.localScale.x < maxScale)
                    {
                        megaBallAtiva = true;
                        transform.localScale *= 2;
                        megaballPanel.gameObject.SetActive(true);
                        StartCoroutine(Esperar());
                        StartCoroutine(ReverterTamanhoBola(duracao));
                    }
                }
            }
        }
    }


    public void AtivarBolaExplosiva(float duracao)
    {
        if (!megaBallAtiva)
        {
            if (!SpeedBallativa)
            {
                if (!bolaExplosivaAtiva)
                {
                    bolaRenderer.material = blackMaterial;
                    
                    bolaExplosivaAtiva = true;
                    BolaBombaPrefab.gameObject.SetActive(true);
                    BombBallPanel.gameObject.SetActive(true);

                    StartCoroutine(Esperar());
                    StartCoroutine(DesativarEfeitoBolaExplosiva(duracao));
                }
            }
        }
    }


    public void AtivarSpeedBall(float duracao)
    {
        if (!megaBallAtiva)
        {
            if (!SpeedBallativa)
            {
                if (!bolaExplosivaAtiva)
                {
                    bolaRenderer.material = blueMaterial;
                    SpeedBallativa = true;
                    SpeedBallPrefab.gameObject.SetActive(true);
                    SpeedPanel.gameObject.SetActive(true);

                    StartCoroutine(Esperar());
                    StartCoroutine(ReverterAumentovelocidade(6));
                }
            }
        }
    }




    private IEnumerator ReverterTamanhoBola(float duracao)
    {
        yield return new WaitForSeconds(duracao);
        if (transform.localScale != tamanhoOriginal)
        {
            transform.localScale /= 2f;
        }
        megaballImpact.SetActive(false);
        ResetBallPowers();
    }

    private IEnumerator DesativarEfeitoBolaExplosiva(float duracao)
    {
        yield return new WaitForSeconds(duracao);
        BolaBombaPrefab.gameObject.SetActive(false);
        bolaRenderer.material = defaultMaterial;
        ResetBallPowers();
    }

    private IEnumerator ReverterAumentovelocidade(int duracao)
    {
        yield return new WaitForSeconds(duracao);
        rb.velocity *= 0.80f;
        SpeedBallPrefab.gameObject.SetActive(false);
        bolaRenderer.material = defaultMaterial;
        ResetBallPowers();
    }

    private IEnumerator DestruirExplosao(GameObject explosion)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(explosion);
    }

    private IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1f);
        giveLife.SetActive(false);
        stealLife.SetActive(false);
        megabarPanel.SetActive(false);
        megaballPanel.SetActive(false);
        BombBallPanel.SetActive(false);
        SpeedPanel.SetActive(false);
    }


}
