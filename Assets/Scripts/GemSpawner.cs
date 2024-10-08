using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : Singleton<GemSpawner>
{
    [SerializeField] int amount = 100;
    [SerializeField] Gem prefab;

    private Stack<Gem> _availableGems = new();

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var gem = Instantiate(prefab, transform);
            gem.gameObject.SetActive(false);
            _availableGems.Push(gem);
        }
    }

    public Gem GetGem()
    {
        var gem = _availableGems.Pop();
        gem.gameObject.SetActive(true);
        return gem;
    }

    public void ReturnGem(Gem gem)
    {
        _availableGems.Push(gem);
    }
}
