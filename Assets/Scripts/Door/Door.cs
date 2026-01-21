using UnityEngine;

public class Door : MonoBehaviour
{
    public int minLimits = -90;
    public int maxLimits = 90;

    public AudioSource audioSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    public HingeJoint hinge;
    public JointLimits limits;

    private bool isOpen = false;
    private bool hasClosed = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hinge = GetComponentInChildren<HingeJoint>();

        if (hinge == null)
        {
            Debug.LogError("HingeJoint missing!");
            enabled = false;
            return;
        }

        limits = hinge.limits;
        limits.min = minLimits;
        limits.max = maxLimits;
        limits.bounciness = 0;
        limits.bounceMinVelocity = 0;
        hinge.limits = limits;
        hinge.useLimits = true;
    }

    void Update()
    {
        float currentAngle = hinge.angle;

        if (currentAngle < -1f || currentAngle > 1f && !isOpen)
        {
            if (hasClosed == true)
                audioSource.PlayOneShot(doorOpen);
            isOpen = true;
            hasClosed = false;
        }

        else if (currentAngle >= -1f && currentAngle <= 1f && isOpen)
        {
            audioSource.PlayOneShot(doorClose);
            isOpen = false;
            hasClosed = true;
        }
    }
}