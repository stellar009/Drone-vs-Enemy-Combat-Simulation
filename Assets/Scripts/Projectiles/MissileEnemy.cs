using UnityEngine;

public class MissileEnemy : MonoBehaviour
{
    public float velocity = 2f;

    [Header("Audio Settings")]
    public AudioClip launchAudio;
    public AudioClip explosionAudio;

    private AudioSource audioSrc;
    private ParticleSystem explosion;

    private void Start()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        audioSrc = GetComponentInChildren<AudioSource>();

        audioSrc.PlayOneShot(launchAudio);
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(collision.collider.gameObject);
        }
    }

    void Explode()
    {
        if (explosion != null)
        {
            audioSrc.PlayOneShot(explosionAudio);
            explosion.transform.parent = null;
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }

        Destroy(gameObject);
    }
}
