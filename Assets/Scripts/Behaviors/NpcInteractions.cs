using System;
using UnityEngine;

public class NpcInteractions : MonoBehaviour
{
    [Header("Info")] [SerializeField] private string _npcName;
    [SerializeField] private string _description;

    [Header("Stats")] [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [Header("Movement")] [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stoppingDistance = 1f;

    public string NPCName => _npcName;
    public bool IsAlive => _currentHealth > 0f;

    public event Action<NpcInteractions> OnDeath;
    public event Action<float> OnHealthChanged;

    protected virtual void Awake()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void Start() { }
    protected virtual void Update() { }

    // -- Health --
    public virtual void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0f, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0f)
            Die();
    }

    public virtual void Heal(float amount)
    {
        if (!IsAlive) return;
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0f, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke(this);
    }

    // -- Interaction --
    public virtual void Interact() { }
    public virtual void OnPlayerEnterRange() { }
    public virtual void OnPlayerExitRange() { }

    // -- State --
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}