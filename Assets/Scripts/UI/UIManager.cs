using UnityEngine;

namespace Player47.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] GameObject _HUDGameplay;

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

        public void ShowHUDGameplay(bool isVisible)
        {
            _HUDGameplay.SetActive(isVisible);
        }
    } 
}
