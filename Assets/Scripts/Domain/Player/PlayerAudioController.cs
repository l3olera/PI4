using System.Collections;
using UnityEngine;

namespace Player47.Domain.Player
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] AudioSource m_audioSource;

        [Header("Clipes de Audios")]
        [SerializeField] AudioClip[] m_footstepConcreteClips;
        [SerializeField] AudioClip[] m_footstepForestClips;
        [SerializeField] AudioClip[] m_footstepRockyClips;

        [Header("Terreno")]
        public LayerMask groundLayer;

        [Space]
        [SerializeField] string m_concreteTag = "Concrete";
        [SerializeField] string m_forestTag = "Forest";
        [SerializeField] string m_rockyTag = "Rocky";

        void Awake()
        {
            if (m_audioSource == null)
            {
                m_audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void PlaySoundFootstep()
        {
            if (m_audioSource == null || (m_audioSource != null && m_audioSource.isPlaying)) return;

            AudioClip[] audios = CheckSoundToTheGround();
            if (audios == null) return;

            m_audioSource.clip = ChoiseRandomClip(audios);
            m_audioSource.Play();
        }

        AudioClip ChoiseRandomClip(AudioClip[] clips)
        {
            return clips[Random.Range(0, clips.Length)];
        }

        AudioClip[] CheckSoundToTheGround()
        {
            AudioClip[] audioClips = null;
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f, groundLayer))
            {
                if (hit.collider.CompareTag(m_concreteTag))
                {
                    audioClips = m_footstepConcreteClips;
                }
                else if (hit.collider.CompareTag(m_forestTag))
                {
                    audioClips = m_footstepForestClips;
                }
                else if (hit.collider.CompareTag(m_rockyTag))
                {
                    audioClips = m_footstepRockyClips;
                }
            }
            return audioClips;
        }
    }
}