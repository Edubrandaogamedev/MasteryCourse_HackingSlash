using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    
    private Character currentCharacter;
    private void Awake()
    {
        var player = GetComponentInParent<PlayerController>();
        player.OnCharacterChanged += OnCharacterChanged;
        gameObject.SetActive(false);
    }

    private void OnCharacterChanged(Character character)
    {
        currentCharacter = character;
        currentCharacter.OnHealthChanged += HandleHealthChanged;
        currentCharacter.OnDied += OnCharacterDied;
        gameObject.SetActive(true);
    }

    private void OnCharacterDied(IDie character)
    {
        character.OnHealthChanged -= HandleHealthChanged;
        character.OnDied -= OnCharacterDied;
        currentCharacter = null;
        gameObject.SetActive(false);
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        var percentage = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = percentage;
    }
}
