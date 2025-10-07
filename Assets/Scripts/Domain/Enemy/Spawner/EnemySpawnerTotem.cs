using UnityEngine;

namespace Player47.Domain.Enemy.Spawner
{
    public class EnemySpawnerTotem : MonoBehaviour
    {
        public bool isActive = true;

        [Header("Configuração do Spawn")]
        [SerializeField] GameObject m_enemyPrefab;
        [SerializeField] Transform[] m_wayPoints;
        [SerializeField] Transform[] m_spawnPoints;
        [SerializeField] float m_spawnInterval = 5f;
        [SerializeField] int m_maxEnemiesAlive = 5;

        float m_timer;
        int m_enemiesAlive = 0;

        void Update()
        {
            if (!isActive) return;

            m_timer += Time.deltaTime;

            if (m_timer >= m_spawnInterval && m_enemiesAlive < m_maxEnemiesAlive)
            {
                SpawnEnemy();
                m_timer = 0;
            }
        }

        void SpawnEnemy()
        {
            GameObject enemyGO = Instantiate(m_enemyPrefab, GetRandomWayPoint().position, Quaternion.identity);
            
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            // Aqui, sempre que o inimigo morrer, ele deve avisar o spawner
            enemy.OnDeath += HandleEnemyDeath;

            enemy.SetWaypoint(m_wayPoints);

            m_enemiesAlive++;
        }

        Transform GetRandomWayPoint()
        {
            return m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];
        }

        void HandleEnemyDeath(Enemy enemy)
        {
            m_enemiesAlive--;
        }

        // Método para desativar o totem
        public void DisableTotem()
        {
            isActive = false;
            Debug.Log("Totem desativado!");
        }
    }

}