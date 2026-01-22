using System.Collections;
using UnityEditor;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioSource audioSource;
    private bool firstPickup;
    public GameObject wig;

    [Header("Mother")]
    public AudioClip mother1;
    public AudioClip mother2;
    public AudioClip mother3;
    public AudioClip mother4;

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
    public AudioClip wolf8;

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
        StartCoroutine(Morning());
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

    IEnumerator Run(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
    }

    IEnumerator PlayVoiceline(AudioClip clip)
    {
        dialogueManager.isCutscene = true;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitUntil(() => !audioSource.isPlaying);
        dialogueManager.isCutscene = false;
    }

    private void Running(IEnumerator coroutine)
    {
        StartCoroutine(Run(coroutine));
    }

    private void Dialogue(AudioClip clip)
    {
        StartCoroutine(PlayVoiceline(clip));
    }

    IEnumerator Morning()
    {
        dialogueManager.isCutscene = true;

        Animator anim = dialogueManager.motherAnim;

        yield return StartCoroutine(PlayVoiceline(mother1));

        anim.SetBool("motherPoint", true);

        yield return StartCoroutine(PlayVoiceline(mother2));

        anim.SetBool("motherPoint", false);

        yield return null;
    }

    public void BasketInteraction()
    {
        StartCoroutine(Goodbye());
    }

    IEnumerator Goodbye()
    {
        Animator anim = dialogueManager.motherAnim;

        yield return StartCoroutine(PlayVoiceline(mother3));

        anim.SetBool("motherWave", true);

        yield return StartCoroutine(PlayVoiceline(mother4));

        anim.SetBool("motherWave", false);

        yield return null;

        dialogueManager.isCutscene = false;
    }

    public void Help()
    {
        Dialogue(littleRedRidingHood7);
        StartCoroutine(Coming());
    }

    IEnumerator Coming()
    {
        dialogueManager.isCutscene = true;

        Animator anim = dialogueManager.lumberjackAnim;

        anim.SetBool("lumberjackChopping", false);
        anim.SetBool("lumberjackWalk", true);

        yield return StartCoroutine(LumberjackWalk());

        anim.SetBool("lumberjackWalk", false);
        anim.SetBool("lumberjackTurn", true);

        yield return StartCoroutine(PlayVoiceline(lumberjack1));

        anim.SetBool("lumberjackTurn", false);
        anim.SetBool("lumberjackWalk", true);

        yield return StartCoroutine(LumberjackWalk());

        dialogueManager.isCutscene = false;
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