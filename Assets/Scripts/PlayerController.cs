using UnityEngine; 

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    private bool playerFacingRight = true;

    void Start()
    {
        //Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();  

        //Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        UpdateAnimator();
    }

    //Fixed Update geralmente é utilizada para implementação de física no jogo
    //Por ter uma execução padronizada em diferentes dispositicos
    private void FixedUpdate()
    {
        //Verificar se o Player está em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        //Pega a entrada do jogador, e cria um Vector2 para usar no player Direction
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se o player vai para a ESQUERDA e está olhando para a DIREITA
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
        //Vai girar o sprite player em 180° no eixo Y
        //Inverter o valor da variável
         playerFacingRight = !playerFacingRight;

        //Girar o sprite do player em 180° no eixo Y
        //               x,  y , z
        transform.Rotate(0, 180, 0);
    }
}
