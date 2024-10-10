using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponController
{
    [SerializeField] Animator _animator;
    public override IEnumerator WeaponAttack()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Data.CoolDownTime);
            _animator.SetTrigger(PlayerController.Instance.SpriteRenderer.flipX ? "sword_left" : "sword_right");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemy)){
            print($"Trigger: {enemy.name}");
            enemy.TakeDamage(Data.DamageAmount);
        }
    }
}
