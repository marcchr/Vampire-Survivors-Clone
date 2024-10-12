using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{

    public EnemyData Data;

    SpriteRenderer _spriteRenderer;


    [SerializeField] Animator _animator;

    EnemySpawner _spawner;

    [SerializeField] Transform _player;

    float _currentHealth;
    float _currentMoveSpeed;

    

    Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
       
    }

    public void Initialize(Transform player, EnemySpawner spawner, EnemyData data)
    {
       _player = player;
       _spawner = spawner;

        Data = data;
    }

    private void OnEnable()
    {
        ResetToDefault();
    }

    private void FixedUpdate()
    {
        _rb2D.MovePosition(Vector2.MoveTowards(_rb2D.position, _player.position, _currentMoveSpeed * Time.fixedDeltaTime));
        // _animator.Play("enemy1_default");

        if (transform.position.x - _player.position.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (transform.position.x - _player.position.x < 0)
        {
            _spriteRenderer.flipX = false;
        }

    }

    public void ResetToDefault()
    {
        _currentHealth = Data.MaxHealth;
        _spriteRenderer.color = Color.white;
        _currentMoveSpeed = Data.MoveSpeed;
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        StartCoroutine(FlashRed());
        if (_currentHealth <= 0f)
        {
            StartCoroutine(DeathAnim());

            GameManager.Instance.killCount++;

            _spawner.ReturnEnemyToPool(this);

            var coin = GemSpawner.Instance.GetGem();
            coin.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
        }
            
    }

    public IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = Color.white;
    }

    public IEnumerator DeathAnim()
    {
        _animator.SetTrigger("death");
        _currentMoveSpeed = 0f;
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
