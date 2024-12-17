using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyRanged : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    // Variavel que indica se o inimigo est� vivo
    public bool isDead;

    // Variaveis para controlar o lado que o inimigo est� virado
    public bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar posi��o do Player
    private Transform target;

    // Variaveis para movimenta��o do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;

    private bool isWalking;

    private float horizontalForce;
    private float verticalForce;

    // Variavel que vamos usar para controlar o intervalo de tempo que o inimigo ficar� andando vertical
    // Isso vai ajudar � dar uma aleatoriedade ao movimento do inimigo
    private float walkTimer;

    // Vari�veis para mec�nica de ataque
    private float attackRate = 1f;
    private float nextAttack;

    // Variaveis para mec�nica de dano
    public int maxHealth;
    public int currentHealth;
    public Sprite enemyImage;

    public float staggerTime = 0.5f;
    private float damageTimer;
    public bool isTakingDamage;

    //Vari�vel projetil
    public GameObject projectile;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar o Player e armazenar sua posi��o
        target = FindAnyObjectByType<PlayerController>().transform;

        // Inicializar a velocidade do inimigo
        currentSpeed = enemySpeed;

        // Inicializar a vida do inimigo
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Verificar se o Player est� para a Direita ou para a Esquerda
        // E com isso determinar o lado que o Inimigo ficar� virado
        if (target.position.x < this.transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        // Se facingRight for TRUE, vamos virar o inimigo em 180� no eixo Y,
        // Sen�o vamos virar o inimigo para a esquerda

        // Se o Player � direita e a dire��o anterior N�O era direita (inimigo olhando para esquerda)
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        // Se o Player N�O est� � direita e a dire��o anterior ERA direita (inimigo olhando para direita)
        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        // Iniciar o timer do caminhar do inimigo
        walkTimer += Time.deltaTime;

        // Gerenciar a anima��o do inimigo
        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        // Gereciar o tempo de stagger
        if (isTakingDamage && !isDead)
        {
            damageTimer += Time.deltaTime;

            ZeroSpeed();

            if (damageTimer >= staggerTime)
            {
                isTakingDamage = false;
                damageTimer = 0;

                ResetSpeed();
            }
        }

        // Atualiza o animator
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 targetDistance = target.position - this.transform.position;

            if (walkTimer >= Random.Range(2.5f, 3.5f))
            {
                verticalForce = targetDistance.y / Mathf.Abs(targetDistance.y);
                horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

                walkTimer = 0;
            }

            if (Mathf.Abs(targetDistance.x) < 0.2f)
            {
                horizontalForce = 0;
            }

            rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);

            if (Mathf.Abs(targetDistance.x) < 1f)
            {
                horizontalForce = 0;
            }

            if (Mathf.Abs(targetDistance.y)< 0.05f)
            {
                verticalForce = 0;
            }

            if(!isTakingDamage)
            {
                rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce);
            }

            //L�gica do Ataque
            if(Mathf.Abs(targetDistance.x) < 1.3f && Mathf.Abs(targetDistance.y) < 0.05f && Time.time > nextAttack)
            {
                //Ataque do inimigo
                animator.SetTrigger("Attack");
                ZeroSpeed();

                nextAttack = Time.time + attackRate;
            }
        }
    }

    public void Shoot()
    {
        //Define a posi��o de Spawn do projetil
        Vector2 spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y + 0.2f);

        //Spawnar o projetil na posi��o definida
        GameObject shotObject = Instantiate(projectile, spawnPosition, Quaternion.identity);

        // Ativar o projetil
        shotObject.SetActive(true);

        var shotPhysics = shotObject.GetComponent<Rigidbody2D>();
        
        if (facingRight)
        {
            //Aplica for�a no projetil para ele se deslocar
            shotPhysics.AddForceX(80f);
            shotObject.transform.Rotate(0, 0, 180);
        }
        else
        {
            //Aplica for�a no proj�til para ele se deslocar para a esquerda
            shotPhysics.AddForceX(-80f);
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;

            currentHealth -= damage;

            animator.SetTrigger("Hurt");

            //Atualiza a UI do inimigo
            FindFirstObjectByType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyImage);

            if (currentHealth <= 0)
            {
                isDead = true;

                //Corrige bug do inimigo deslizar ap�s morte
                rb.linearVelocity = Vector2.zero;

                animator.SetTrigger("Dead");
            }
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = enemySpeed;
    }

    public void DisableEnemy()
    {
        // Desabilita este inimigo
        this.gameObject.SetActive(false);
    }
}