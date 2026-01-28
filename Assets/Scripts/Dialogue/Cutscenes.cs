using System.Collections;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public TriggerCutscene triggerCutscene;
    public AudioSource audioSource;
    private bool firstPickup;

    public GameObject basket;
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

        basket.SetActive(true);

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

    public void Hello()
    {
        StartCoroutine(HelloLittleGirl());
    }

    public IEnumerator HelloLittleGirl()
    {
        dialogueManager.isCutscene = true;

        Animator anim = dialogueManager.wolfAnim;

        anim.SetBool("wolfWalk", true);

        yield return StartCoroutine(WolfWalk());

        yield return StartCoroutine(WolfWalk());

        yield return StartCoroutine(PlayVoiceline(wolf1));

        yield return StartCoroutine(PlayVoiceline(littleRedRidingHood2));

        yield return StartCoroutine(PlayVoiceline(wolf2));

        yield return StartCoroutine(WolfWalk());

        yield return StartCoroutine(WolfWalk());

        anim.SetBool("wolfWalk", false);
        anim.SetBool("wolfPoint", true);

        yield return StartCoroutine(PlayVoiceline(wolf3));

        anim.SetBool("wolfPoint", false);
        anim.SetBool("wolfWalk", true);

        yield return StartCoroutine(WolfWalk());

        yield return StartCoroutine(WolfWalk());

        anim.SetBool("wolfWalk", false);
        anim.SetBool("wolfWave", true);

        yield return StartCoroutine(PlayVoiceline(wolf4));

        anim.SetBool("wolfWave", false);

        yield return null;

        dialogueManager.isCutscene = false;
    }

    public void Grandma()
    {
        StartCoroutine(HelloGrandmother());
    }

    IEnumerator HelloGrandmother()
    {
        dialogueManager.isCutscene = true;

        Animator anim = dialogueManager.disguisedWolfAnim;

        yield return StartCoroutine(PlayVoiceline(littleRedRidingHood3));

        yield return StartCoroutine(PlayVoiceline(wolf5));

        yield return StartCoroutine(PlayVoiceline(littleRedRidingHood4));

        yield return StartCoroutine(PlayVoiceline(wolf6));

        yield return StartCoroutine(PlayVoiceline(littleRedRidingHood5));

        anim.SetBool("wolfStandUp", true);

        yield return StartCoroutine(PlayVoiceline(wolf7));

        anim.SetBool("wolfStandUp", false);
        anim.SetBool("wolfIdle", true);

        yield return StartCoroutine(PlayVoiceline(littleRedRidingHood6));

        dialogueManager.isCutscene = false;
    }

    public void Help()
    {
        StartCoroutine(Coming());
    }

    IEnumerator Coming()
    {
        dialogueManager.isCutscene = true;

        Animator lumberjackanim = dialogueManager.lumberjackAnim;
        Animator wolfanim = dialogueManager.disguisedWolfAnim;

        yield return StartCoroutine(PlayVoiceline(littleRedRidingHood7));

        triggerCutscene.lumberjackStartFirstMeetingPoint.SetActive(false);

        lumberjackanim.SetBool("lumberjackChopping", false);
        lumberjackanim.SetBool("lumberjackWalk", true);

        yield return StartCoroutine(LumberjackWalk());

        lumberjackanim.SetBool("lumberjackWalk", false);
        lumberjackanim.SetBool("lumberjackTurn", true);

        yield return StartCoroutine(PlayVoiceline(lumberjack1));

        lumberjackanim.SetBool("lumberjackTurn", false);
        lumberjackanim.SetBool("lumberjackWalk", true);

        yield return StartCoroutine(LumberjackWalk());

        lumberjackanim.SetBool("lumberjackWalk", false);
        lumberjackanim.SetBool("lumberjackTurn", true);

        yield return StartCoroutine(PlayVoiceline(lumberjack2));

        wig.SetActive(false);

        Dialogue(wolf8);

        wolfanim.SetBool("wolfIdle", false);
        wolfanim.SetBool("wolfRun", true);

        yield return StartCoroutine(WolfRun());

        Running(WolfRun());

        lumberjackanim.SetBool("lumberjackTurn", false);
        lumberjackanim.SetBool("lumberjackRun", true);

        yield return Run(LumberjackRun());

        Running(Run(WolfRun()));

        yield return Run(LumberjackRun());

        yield return StartCoroutine(PlayVoiceline(grandmother1));

        triggerCutscene.disguisedWolfEndFirstMeetingPoint.SetActive(false);

        triggerCutscene.lumberjackEndFirstMeetingPoint.SetActive(true);

        triggerCutscene.lumberjackBorder1.SetActive(true);

        triggerCutscene.lumberjackBorder2.SetActive(true);

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