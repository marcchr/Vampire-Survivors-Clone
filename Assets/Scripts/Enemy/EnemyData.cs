using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemies/New Enemy Data")]

public class EnemyData : ScriptableObject
{
    [field: SerializeField] public EnemyController EnemyPrefab { get; private set; }

    [field: SerializeField] public HordeController HordePrefab { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float GemDropChance { get; private set; }

}
