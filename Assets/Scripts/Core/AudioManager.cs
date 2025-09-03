using UnityEngine;
using UnityEngine.Audio;

namespace MidiaAdapt.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioMixer _mainMixer;

        public void SetMasterVolume(float amount)
        {
            _mainMixer.SetFloat("VolumeMaster", 20 * Mathf.Log10(amount)); //Calcula para deixar proporcional com base no slider para decibéis
        }

        public void SetMusicVolume(float amount)
        {
            _mainMixer.SetFloat("VolumeMusic", 20 * Mathf.Log10(amount));
        }

        public void SetSFXVolume(float amount)
        {
            _mainMixer.SetFloat("VolumeSFX", 20 * Mathf.Log10(amount));
        }
    }

}