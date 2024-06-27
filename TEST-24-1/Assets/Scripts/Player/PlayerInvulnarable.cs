using System.Collections;
using UnityEngine;
using General;
using Ephemerals;

namespace Player
{
    public class PlayerInvulnarable : MonoBehaviour
    {
        [SerializeField] private float _teleportInvulnarableTime;
        private float _killInvulnarableTime = 0.5f;
        private int _invulnarableCount = 0;

        private void Awake()
        {
            Physics2D.IgnoreLayerCollision((int)CollisionLayers.Player, (int)CollisionLayers.EnemyHitbox, false);
        }

        private void OnEnable()
        {
            PlayerTeleport.Teleport += HandelTeleport;
            //Explode.SuccessExploasion += HandelKill;
            PlayerHealth.LivesChanged += HandelLostLive;
        }

        private void OnDisable()
        {
            PlayerTeleport.Teleport -= HandelTeleport;
            //Explode.SuccessExploasion -= HandelKill;
            PlayerHealth.LivesChanged -= HandelLostLive;
        }

        private void HandelTeleport(TeleportArgs teleportArgs)
        {
            StartCoroutine(InvulnarabilityPeriod(_teleportInvulnarableTime));
        }

        private void HandelKill()
        {
            StartCoroutine(InvulnarabilityPeriod(_killInvulnarableTime));
        }

        private void HandelLostLive(LivesChangedArgs arg)
        {
            if (arg.cooldown > 0)
            {
                StartCoroutine(InvulnarabilityPeriod(arg.cooldown));
            }
        }

        private IEnumerator InvulnarabilityPeriod(float invulnarableTime)
        {
            ++_invulnarableCount;
            if (_invulnarableCount == 1)
            {
                Physics2D.IgnoreLayerCollision((int)CollisionLayers.Player, (int)CollisionLayers.EnemyHitbox, true);
            }
            yield return new WaitForSeconds(invulnarableTime);
            --_invulnarableCount;
            if (_invulnarableCount == 0)
            {
                Physics2D.IgnoreLayerCollision((int)CollisionLayers.Player, (int)CollisionLayers.EnemyHitbox, false);
            }
        }
    }
}
