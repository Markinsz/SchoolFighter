using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;

    public int numberOfEnemies;
    private int currentEnemies;

    public float spawnTime;

    public string nextSection;

    // Update is called once per frame
    void Update()
    {
        // Caso atinja o m�ximo de inimigos spawnados
        if (currentEnemies >= numberOfEnemies)
        {
            // Contagem de inimigos ativos na ceno
            int enemies = FindObjectsByType<EnemyMeleeController>(FindObjectsSortMode.None).Length;

            if (enemies <= 0)
            {
                // Avan�a de se��o
                LevelManager.ChangeSection(nextSection);

                //Desabilitar o spawner
                this.gameObject.SetActive(false);   
            }
        }
    }

    void SpawnEnemy()
    {
        // Posi��o de spawn do inimigo
        Vector2 spawnPosition;

        //Limites Y
        //-0.37 && -0.956
        spawnPosition.y = Random.Range(-0.37f, -0.956f);

        //Posi��o X m�ximo (direita) de confiner da camera + 1 de distancia
        //Pegar RightBound (limite direito) da Section (Confiner) como base
        float rightSectionBound = LevelManager.currentConfiner.BoundingShape2D.bounds.max.x;

        //Define o x do spawnPosition, igual ao ponto da DIREITA do confiner
        spawnPosition.x = rightSectionBound;

        /*
        
        Instancia (Spawna) os inimigos,
        pegando um inimigo aleat�rio da lista
        e spawnando na posi��o SpawnPosition.
        Quaterion � utilizado para trabalhar com rota��o

         */
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        // Incrementa o contador de inimigos do Spawner
        currentEnemies++;

        //Se o n�mero de inimigos atualmente na cena for menor que o n�mero m�ximo dos inimigos,
        //Invoca novamenta a fun��o de spawn
        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            //Deastiva o colisor para iniciar o spawning apenas uma vez
            //ATEN��O: Desabilita o collider,  as o objeto Spawner continua ativo
            this.GetComponent<BoxCollider2D>().enabled = false;

            //Invoca pela primeira vez a fun��o SpawnEnemy
            SpawnEnemy();
        }
    }
}
