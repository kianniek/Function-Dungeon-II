using System.Collections;
using Projectile;
using UnityEngine;
using UnityEngine.Events;

namespace Targets
{
    public class HitScript : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDieEvent = new();

        [SerializeField] private UnityEvent onDamageEvent = new();

        [SerializeField] private float hp = 3;

        [SerializeField] private Material damageColor;

        [SerializeField] private GameObject bloodsprayParticles;

        private const float PhysicDamageTreshold = 2f; // The treshold of damage the block needs to recieve for it to call the OnBlockHit event

        private const float StartInvulnerabilityTime = 1f; // The time the block is invulnerable for at the start of the game. To prevent block breaking when the level starts

        private bool _damageable;

        private Material _startMaterial;

        private SpriteRenderer _spriteRenderer;

        public UnityEvent OnDamageEvent { get => onDamageEvent; }
        public UnityEvent OnDieEvent { get => onDieEvent; }

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            _startMaterial = _spriteRenderer.material;
            StartCoroutine(EnableDamage());
        }

        public void OnBlockHit(float damage)
        {
            if (_damageable)
            {
                hp = hp - damage;
                onDamageEvent.Invoke();
                if (hp <= 0)
                {
                    if (bloodsprayParticles != null)
                    {
                        bloodsprayParticles.SetActive(true);
                    }
                    onDieEvent.Invoke();
                    gameObject.SetActive(false);
                }
                else
                {
                    StartCoroutine(FlashRed());
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<ProjectileScript>(out var _))
            {
                return;
            }

            var relativeVelocity = collision.relativeVelocity.magnitude;
            if (relativeVelocity > PhysicDamageTreshold)
            {
                OnBlockHit(relativeVelocity);
            }
        }

        private IEnumerator FlashRed()
        {
            _spriteRenderer.material = damageColor;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = _startMaterial;
        }

        /// <summary>
        /// Waits an amount of seconds equal to the StartInvulnerabilityTime variable, afterwards enables the object to be damageable
        /// </summary>
        private IEnumerator EnableDamage()
        {
            yield return new WaitForSeconds(StartInvulnerabilityTime);
            _damageable = true;
        }
    }
}
