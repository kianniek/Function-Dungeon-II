using System.Collections;
using UnityEngine;

namespace Health
{
    public class DamageEffect : MonoBehaviour
    {
        [Header("Separate View")]
        [SerializeField] private GameObject view;
        
        [Header("Color Visual")] 
        [SerializeField] private bool applyColorVisual = true;
        [SerializeField] private Color damageColor = Color.red;
        [SerializeField] private float colorDuration = 0.1f;
        
        [Header("Sprite Visual")]
        [SerializeField] private bool applySpriteVisual;
        [SerializeField] private Sprite damageSprite;
        [SerializeField] private float spriteDuration = 0.1f;
        
        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private Color _startColor;
        private Sprite _startSprite;
        
        private void Awake()
        {
            if (view && view.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                _spriteRenderer = spriteRenderer;
            else
                _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (!_spriteRenderer)
                Debug.Log("There is no sprite renderer on this object or SpriteView!");
            else
            {
                _material = _spriteRenderer.material;
                _startColor = _material.color;
                _startSprite = _spriteRenderer.sprite;
            }
        }
        
        /// <summary>
        /// Start the damage effect.
        /// </summary>
        public void ApplyDamageEffect()
        {
            if (!_spriteRenderer)
                return;
            
            if (applyColorVisual)
                StartCoroutine(FlashColor());
            
            if (applySpriteVisual)
                StartCoroutine(FlashSprite());
        }
        
        private IEnumerator FlashColor()
        {
            _material.color = damageColor;
            
            yield return new WaitForSeconds(colorDuration);
            
            _material.color = _startColor;
        }
        
        private IEnumerator FlashSprite()
        {
            _spriteRenderer.sprite = damageSprite;
            
            yield return new WaitForSeconds(spriteDuration);
            
            _spriteRenderer.sprite = _startSprite;
        }
    }
}