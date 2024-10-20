using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] UpgradeItem _upgradePrefab;
    List<UpgradeItem> _upgrades = new();

    private void OnEnable()
    {
        foreach (var weapon in WeaponManager.Instance.Weapons)
        {
            var upgrade = Instantiate(_upgradePrefab, transform);
            upgrade.Initialize(weapon);
            upgrade.GetComponent<Button>().onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                GameManager.Instance.EndLevelUp();
            });
            _upgrades.Add(upgrade);
        }
    }

    private void OnDisable()
    {
        foreach (var upgrade in _upgrades)
        {
            Destroy(upgrade.gameObject);
        }

        _upgrades.Clear();
    }
}
