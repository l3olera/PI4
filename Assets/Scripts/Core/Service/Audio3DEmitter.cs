using UnityEngine;

namespace Player47.Core.Service
{
    [RequireComponent(typeof(AudioSource))]
    public class Audio3DEmitter : MonoBehaviour, IAudioEmitter
    {
        [SerializeField] AudioClip interactionClip;
        AudioSource source;

        public bool IsPlaying => source.isPlaying;

        void Awake()
        {
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 1f; // 3D
        }

        public void PlaySound()
        {
            if (interactionClip != null)
            {
                Debug.Log("Playing 3D sound: " + interactionClip.name);
                source.PlayOneShot(interactionClip);
            }
        }
    }
}