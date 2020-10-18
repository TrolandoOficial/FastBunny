using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Player _player;

    [Header("Config. Personagem")]
    public float velocidadeMovimento = 1;

    public float limiteMaxY;
    public float limiteMinY;
    public float limiteMaxX;
    public float limiteMinX;

    [Header("Config. Objetos")]
    public float velocidadeObjeto;
    public float distanciaDestruir;
    public float tamanhoPonte;
    public GameObject[] pontePrefab;

    [Header("Config. Barril")]
    public int orderTop;
    public int orderDown;

    public float posYTop;
    public float posYDown;

    public float tempoSpawn;
    public GameObject barrilPrefab;

    [Header("Globals")]
    public float posXPlayer;
    public int score;
    public Text txtScore;

    [Header("FX Sound")]
    public AudioSource fxSource;
    public AudioClip fxPontos;

    // Start is called before the first frame update
    void Start()
    {
        //_player = FindObjectOfType(typeof(Player)) as Player;
        //StartCoroutine("spawnBarril");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //posXPlayer = _player.transform.position.x;
    }

    IEnumerator spawnBarril() 
    {
        yield return new WaitForSeconds(tempoSpawn);

        int rand = Random.Range(0, 100);
        float posY = 0;
        int order = 0;
        if (rand < 50) 
        {
            posY = posYTop;
            order = orderTop;
        }
        else 
        {
            posY = posYDown;
            order = orderDown;
        }

        GameObject temp = Instantiate(barrilPrefab);

        temp.transform.position = new Vector3(temp.transform.position.x, posY, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = order;

        StartCoroutine("spawnBarril");
    }
    public void Pontuar(int qtdPontos)
    {
        score += qtdPontos;
        txtScore.text = "Score: " + score.ToString();
        fxSource.PlayOneShot(fxPontos);
    }

    public void mudarCena(string cenaDestino) 
    {
        SceneManager.LoadScene(cenaDestino);
    }
}
