using UnityEngine;
public class WolfSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wolfDamageSound;
    [SerializeField] private AudioClip wolfDeathSound;
    [SerializeField] private AudioClip[] wolfHurtSounds;

    public void PlayDamageSound()
    {
        if (wolfDamageSound == null)
        {
            Debug.LogWarning("wolfDamageSound не назначен!");
            return;
        }

        Debug.Log("ѕроигрываетс€ звук wolfDamageSound.");
        audioSource.PlayOneShot(wolfDamageSound);
    }

    public void PlayDeathSound()
    {
        if (wolfDeathSound == null)
        {
            Debug.LogWarning("wolfDeathSound не назначен!");
            return;
        }

        Debug.Log("ѕроигрываетс€ звук wolfDeathSound.");
        audioSource.PlayOneShot(wolfDeathSound);
    }

    public void PlayRandomHurtSound()
    {
        if (wolfHurtSounds == null || wolfHurtSounds.Length == 0)
        {
            Debug.LogWarning("wolfHurtSounds массив пуст или не назначен!");
            return;
        }

        int randomIndex = Random.Range(0, wolfHurtSounds.Length);
        Debug.Log($"ѕроигрываетс€ случайный звук wolfHurtSounds[{randomIndex}].");
        audioSource.PlayOneShot(wolfHurtSounds[randomIndex]);
    }
}
