using UnityEngine;

public class Artefact : MonoBehaviour
{
    public static int artefactsCollected = 0;
    private bool isCollected = false;

    public static event System.Action<int> OnArtefactCollected;

    public AudioClip collectSound;
    private AudioSource audioSource;

    public AudioClip proximitySound;
    private AudioSource proximityAudioSource;

    public float maxDistance = 10f;
    public float minVolume = 0.1f;
    public float maxVolume = 1f;

    private Transform player;

    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length < 2)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            proximityAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = sources[0];
            proximityAudioSource = sources[1];
        }

        audioSource.playOnAwake = false;
        proximityAudioSource.playOnAwake = false;
        proximityAudioSource.loop = true;
        proximityAudioSource.clip = proximitySound;
        proximityAudioSource.volume = 0;

        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (proximityAudioSource != null && player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            float volume = Mathf.Clamp01((maxDistance - distance) / maxDistance);
            volume = Mathf.Lerp(minVolume, maxVolume, volume);
            proximityAudioSource.volume = volume;

            if (volume > 0 && !proximityAudioSource.isPlaying)
            {
                proximityAudioSource.Play();
            }
            else if (volume == 0 && proximityAudioSource.isPlaying)
            {
                proximityAudioSource.Stop();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isCollected && artefactsCollected < 5)
        {
            isCollected = true;
            artefactsCollected++;

            if (collectSound != null)
                audioSource.PlayOneShot(collectSound);

            OnArtefactCollected?.Invoke(artefactsCollected);
            Destroy(gameObject, collectSound != null ? collectSound.length : 0f);
        }
    }

    public static void ResetArtefactsCollected()
    {
        artefactsCollected = 0;
        OnArtefactCollected?.Invoke(artefactsCollected);
    }
}
