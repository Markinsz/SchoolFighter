using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            player.TakeDamage(damage);

            Destroy(this.gameObject);
        }

        //Destruir o projetil ao colidir com os limites da fase (Left e Right)
        if (collision.CompareTag("Wall"))
        {
            //O proj�til � destru�do
            Destroy(this.gameObject);
        }
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
