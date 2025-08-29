using UnityEngine;
using MidiaAdapt.Core.Interfaces;

namespace MidiaAdapt.Weapon.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;
        [SerializeField] int _damage;
        [SerializeField] float _lifeTime = 2f;
        [SerializeField] LayerMask _layerDestructive;

        Rigidbody _rb;

        void Start()
        {
            if (TryGetComponent<Rigidbody>(out _rb))
            {
                _rb = GetComponent<Rigidbody>();
                _rb.linearVelocity = transform.forward * _speed;
            }

            Destroy(gameObject, _lifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
            }
            else if (((1 << other.gameObject.layer) & _layerDestructive) != 0) //Faz um bitwise AND para verificar se a layer do objeto colidido está no LayerMask
            {
                Destroy(gameObject);
            }
        }
    }

}