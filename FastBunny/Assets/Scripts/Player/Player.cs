using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameController _GameController;

    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float posX = transform.position.x;
        float posY = transform.position.y;

        playerRB.velocity = new Vector2(horizontal * _GameController.velocidadeMovimento, vertical * _GameController.velocidadeMovimento);

        if (transform.position.y > _GameController.limiteMaxY)
        {
            posY = _GameController.limiteMaxY;
        }
        else if (transform.position.y < _GameController.limiteMinY) 
        {
            posY = _GameController.limiteMinY;
        }
        if (transform.position.x > _GameController.limiteMaxX)
        {
            posX = _GameController.limiteMaxX;
        }
        else if (transform.position.x < _GameController.limiteMinX) 
        {
            posX = _GameController.limiteMinX;
        }

        transform.position = new Vector3(posX, posY, 0);
    }

    private void OnTriggerEnter2D()
    {
        _GameController.mudarCena("Over");
    }
}
