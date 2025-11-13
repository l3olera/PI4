using Codice.Client.Common;
using System.Collections;
using UnityEngine;

namespace Player47.Domain.Player
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] AudioSource m_audioSource;

        [Header("Clipes de Áudio - Passos")]
        [SerializeField] AudioClip[] m_footstepConcreteClips;
        [SerializeField] AudioClip[] m_footstepForestClips;
        [SerializeField] AudioClip[] m_footstepRockyClips;

        [Header("Clipes de Áudio - Pulo")]
        [SerializeField] AudioClip[] m_jumpConcreteClips;
        [SerializeField] AudioClip[] m_jumpForestClips;
        [SerializeField] AudioClip[] m_jumpRockyClips;

        [Header("Terreno")]
        public LayerMask groundLayer;

        [Space]
        [SerializeField] string m_concreteTag = "Concrete";
        [SerializeField] string m_forestTag = "Forest";
        [SerializeField] string m_rockyTag = "Rocky";

        [Header("Configurações de Corrida")]
        [SerializeField] float walkVolume = 0.7f;
        [SerializeField] float runVolume = 1f;
        [SerializeField] float walkPitch = 1f;
        [SerializeField] float runPitch = 1.25f;

        bool m_jumpingAudioIsPlaying = false;

        void Awake()
        {
            if (m_audioSource == null)
            {
                m_audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // ===============================
        // PASSOS
        // ===============================
        public void PlayFootstep(bool isRunning)
        {
            if (m_audioSource == null || m_audioSource.isPlaying) return;

            AudioClip[] audios = CheckSoundToTheGround(SurfaceSoundType.Footstep);
            if (audios == null || audios.Length == 0) return;

            m_audioSource.volume = isRunning ? runVolume : walkVolume;
            m_audioSource.pitch = isRunning ? runPitch : walkPitch;
            m_audioSource.clip = ChooseRandomClip(audios);
            m_audioSource.Play();
        }

        // ===============================
        // PULO
        // ===============================
        public void PlayJumpSound()
        {
            if(m_audioSource == null || m_jumpingAudioIsPlaying) return;

            AudioClip[] audios = CheckSoundToTheGround(SurfaceSoundType.Jump);
            if (audios == null || audios.Length == 0) return;

            m_audioSource.pitch = Random.Range(0.95f, 1.05f);
            m_audioSource.clip = ChooseRandomClip(audios);
            m_audioSource.Play();
            m_jumpingAudioIsPlaying = true;
            StartCoroutine(ResetJumpingAudioFlagAfterDelay(m_audioSource.clip.length));
        }

        IEnumerator ResetJumpingAudioFlagAfterDelay(float length)
        {
            yield return new WaitForSeconds(length);
            m_jumpingAudioIsPlaying = false;
        }

        // ===============================
        // VERIFICAÇÃO DO TERRENO
        // ===============================
        enum SurfaceSoundType { Footstep, Jump }

        AudioClip[] CheckSoundToTheGround(SurfaceSoundType soundType)
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f, groundLayer))
            {
                if (hit.collider.CompareTag(m_concreteTag))
                    return soundType == SurfaceSoundType.Footstep ? m_footstepConcreteClips : m_jumpConcreteClips;

                if (hit.collider.CompareTag(m_forestTag))
                    return soundType == SurfaceSoundType.Footstep ? m_footstepForestClips : m_jumpForestClips;

                if (hit.collider.CompareTag(m_rockyTag))
                    return soundType == SurfaceSoundType.Footstep ? m_footstepRockyClips : m_jumpRockyClips;
            }

            return null;
        }

        AudioClip ChooseRandomClip(AudioClip[] clips)
        {
            return clips[Random.Range(0, clips.Length)];
        }
    }
}
