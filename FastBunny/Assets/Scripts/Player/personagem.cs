using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagem : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private Animator animatorPlayer;
    private GameController _GameController;

    public Transform groundCheck;
    public Transform armaPosition;
    public LayerMask whatIsGround;
    public GameObject cenouraProjetilPrefab;

    private bool isGrounded;
    private int speedX;
    private float speedY;
    private int extraJump;
    private float forcaTiro;
    private bool disparo;

    public float speed;
    public float jumpForce;
    public bool isLookingLeft;
    public int puloExtra;
    public float forcaTiroBase;
    public float delayDisparo;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();

        extraJump = puloExtra;

        forcaTiro = forcaTiroBase;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0) speedX = 1; else speedX = 0;
        if (isLookingLeft == true && horizontal > 0) 
        {
            Flip();
        }
        if (isLookingLeft == false && horizontal < 0) 
        {
            Flip();
        }

        speedY = rbPlayer.velocity.y;

        rbPlayer.velocity = new Vector2(horizontal * speed, speedY);

        if (isGrounded) extraJump = puloExtra;

        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            Jump();
            extraJump--;
        }
        else if (Input.GetButtonDown("Jump") && extraJump == 0 && isGrounded) 
        {
            Jump();
        }

        if (Input.GetButton("Fire1") && _GameController.municao > 0 && !disparo) 
        {
            AtirarCenoura();
        }

        if (transform.position.y < -5) 
        {
            _GameController.mudarCena("Over");
        }

    }

    public void Jump() 
    {
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
        rbPlayer.AddForce(new Vector2(0, jumpForce));
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
    }

    private void LateUpdate()
    {
        animatorPlayer.SetInteger("speedX", speedX);
        animatorPlayer.SetFloat("speedY", speedY);
        animatorPlayer.SetBool("isGrounded", isGrounded);
    }

    //Gira o personagem
    void Flip() 
    {
        isLookingLeft = !isLookingLeft;

        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        cenouraProjetilPrefab.transform.localScale = new Vector3(-cenouraProjetilPrefab.transform.localScale.x, -cenouraProjetilPrefab.transform.localScale.y, cenouraProjetilPrefab.transform.localScale.z);
        forcaTiro *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "coletavel":

                idColetavel idC = collider.gameObject.GetComponent<idColetavel>();

                switch (idC.nomeItem) 
                {
                    case "ovo":
                        _GameController.Pontuar(idC.pontos, idC.FX);
                        break;
                    case "municao":
                        _GameController.Pontuar(idC.pontos, idC.FX);
                        _GameController.municao += idC.quantidade;
                        break;
                }

                Destroy(collider.gameObject);
                break;

            case "obstaculo":
                _GameController.mudarCena("Over");
                break;
        }
    }

    void AtirarCenoura() 
    {
        disparo = true;
        StartCoroutine("delayTiro");

        _GameController.municao--;

        GameObject temp = Instantiate(cenouraProjetilPrefab);
        temp.transform.position = armaPosition.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0);

        Destroy(temp, 2f);
    }

    IEnumerator delayTiro()
    {
        yield return new WaitForSeconds(delayDisparo);
        disparo = false;
    }
}
