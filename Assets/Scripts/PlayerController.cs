using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    //Player olhando para a direita
    private bool playerFacingRight = true;

    //Vari�vel contadora
    private int PunchCount;

    //Tempo de Ataque
    private float timeCross = 0.75f;

    private bool comboControl;
    
    //Indicar se Player est� morto
    private bool isDead;

    void Start()
    {
        //Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();  

        //Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>(); 

        currentSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        UpdateAnimator();


        if (Input.GetKeyDown(KeyCode.X))
        {
               if (PunchCount < 2)
                {
                    PlayerJab();
                    PunchCount++;
                    if (!comboControl)
                    {
                        //Iniciar o temporizador
                        StartCoroutine(CrossController());
                    }
                }
               else if (PunchCount >= 2)
                {
                    PlayerCross();
                    PunchCount = 0;
                }
            //Parando o temporizador
            StopCoroutine(CrossController());
        }

    }

    //Fixed Update geralmente � utilizada para implementa��o de f�sica no jogo
    //Por ter uma execu��o padronizada em diferentes dispositicos
    private void FixedUpdate()
    {
        //Verificar se o Player est� em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        //playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        //Pega a entrada do jogador, e cria um Vector2 para usar no player Direction
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se o player vai para a ESQUERDA e est� olhando para a DIREITA
        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        playerAnimator.SetBool("isWalking"/*Declarado no animator*/, isWalking/*Declarado no VC*/);
    }

    void Flip()
    {
        //Vai girar o sprite player em 180� no eixo Y
        //Inverter o valor da vari�vel
         playerFacingRight = !playerFacingRight;

        //Girar o sprite do player em 180� no eixo Y
        //               x,  y , z
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        //Acessa anima��o do Jab
        //Ativa o gatilho de Ataque;
        playerAnimator.SetTrigger("isJabbing");
    }

    void PlayerCross()
    {
        //Acessa anima��o do Cross
        //Ativa o gatilho do Ataque;
        playerAnimator.SetTrigger("isCrossing");
    }

    IEnumerator CrossController ()
    {
        comboControl = true;

        yield return new WaitForSeconds(timeCross);
        PunchCount = 0;

        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }
}