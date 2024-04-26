using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 3, hp = 3, damageOnHit = 1;

    [SerializeField]
    private bool damageable = true, testBool = false;

    [SerializeField]
    private Material damageColor;

    private Material startMaterial;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        startMaterial = spriteRenderer.material;
    }

    void OnBlockHit(int damage)
    {
        if (damageable)
        {
            hp = hp - damage;
            StartCoroutine(FlashRed());
            if (hp <= 0)
            {
                Destroy(this.gameObject);
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

    private IEnumerator FlashRed()
    {
        spriteRenderer.material = damageColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = startMaterial;
    }
}
