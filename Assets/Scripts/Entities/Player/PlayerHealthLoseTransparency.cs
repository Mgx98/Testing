using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealthLoseTransparency : MonoBehaviour
{
    public Material playerGhostMaterial;
    public Volume postProcessingVolume;
    public AudioSource audioSource;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        Reset();

        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        Color color = playerGhostMaterial.color;

        color.a = Remap(_playerHealth.health, 0, _playerHealth.maxHealth, 0, 1);

        playerGhostMaterial.color = color;

        playerGhostMaterial.mainTextureOffset = new Vector2(
            playerGhostMaterial.mainTextureOffset.x + 0.0002f + Mathf.Pow((_playerHealth.maxHealth - _playerHealth.health) * 0.003f, 4),
            playerGhostMaterial.mainTextureOffset.y + 0.0002f + Mathf.Pow((_playerHealth.maxHealth - _playerHealth.health) * 0.003f, 4)
        );

        postProcessingVolume.weight = Remap(_playerHealth.maxHealth - _playerHealth.health, _playerHealth.maxHealth/2, _playerHealth.maxHealth, 0, 1);
        postProcessingVolume.weight = Mathf.Clamp(postProcessingVolume.weight, 0, 1);

        audioSource.pitch = Mathf.Clamp(1f - Mathf.Max(0, postProcessingVolume.weight * 2), 0.85f, 1f);
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnDestroy()
    {
        Reset();
    }

    private void Reset()
    {
        Color color = playerGhostMaterial.color;

        color.a = 1;

        playerGhostMaterial.color = color;

        playerGhostMaterial.mainTextureOffset = new Vector2(0, 0);

        postProcessingVolume.weight = 0;
    }
}