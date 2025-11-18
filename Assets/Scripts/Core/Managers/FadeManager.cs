using UnityEngine;

namespace Player47.Core.Managers
{
    public class FadeManager : MonoBehaviour
    {
        public static FadeManager Instance { get; private set; }

        [SerializeField] Animator m_animFade;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void FadeToBlack()
        {
            m_animFade.SetTrigger("FadeOut");
        }

        public void FadeFromBlack()
        {
            m_animFade.SetTrigger("FadeIn");
        }
    }

}