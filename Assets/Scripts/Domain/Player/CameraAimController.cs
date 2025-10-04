using Player47.Core.Managers;
using System.Collections;
using UnityEngine;
using Cinemachine;

namespace Player47.Domain.Player
{
    /// <summary>
    /// Cuida da mudança/transição de FOV da mira do jogador com a arma
    /// </summary>
    public class CameraAimController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera m_playerCamera;
        [SerializeField] float m_normalFOV = 60f;   // FOV normal
        [SerializeField] float m_aimFOV = 40f;      // FOV quando mira
        [SerializeField] float m_aimSpeed = 10f;    // Velocidade da transição
        void OnEnable()
        {
            InputManager.AimInputPerformed += HandleAimInputPerformed;
        }

        void OnDisable()
        {
            InputManager.AimInputPerformed -= HandleAimInputPerformed;
        }

        void Start()
        {
            if (m_playerCamera == null)
            {
                m_playerCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
            }
        }

        void HandleAimInputPerformed(bool isAiming)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothFOV(isAiming ? m_aimFOV : m_normalFOV));
        }

        /// <summary>
        /// Transição suave entre normal e mira
        /// </summary>
        /// <param name="targetFOV">Fov alvo para ser transicionado</param>
        /// <returns></returns>
        IEnumerator SmoothFOV(float targetFOV)
        {
            while (Mathf.Abs(m_playerCamera.m_Lens.FieldOfView - targetFOV) > 0.1f)
            {
                m_playerCamera.m_Lens.FieldOfView  = Mathf.Lerp(
                    m_playerCamera.m_Lens.FieldOfView,
                    targetFOV,
                    Time.deltaTime * m_aimSpeed
                );
                yield return null;
            }

            m_playerCamera.m_Lens.FieldOfView = targetFOV; // garante que fixa no valor final
        }
    }
}