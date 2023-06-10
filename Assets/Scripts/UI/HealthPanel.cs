using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthPanel : MonoBehaviour
{
    private Text _healthText;

    [SerializeField]
    private int _minHealth = 5;

    [Inject]
    private Player _player;

    private void Start()
    {
        _player.PlayerChangeHealthEvent += ChangeHealth;
        Restart();
    }

    public void Restart()
    {
        _healthText = GetComponent<Text>();
        _healthText.text = _player.GetCurrentHealth().ToString();
        _healthText.color = Color.yellow;
    }

    private void ChangeHealth()
    {
        var health = _player.GetCurrentHealth();
        _healthText.text = health.ToString();
        if (health < 0)
            _healthText.text = "0";

        if (health <= _minHealth)
            _healthText.color = Color.red;
        else _healthText.color = Color.yellow;
    }

    private void OnDestroy()
        => _player.PlayerChangeHealthEvent -= ChangeHealth;
}
