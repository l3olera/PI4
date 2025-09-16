using UnityEngine;

namespace Player47.Domain.Souls
{
    public class SoulsManager : MonoBehaviour
    {
        public static SoulsManager Instance { get; private set; }

        int _souls = 0;
        public int souls { get; private set; }

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

        public void AddSouls(int amount)
        {
            _souls += amount;
        }
    }
}
