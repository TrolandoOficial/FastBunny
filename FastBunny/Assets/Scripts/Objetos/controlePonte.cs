using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlePonte : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D ponteRb;

    private bool instanciado;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        ponteRb = GetComponent<Rigidbody2D>();

        ponteRb.velocity = new Vector2(_GameController.velocidadeObjeto, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!instanciado) 
        {
            if (transform.position.x <= 0) 
            {
                instanciado = true;

                int idPrefab = 0;
                int rand = Random.Range(0, 100);
                if (rand < 20)
                {
                    idPrefab = 4;
                }
                else if (rand >= 20 && rand < 40) 
                {
                    idPrefab = 3;
                }
                else if (rand >= 40 && rand < 60)
                {
                    idPrefab = 2;
                }
                else if (rand >= 60 && rand < 80)
                {
                    idPrefab = 1;
                }

                GameObject temp = Instantiate(_GameController.pontePrefab[idPrefab]);
                float posX = transform.position.x + _GameController.tamanhoPonte;
                float posY = transform.position.y;
                temp.transform.position = new Vector3(posX, posY, 0);
            }
        }
        if (transform.position.x < _GameController.distanciaDestruir) 
        {
            Destroy(this.gameObject);
        }
    }
}
