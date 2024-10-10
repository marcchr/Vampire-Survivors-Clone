using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] Image _healthBarFill;

    SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    [SerializeField] Animator _animator;

    float _currentHealth = 0;

    Rigidbody2D _rb2D;


    Vector2 _moveDirection;
    public Vector2 MoveDirection => _moveDirection;

    protected override void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // _animator = GetComponent<Animator>();

    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        _moveDirection = new (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        _moveDirection.Normalize();

        _healthBarFill.fillAmount = _currentHealth / _maxHealth;

        if (_moveDirection.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_moveDirection.x > 0) 
        {
            _spriteRenderer.flipX = false;
        }

        if (Input.anyKey)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        _rb2D.MovePosition(_rb2D.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            _currentHealth--;
        }
    }



}
