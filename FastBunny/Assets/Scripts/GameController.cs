using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Config. Objetos")]
    public float velocidadeObjeto;
    public float distanciaDestruir;
    public float tamanhoPonte;
    public GameObject[] pontePrefab;

    [Header("Globals")]
    public int score;
    public Text txtScore;

    [Header("FX Sound")]
    public AudioSource fxSource;
    public AudioClip fxPontos;
    public AudioClip fxPack;
    
    [Header("Weapon")]
    public int municao;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    public void Pontuar(int qtdPontos, int pack)
    {
        score += qtdPontos;
        txtScore.text = "Score: " + score.ToString();
        if (pack == 1) fxSource.PlayOneShot(fxPack);
        else if (pack == 2) fxSource.PlayOneShot(fxPack);
        else fxSource.PlayOneShot(fxPontos);


    }

    public void mudarCena(string cenaDestino) 
    {
        SceneManager.LoadScene(cenaDestino);
    }
}
