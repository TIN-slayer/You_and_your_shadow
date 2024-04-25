using System.Collections;
using UnityEngine;
using General;

namespace Player
{
    public class PlayerInvulnarable : MonoBehaviour
    {
        [SerializeField] private float _invulnarableTime;

        private void Awake()
        {
            Physics2D.IgnoreLayerCollision((int)CollisionLayers.Player, (int)CollisionLayers.EnemyHitbox, false);
        }

        private void OnEnable()
        {
            PlayerTeleport.Teleport += GetInvulnarable;
        }

        private void OnDisable()
        {
            PlayerTeleport.Teleport -= GetInvulnarable;
        }

        public void GetInvulnarable(Vector3 teleportPosition)
        {
            StartCoroutine(InvulnarabilityPeriod());
        }
        private IEnumerator InvulnarabilityPeriod()
        {
            //spriteRenderer.color = transpColor;
            Physics2D.IgnoreLayerCollision((int)CollisionLayers.Player, (int)CollisionLayers.EnemyHitbox, true);
            yield return new WaitForSeconds(_invulnarableTime);
            //spriteRenderer.color = fullColor;
            Physics2D.IgnoreLayerCollision((int)CollisionLayers.Player, (int)CollisionLayers.EnemyHitbox, false);
        }
    }
}
