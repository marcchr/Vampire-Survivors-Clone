using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapons/New Projectile Weapon Data")]

public class ProjectileWeaponData : WeaponData
{
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public int Speed { get; private set; }
    [field: SerializeField] public int HitsToTake { get; private set; }

}
