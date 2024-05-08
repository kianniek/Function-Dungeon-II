using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Targets
{
    public class HitScript : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDieEvent = new();

        [SerializeField] private int hp = 3;

        [SerializeField] private bool damageable = true;

        [SerializeField] private Material damageColor;

        private Material _startMaterial;

        private SpriteRenderer _spriteRenderer;

        [SerializeField] private GameObject bloodsprayParticles;

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            _startMaterial = _spriteRenderer.material;
        }

        public void OnBlockHit(int damage)
        {
            if (damageable)
            {
                hp = hp - damage;
                StartCoroutine(FlashRed());
                if (hp <= 0)
                {
                    Destroy(gameObject);
                    onDieEvent.Invoke();
                }
            }
        }

        //TODO:
        //impact from fall
        //impact from things falling on it

        //  TODO: Use this inside the class to make it clear that it is a method for this class
        // Or move to separate script
        public void EnemyOnDie()
        {
            //Particles should already exist, remove instantiate
            Instantiate(bloodsprayParticles, transform.position, Quaternion.identity);

            //Change this
            GameManager.instance._enemyKillCount++;
        }

        private IEnumerator FlashRed()
        {
            _spriteRenderer.material = damageColor;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = _startMaterial;
        }
    }
}
