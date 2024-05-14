using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Targets
{
    public class HitScript : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDieEvent = new();

        [SerializeField] private float hp = 3;

        [SerializeField] private bool damageable = true;

        [SerializeField] private Material damageColor;

        [SerializeField] private GameObject bloodsprayParticles;

        private Material _startMaterial;

        private SpriteRenderer _spriteRenderer;

        public UnityEvent OnDieEvent { get => onDieEvent; }

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            _startMaterial = _spriteRenderer.material;
        }

        public void OnBlockHit(float damage)
        {
            if (damageable)
            {
                hp = hp - damage;
                StartCoroutine(FlashRed());
                if (hp <= 0)
                {
                    if(bloodsprayParticles != null)
                    {
                        bloodsprayParticles.transform.position = transform.position;
                        bloodsprayParticles.SetActive(true);
                    }
                    gameObject.SetActive(false);
                    onDieEvent.Invoke();
                }
            }
        }

        //TODO:
        //impact from fall
        //impact from things falling on it

        private IEnumerator FlashRed()
        {
            _spriteRenderer.material = damageColor;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = _startMaterial;
        }
    }
}
