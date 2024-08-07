using UnityEngine;

namespace Player
{
    public class MakeExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        private void OnEnable()
        {
            PlayerTeleport.Teleport += SpawnExplosion;
        }

        private void OnDisable()
        {
            PlayerTeleport.Teleport -= SpawnExplosion;
        }

        private void SpawnExplosion(TeleportArgs teleportArgs)
        {
            Instantiate(_explosionPrefab, teleportArgs.position, new Quaternion());
        }
    }
}
