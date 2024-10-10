using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponController : WeaponController
{
    [SerializeField] protected Projectile _projectile;

    public GameObject[] enemiesInRange;
    public GameObject nearestEnemy;
    float distance;
    float nearestDistance = 100;
    public override IEnumerator WeaponAttack()
    {
        Debug.Log("Weapon Attack");
        var data = Data as ProjectileWeaponData;
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(data.CoolDownTime);

            Debug.Log("Search Enemies");
            enemiesInRange = GameObject.FindGameObjectsWithTag("Enemy");
            SearchNearestEnemy();

            //Vector2 targetPosition = nearestEnemy.transform.position;       //Y U ERROR


            for (int i = 0; i < data.Amount; i++)
            {

                var weapon = Instantiate(_projectile, transform);
                weapon.Initialize(data);//, targetPosition);
                Debug.Log("Spawn shuriken");
                nearestDistance = 100;
            }
        }
    }


    public void SearchNearestEnemy()
    {
        for (int i = 0; i < enemiesInRange.Length;  i++)
        {
            distance = Vector2.Distance(this.transform.position, enemiesInRange[i].transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = enemiesInRange[i];
                nearestDistance = distance;
            }
        }
    }

}
