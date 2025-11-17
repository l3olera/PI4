using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player47.Domain.Enemy
{
    /// <summary>
    /// Gerencia os sons ambientes dos inimigos, garantindo que apenas um inimigo
    /// de cada tipo toque o som ambiente em um dado momento.
    /// </summary>
    public class EnemyAmbientSoundManager : MonoBehaviour
    {
        private static EnemyAmbientSoundManager _instance;
        public static EnemyAmbientSoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("EnemyAmbientSoundManager");
                    _instance = obj.AddComponent<EnemyAmbientSoundManager>();
                }
                return _instance;
            }
        }

        readonly Dictionary<IdEnemy, bool> _isPlayingSound = new();
        readonly Dictionary<IdEnemy, float> _nextSoundTime = new();

        [SerializeField, Min(2f)]
        private float defaultInterval = 8f;

        public void TryPlayAmbientSound(Enemy enemy, AudioClip[] clips, float? intervalOverride = null)
        {
            if (clips == null || clips.Length <= 0 || enemy == null || enemy.stats == null)
                return;

            IdEnemy type = enemy.stats.idEnemy;
            float currentTime = Time.time;

            // Inicializa dicionários se não houver dados desse tipo ainda
            if (!_isPlayingSound.ContainsKey(type))
            {
                _isPlayingSound[type] = false;
                _nextSoundTime[type] = currentTime + Random.Range(1f, 3f);
            }

            // Se ainda não chegou o momento do próximo som, sai
            if (currentTime < _nextSoundTime[type])
                return;

            // Se já há um som sendo tocado por esse tipo, sai
            if (_isPlayingSound[type])
                return;

            // Começa a tocar o som
            _isPlayingSound[type] = true;
            enemy.StartCoroutine(PlaySoundRoutine(type, enemy, clips, intervalOverride ?? defaultInterval));
        }

        IEnumerator PlaySoundRoutine(IdEnemy type, Enemy enemy, AudioClip[] clips, float interval)
        {
            AudioClip clipChosen = clips[Random.Range(0, clips.Length)];
            enemy.AudioSource.PlayOneShot(clipChosen);
            yield return new WaitForSeconds(clipChosen.length);

            _isPlayingSound[type] = false;
            _nextSoundTime[type] = Time.time + interval + Random.Range(-2f, 2f);
        }
    }
}