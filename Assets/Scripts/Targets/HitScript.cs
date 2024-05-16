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

        [SerializeField] private int points;

        private float _physicDamageTreshold = 2f; // The treshold of damage the block needs to recieve for it to call the OnBlockHit event

        private float _startInvulnerabilityTime = 1f; // The time the block is invulnerable for at the start of the game. To prevent block breaking when the level starts

        private bool _damageable;

        private Material _startMaterial;

        private SpriteRenderer _spriteRenderer;

        public UnityEvent OnDieEvent { get => onDieEvent; }
        public UnityEvent OnDamageEvent { get => onDamageEvent; }
        public int Points
        {
            get { return points; }
            private set { points = value; }
        }

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
                    gameObject.SetActive(false);
                    onDieEvent.Invoke();
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

            var relativeVelocty = collision.relativeVelocity.magnitude;
            if (relativeVelocty > _physicDamageTreshold)
            {
                OnBlockHit(collision.relativeVelocity.magnitude);
            }
        }

        private IEnumerator FlashRed()
        {
            _spriteRenderer.material = damageColor;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = _startMaterial;
        }

        private IEnumerator EnableDamage()
        {
            yield return new WaitForSeconds(_startInvulnerabilityTime);
            _damageable = true;
        }
    }
}
