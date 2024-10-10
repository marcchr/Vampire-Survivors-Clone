using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private float DamageAmount;
    private int Health;

    public void Initialize(ProjectileWeaponData data, Vector2 targetPosition)
    {
        DamageAmount = data.DamageAmount;
        Health = data.HitsToTake;
        Destroy(gameObject, 5f);


        StartCoroutine(MoveToTarget(data, targetPosition));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.TakeDamage(DamageAmount);
            Health--;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MoveToTarget(ProjectileWeaponData data, Vector2 targetPosition)
    {
        float time = 0;
        Vector2 startPosition = transform.position;
        //Vector2 targetPosition = new Vector2(1, 1);
        
        while (time < data.Speed)
        {
            transform.position = Vector2.MoveTowards(startPosition, targetPosition, time * data.Speed);//*Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        
        // Destroy(gameObject);
    }

    
}
