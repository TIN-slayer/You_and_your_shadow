using General;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class ObjectSpawner : MonoBehaviour
    {
        private ContactFilter2D _contactFilter = new ContactFilter2D();
        private List<Vector2> _enemySpawnPoints = new()
        {
            new(3f, 1f), new(4f, 1f), new(5f, 1f), new(2f, 2f),
            new(3f, 2f), new(4f, 2f), new(1f, 3f), new(2f, 3f),
            new(3f, 3f), new(4f, 3f), new(1f, 4f), new(2f, 4f),
            new(3f, 4f), new(1f, 5f), new(3f, -1f), new(4f, -1f),
            new(5f, -1f), new(2f, -2f), new(3f, -2f), new(4f, -2f),
            new(1f, -3f), new(2f, -3f), new(3f, -3f), new(4f, -3f),
            new(1f, -4f), new(2f, -4f), new(3f, -4f), new(1f, -5f),
            new(0f, 3f), new(0f, 4f), new(0f, 5f), new(3f, 0f),
            new(4f, 0f), new(5f, 0f), new(-5f, -0f), new(-4f, -0f),
            new(-3f, -0f), new(-0f, -5f), new(-0f, -4f), new(-0f, -3f),
            new(-1f, 5f), new(-3f, 4f), new(-2f, 4f), new(-1f, 4f),
            new(-4f, 3f), new(-3f, 3f), new(-2f, 3f), new(-1f, 3f),
            new(-4f, 2f), new(-3f, 2f), new(-2f, 2f), new(-5f, 1f),
            new(-4f, 1f), new(-3f, 1f), new(-1f, -5f), new(-3f, -4f),
            new(-2f, -4f), new(-1f, -4f), new(-4f, -3f), new(-3f, -3f),
            new(-2f, -3f), new(-1f, -3f), new(-4f, -2f), new(-3f, -2f),
            new(-2f, -2f), new(-5f, -1f), new(-4f, -1f), new(-3f, -1f)
        };
        [SerializeField] private List<GameObject> _enemyList;
        [SerializeField] private GameObject _orb;
        private Dictionary<string, GameObject> _enemyDict = new();

        private void Awake()
        {
            MakeDict(_enemyList, _enemyDict);
        }

        // Подумать над триггерами для спауна врагов
        public void SpawnEnemy(string enemyTag)
        {
            if (_enemyDict.ContainsKey(enemyTag))
            {
                // Test kostyl
                if (enemyTag == "SneakerEnemy")
                {
                    StartCoroutine(DelaySpawn(enemyTag));
                }
                else
                {
                    RandomSpawn(_enemyDict[enemyTag], _enemySpawnPoints);
                }
            }
        }

        private IEnumerator DelaySpawn(string enemyTag)
        {
            yield return new WaitForSeconds(0f);
            RandomSpawn(_enemyDict[enemyTag], _enemySpawnPoints);
        }

        // Подумать над триггерами для респауна орбов
        public void SpawnOrb(string orbTag)
        {
            Instantiate(_orb, new Vector2(), new Quaternion());
        }

        private void RandomSpawn(GameObject obj, in List<Vector2> spawnPoints)
        {
            Vector2 spawnPoint = new(0, 0);
            List<Collider2D> results = new();
            List<Collider2D> parallelResults = new();
            for (int i = 0, randInd = Random.Range(0, spawnPoints.Count); i < spawnPoints.Count; i++, randInd = (randInd + 1) % spawnPoints.Count)
            {
                spawnPoint = spawnPoints[randInd];
                if (CheckPosition(spawnPoint, results, parallelResults))
                {
                    break;
                }
            }
            Instantiate(obj, spawnPoint, new Quaternion());
        }

        private bool CheckPosition(Vector2 spawnPoint, List<Collider2D> results, List<Collider2D> parallelResults)
        {
            Vector2 parallelSpawnPoint = new(-spawnPoint.x, -spawnPoint.y);
            Physics2D.OverlapCircle(spawnPoint, 0.4f, _contactFilter, results);
            Physics2D.OverlapCircle(parallelSpawnPoint, 0.4f, _contactFilter, parallelResults);
            // Подумать над коллизиями пи спауне (Возможно у врагов нет обычноq коллизии)
            foreach (var coll in results)
            {
                if (coll.gameObject.layer == (int)CollisionLayers.Enemy)
                {
                    return false;
                }
            }
            foreach (var coll in parallelResults)
            {
                if (coll.gameObject.layer == (int)CollisionLayers.Enemy)
                {
                    return false;
                }
            }
            return true;
        }

        private void MakeDict(List<GameObject> list, Dictionary<string, GameObject> dict)
        {
            foreach (var el in list)
            {
                dict[el.tag] = el;
            }
            list.Clear();
        }
    }
}
