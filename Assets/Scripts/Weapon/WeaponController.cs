using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    protected WeaponData Data;

    public virtual void Initialize(WeaponData data)
    {
        Data = data;
        StartCoroutine(WeaponAttack());
    }

    public abstract IEnumerator WeaponAttack();
}
