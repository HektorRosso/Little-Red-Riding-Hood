using System.Collections;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioSource audioSource;
    private bool firstPickup;

    [Header("Mother")]
    public AudioClip mother1;
    public AudioClip mother2;
    public AudioClip mother3;

    [Header("Little Red Riding Hood")]
    public AudioClip littleRedRidingHood1;
    public AudioClip littleRedRidingHood2;
    public AudioClip littleRedRidingHood3;
    public AudioClip littleRedRidingHood4;
    public AudioClip littleRedRidingHood5;
    public AudioClip littleRedRidingHood6;
    public AudioClip littleRedRidingHood7;
    public AudioClip littleRedRidingHood8;

    [Header("Wolf")]
    public AudioClip wolf1;
    public AudioClip wolf2;
    public AudioClip wolf3;
    public AudioClip wolf4;
    public AudioClip wolf5;
    public AudioClip wolf6;
    public AudioClip wolf7;

    [Header("Lumberjack")]
    public AudioClip lumberjack1;
    public AudioClip lumberjack2;

    [Header("Grandmother")]
    public AudioClip grandmother1;
    public AudioClip grandmother2;
    public AudioClip grandmother3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        
    }

    IEnumerator Mother()
    {
        dialogueManager.StartMotherMoving();
        yield return new WaitUntil(() => !dialogueManager.isMotherMoving);
    }

    IEnumerator WolfRun()
    {
        dialogueManager.StartWolfRunning();
        yield return new WaitUntil(() => !dialogueManager.isWolfRunning);
    }

    IEnumerator WolfWalk()
    {
        dialogueManager.StartWolfWalking();
        yield return new WaitUntil(() => !dialogueManager.isWolfWalking);
    }

    IEnumerator LumberjackRun()
    {
        dialogueManager.StartLumberjackRunning();
        yield return new WaitUntil(() => !dialogueManager.isLumberjackRunning);
    }

    IEnumerator LumberjackWalk()
    {
        dialogueManager.StartLumberjackWalking();
        yield return new WaitUntil(() => !dialogueManager.isLumberjackWalking);
    }

    IEnumerator Grandmother()
    {
        dialogueManager.StartGrandmotherMoving();
        yield return new WaitUntil(() => !dialogueManager.isGrandmotherMoving);
    }

    IEnumerator PlayVoiceline(AudioClip clip)
    {
        dialogueManager.isCutscene = true;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitUntil(() => !audioSource.isPlaying);
        dialogueManager.isCutscene = false;
    }

    private void Dialogue(AudioClip clip)
    {
        StartCoroutine(PlayVoiceline(clip));
    }

    public void BasketInteraction()
    {
        Dialogue(mother3);
        StartCoroutine(LumberjackWalk());
    }

    public void FirstPickup()
    {
        if (firstPickup == false)
        {
            audioSource.PlayOneShot(littleRedRidingHood1);
            firstPickup = true;
        }
        else
        {
            return;
        }
    }
}