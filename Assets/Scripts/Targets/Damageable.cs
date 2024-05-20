using System.Collections;
using Projectile;
using UnityEngine;
using UnityEngine.Events;

namespace Targets
{
    public class Damageable : MonoBehaviour
    {
        [Header("Target Settings")]
        [Tooltip("The threshold of damage the block needs to receive for it to call the OnBlockHit event")]
        [SerializeField] private float physicDamageThreshold = 2f;
        [Tooltip("The time the block is invulnerable for at the start of the game. To prevent block breaking when the level starts")]
        [SerializeField] private float startInvulnerabilityTime = 1f;
        [SerializeField] private float startHealth = 3;
        [SerializeField] private bool enableNegativeHealth;
        
        [Header("Damage Effect")] 
        [SerializeField] private GameObject spriteView;
        [SerializeField] private bool enableDamageEffect;
        [SerializeField] private Color damageColor;
        
        [Header("Events")] 
        [SerializeField] private UnityEvent onDieEvent = new();
        [SerializeField] private UnityEvent onDamageEvent = new();
        
        private bool _damageable;
        private float _health;
        private Material _material;
        private Color _startColor;
        
        public UnityEvent OnDamageEvent => onDamageEvent;
        public UnityEvent OnDieEvent => onDieEvent;
        
        public float Health
        {
            get => _health;
            set
            {
                if (!_damageable)
                    return;
                
                onDamageEvent.Invoke();
                
                if (!enableNegativeHealth && value < 0)
                    _health = 0;
                else
                    _health = value;
                
                if (value <= 0)
                    onDieEvent.Invoke();
                else if (enableDamageEffect)
                    StartCoroutine(FlashRed());
            }
        }
        
        private void Awake()
        {
            if (spriteView && spriteView.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                _material = spriteRenderer.material;
            }
            else
            {
                _material = GetComponent<SpriteRenderer>().material;
            }
            
            switch (enableDamageEffect)
            {
                case true when !_material && !spriteView:
                    Debug.LogError($"No material found on {spriteView.name}!");
                    break;
                case true when !_material:
                    Debug.LogError($"No material found on {gameObject.name}!");
                    break;
            }
        }
        
        private void Start()
        {
            _startColor = _material.color;
            _health = startHealth;
            
            StartCoroutine(EnableDamage());
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<ProjectileScript>(out _))
                return;
            
            var relativeVelocity = collision.relativeVelocity.magnitude;
            
            if (relativeVelocity > physicDamageThreshold)
                Health -= relativeVelocity;
        }
        
        private IEnumerator FlashRed()
        {
            _material.color = damageColor;
            
            yield return new WaitForSeconds(0.1f);
            
            _material.color = _startColor;
        }
        
        // Waits an amount of seconds equals to the StartInvulnerabilityTime variable,
        // afterward enables the object to be damageable
        private IEnumerator EnableDamage()
        {
            yield return new WaitForSeconds(startInvulnerabilityTime);
            _damageable = true;
        }
    }
}