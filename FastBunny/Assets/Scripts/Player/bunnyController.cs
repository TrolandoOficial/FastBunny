using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private GameController _GameController;

    public float forcaPulo;

    public Transform groundCheck;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            rbPlayer.AddForce(new Vector2(0, forcaPulo));
        }

        float posX = transform.position.x;
        float posY = transform.position.y;

        if (transform.position.x > _GameController.limiteMaxX)
        {
            posX = _GameController.limiteMaxX;
        }
        else if (transform.position.x < _GameController.limiteMinX)
        {
            posX = _GameController.limiteMinX;
        }

        transform.position = new Vector3(posX, posY, 0);

        if (transform.position.y < -5) 
        {
            _GameController.mudarCena("Over");
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag) 
        {
            case "coletavel":
                _GameController.Pontuar(10);
                Destroy(collider.gameObject);
                break;

            case "obstaculo":
                _GameController.mudarCena("Over");
                break;
        }
    }
}
