using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float _duration = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTowardsPlayer(other.transform));

        }
    }


    private IEnumerator MoveTowardsPlayer(Transform player)
    {
        float time = 0;
        Vector2 startPosition = transform.position;


        while (time < _duration)
        {
            transform.position = Vector2.Lerp(startPosition, player.position, time / _duration);
            time += Time.deltaTime;
            yield return null;
        }

        ExperienceManager.Instance.TotalExperiencePoints += value;
        GemSpawner.Instance.ReturnGem(this);
        gameObject.SetActive(false);
    }
}
