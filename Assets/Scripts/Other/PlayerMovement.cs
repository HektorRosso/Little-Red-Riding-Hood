using UnityEngine;
using UnityEngine.InputSystem;

public class VRPlayerAnimation : MonoBehaviour
{
    [Header("Animation & Input")]
    public Animator playerAnim;
    public InputActionProperty moveAction;
    public DialogueManager dialogueManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip walkingClip;

    private bool wasWalking;

    void Start()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();

        if (!playerAnim)
            playerAnim = GetComponent<Animator>();

        if (audioSource != null && walkingClip != null)
        {
            audioSource.clip = walkingClip;
            audioSource.loop = true;
        }
    }

    void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    void Update()
    {
        if (dialogueManager != null &&
            (dialogueManager.isCutscene ||
             dialogueManager.isMoving ||
             dialogueManager.mainMenu.activeInHierarchy ||
             !dialogueManager.basket.activeInHierarchy))
        {
            StopWalking();
            return;
        }

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        bool isWalking = input.magnitude >= 0.1f;

        if (isWalking && !wasWalking)
            StartWalking();
        else if (!isWalking && wasWalking)
            StopWalking();

        wasWalking = isWalking;
    }

    void StartWalking()
    {
        playerAnim.SetBool("walk", true);

        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }

    void StopWalking()
    {
        playerAnim.SetBool("walk", false);

        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}