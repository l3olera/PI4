using UnityEngine;
using Player47.Core.Service.Triggers;
using Player47.Domain.Combat;
using UnityEngine.Events;

namespace Player47.Domain.Trap
{
    [RequireComponent(typeof(Collider))]
    public class Trap : TriggerSetup
    {
        [Header("Eventos")]
        public UnityEvent Trapped;

        [Header("Configurações de Dano")]
        public int damage = 10;

        [Header("Animação")]
        [SerializeField] Animator m_anim;

        [Header("Áudio da Armadilha")]
        [SerializeField] AudioClip m_trapCloseClip;

        AudioSource m_audioSource;
        bool m_isActivated;

        // =====================================
        // INICIALIZAÇÃO
        // =====================================
        protected override void Awake()
        {
            base.Awake();

            if (m_anim == null)
                m_anim = GetComponent<Animator>();

            // Cria automaticamente o AudioSource
            SetupAudioSource();
        }

        void SetupAudioSource()
        {
            if(gameObject.TryGetComponent<AudioSource>(out AudioSource existingAudioSource))
            {
                m_audioSource = existingAudioSource;
            }  

            if(m_audioSource == null)
                m_audioSource = gameObject.AddComponent<AudioSource>();

            m_audioSource.playOnAwake = false;
            m_audioSource.spatialBlend = 1f;     // 3D sound (segue posição da trap)
            m_audioSource.minDistance = 3f;
            m_audioSource.maxDistance = 15f;
            m_audioSource.volume = 0.3f;
        }

        // =====================================
        // ATIVAÇÃO
        // =====================================
        void OnTriggerEnter(Collider other)
        {
            if (m_isActivated) return;

            if (other.CompareTag("Player") && other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                m_isActivated = true;

                // Toca som e animação
                PlayTrapSound(m_trapCloseClip);
                m_anim?.SetTrigger("Close");

                // Evento + dano
                Trapped?.Invoke();
                damageable.TakeDamage(damage);
            }
        }

        // =====================================
        // SOM
        // =====================================
        void PlayTrapSound(AudioClip clip)
        {
            if (clip == null || m_audioSource == null) return;
            m_audioSource.pitch = Random.Range(0.95f, 1.05f);
            m_audioSource.PlayOneShot(clip);
        }
    }
}
