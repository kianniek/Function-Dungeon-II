using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitScript : MonoBehaviour
{
    public UnityEvent onDieEvent;

    [SerializeField]
    private int maxHp = 3, hp = 3, damageOnHit = 1;

    [SerializeField]
    private bool damageable = true, testBool = false;

    [SerializeField]
    private Material damageColor;

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
        if (damageable)
        {
            hp = hp - damage;
            StartCoroutine(FlashRed());
            if (hp <= 0)
            {
                onDieEvent.Invoke();
            }
        }
    }

    //impact from fall
    //impact from things falling on it

    void FixedUpdate()
    {
        if (testBool)
        {
            OnBlockHit(damageOnHit);
            testBool = false;
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
        _spriteRenderer.material = damageColor;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.material = _startMaterial;
    }
}
