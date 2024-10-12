using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _description;

    private WeaponData _data;

    public void Initialize(WeaponData data)
    {
        _data = data;
        _icon.sprite = data.Sprite;
        _name.text = data.name;
        _description.text = data.Description;
    }

    public void SelectUpgrade()
    {
        WeaponManager.Instance.ActivateWeapon(_data);
    }
}
