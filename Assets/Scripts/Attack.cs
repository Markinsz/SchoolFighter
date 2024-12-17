using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    private AudioSource audioSource;
    public AudioClip hitSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ao colidir, salva na variável enemy, o inimigo que foi colidido
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        EnemyRanged enemyRanged = collision.GetComponent<EnemyRanged>();

        //Ao colidir, salva na variável player, o player que foi colidido
        PlayerController player = collision.GetComponent<PlayerController>();

        //Se a colisão foi um inimigo
        if (enemy != null)
        {
            //Inimigo recebe dano
            enemy.TakeDamage(damage);

            audioSource.clip = hitSound;
            audioSource.Play();
        }

        if (enemyRanged != null)
        {
            enemyRanged.TakeDamage(damage);
            
            audioSource.clip= hitSound;
            audioSource.Play();
        }

        //Se a colisão foi com um player
        if (player != null)
        {
            player.TakeDamage(damage);

            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }
}
