using UnityEngine;
using MidiaAdapt.Core.Interfaces;
using UnityEngine.Pool;
using System.Collections;

namespace MidiaAdapt.Weapon.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        IObjectPool<Projectile> _bulletPool;
        public IObjectPool<Projectile> BulletPool { set => _bulletPool = value; }

        [SerializeField] int _damage;
        [SerializeField] float _lifeTime = 5f;
        [SerializeField] LayerMask _layerDestructive;

        Rigidbody _rb;

        void Start()
        {
            if (TryGetComponent<Rigidbody>(out _rb))
            {
                _rb = GetComponent<Rigidbody>();
            }
        }

        //Responsável por lançar o projétil e já iniciar a contagem para desativá-lo
        public void Launch(float speed)
        {
            if (_rb != null)
            {
                _rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
            }

            Deactivate();
        }


        //Responsável por desativar o projétil após um tempo
        public void Deactivate()
        {
            StartCoroutine(DeactivateRoutine(_lifeTime));
        }

        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            //Reseta a velocidade para evitar que o projétil continue se movendo quando for reutilizado
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            ReturnToPool();
        }

        //Devolve o projétil para a pool
        void ReturnToPool()
        {
            _bulletPool.Release(this);
        }

        void OnTriggerEnter(Collider other)
        {
            //Detecta todos os objetos que implementam a interface IDamageable (Player se precisar, Inimigo) faz com que seja modular e aplica o dano
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                ReturnToPool();
            }
            else if (((1 << other.gameObject.layer) & _layerDestructive) != 0) //Faz um bitwise AND para verificar se a layer do objeto colidido está no LayerMask de destruição (paredes, chão, etc)
            {
                ReturnToPool();
            }
        }

    }

}