using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitScript : MonoBehaviour
{
    public UnityEvent onDieEvent;

    [SerializeField]
    private int _maxHp = 3, _hp = 3, _damageOnHit = 1;

    [SerializeField]
    private bool _damageable = true, _testBool = false;

    [SerializeField]
    private Material _damageColor;

    private Material _startMaterial;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private GameObject _bloodsprayParticles;
    private void Start()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _startMaterial = _spriteRenderer.material;
    }

    void OnBlockHit(int damage)
    {
        if (_damageable)
        {
            _hp = _hp - damage;
            StartCoroutine(FlashRed());
            if (_hp <= 0)
            {
                onDieEvent.Invoke();
            }
        }
    }

    //impact from fall
    //impact from things falling on it

    void FixedUpdate()
    {
        if (_testBool)
        {
            OnBlockHit(_damageOnHit);
            _testBool = false;
        }
    }
    public void OnDie()
    {
        Destroy(this.gameObject);
    }

    public void EnemyOnDie()
    {
        Instantiate(_bloodsprayParticles, transform.position, Quaternion.identity);
        GameManager.instance._enemyKillCount++;
    }
    private IEnumerator FlashRed()
    {
        _spriteRenderer.material = _damageColor;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.material = _startMaterial;
    }
}
