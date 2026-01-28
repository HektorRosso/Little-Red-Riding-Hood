using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pickup;
    public AudioClip putdown;
    public AudioClip littleRedRidingHood1;

    private static bool firstPickup;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        firstPickup = false;
    }

    public void Pickup()
    {
        audioSource.PlayOneShot(pickup);
    }

    public void FirstPickup()
    {
        if (!firstPickup && littleRedRidingHood1 != null)
        {
            audioSource.PlayOneShot(littleRedRidingHood1);
            firstPickup = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            audioSource.PlayOneShot(putdown);
        }
    }
}
