using UnityEngine;
using Player;

namespace Enemies
{
    public class EnemyDamage : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }
}
