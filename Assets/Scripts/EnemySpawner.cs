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
        // Caso atinja o máximo de inimigos spawnados
        if (currentEnemies >= numberOfEnemies)
        {
            // Contagem de inimigos ativos na ceno
            int enemies = FindObjectsByType<EnemyMeleeController>(FindObjectsSortMode.None).Length;

            if (enemies <= 0)
            {
                // Avança de seção
                LevelManager.ChangeSection(nextSection);

                //Desabilitar o spawner
                this.gameObject.SetActive(false);   
            }
        }
    }

    void SpawnEnemy()
    {
        // Posição de spawn do inimigo
        Vector2 spawnPosition;

        //Limites Y
        //-0.37 && -0.956
        spawnPosition.y = Random.Range(-0.37f, -0.956f);

        //Posição X máximo (direita) de confiner da camera + 1 de distancia
        //Pegar RightBound (limite direito) da Section (Confiner) como base
        float rightSectionBound = LevelManager.currentConfiner.BoundingShape2D.bounds.max.x;

        //Define o x do spawnPosition, igual ao ponto da DIREITA do confiner
        spawnPosition.x = rightSectionBound;

        /*
        
        Instancia (Spawna) os inimigos,
        pegando um inimigo aleatório da lista
        e spawnando na posição SpawnPosition.
        Quaterion é utilizado para trabalhar com rotação

         */
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        // Incrementa o contador de inimigos do Spawner
        currentEnemies++;

        //Se o número de inimigos atualmente na cena for menor que o número máximo dos inimigos,
        //Invoca novamenta a função de spawn
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
            //ATENÇÃO: Desabilita o collider,  as o objeto Spawner continua ativo
            this.GetComponent<BoxCollider2D>().enabled = false;

            //Invoca pela primeira vez a função SpawnEnemy
            SpawnEnemy();
        }
    }
}
