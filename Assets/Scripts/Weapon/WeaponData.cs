using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapons/New Weapon Data")]

public class WeaponData : ScriptableObject
{
    [field: SerializeField] public WeaponController WeaponPrefab { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public float CoolDownTime { get; private set; }
    [field: SerializeField] public float DamageAmount { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
}
