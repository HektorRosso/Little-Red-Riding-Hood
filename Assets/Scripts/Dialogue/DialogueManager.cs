using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject move;
    public Cutscenes cutscenes;
    public GameObject basket;
    public GameObject mainMenu;

    [HideInInspector] public bool isCutscene;
    [HideInInspector] public bool isMoving;

    [HideInInspector] public bool isWolfWalking;
    [HideInInspector] public bool isWolfRunning;
    [HideInInspector] public bool isLumberjackWalking;
    [HideInInspector] public bool isLumberjackRunning;
    [HideInInspector] public bool isGrandmotherMoving;

    private bool wolfAtWaypoint;
    private bool lumberjackAtWaypoint;
    private bool grandmotherAtWaypoint;

    private int wolfWaypointIndex;
    private int lumberjackWaypointIndex;
    private int grandmotherWaypointIndex;

    [Header("Mother")]
    public Transform mother;
    public Animator motherAnim;

    [Header("Wolf")]
    public Transform wolf;
    public Transform disguisedWolf;
    public Animator wolfAnim;
    public Animator disguisedWolfAnim;
    public List<Transform> wolfWaypoints = new List<Transform>();

    [Header("Lumberjack")]
    public Transform lumberjack;
    public Animator lumberjackAnim;
    public List<Transform> lumberjackWaypoints = new List<Transform>();

    [Header("Grandmother")]
    public Transform grandmother;
    public Animator grandmotherAnim;
    public List<Transform> grandmotherWaypoints = new List<Transform>();

    [Header("Audio")]
    public AudioSource wolfSource;
    public AudioSource disguisedWolfSource;
    public AudioSource lumberjackSource;
    public AudioSource grandmotherSource;

    public AudioClip insideWalking;
    public AudioClip outsideWalking;
    public AudioClip running;

    void Start()
    {
        motherAnim = mother.GetComponentInChildren<Animator>();
        wolfAnim = wolf.GetComponentInChildren<Animator>();
        disguisedWolfAnim = disguisedWolf.GetComponentInChildren<Animator>();
        lumberjackAnim = lumberjack.GetComponentInChildren<Animator>();
        grandmotherAnim = grandmother.GetComponentInChildren<Animator>();

        wolfSource = wolf.GetComponentInChildren<AudioSource>();
        disguisedWolfSource = disguisedWolf.GetComponentInChildren<AudioSource>();
        lumberjackSource = lumberjack.GetComponentInChildren<AudioSource>();
        grandmotherSource = grandmother.GetComponentInChildren<AudioSource>();

        mainMenu.SetActive(true);
    }

    void Update()
    {
        isMoving = (isWolfWalking || isWolfRunning || isLumberjackWalking || isLumberjackRunning || isGrandmotherMoving);

        if (isWolfWalking)
            isWolfWalking = MoveTowardsWaypoint(wolf, wolfWaypoints, ref wolfWaypointIndex, ref wolfAtWaypoint, ref isWolfWalking, 2);

        if (isWolfRunning)
            isWolfRunning = MoveTowardsWaypoint(disguisedWolf, wolfWaypoints, ref wolfWaypointIndex, ref wolfAtWaypoint, ref isWolfRunning, 10);

        if (isLumberjackWalking)
            isLumberjackWalking = MoveTowardsWaypoint(lumberjack, lumberjackWaypoints, ref lumberjackWaypointIndex, ref lumberjackAtWaypoint, ref isLumberjackWalking, 3);

        if (isLumberjackRunning)
            isLumberjackRunning = MoveTowardsWaypoint(lumberjack, lumberjackWaypoints, ref lumberjackWaypointIndex, ref lumberjackAtWaypoint, ref isLumberjackRunning, 5);

        if (isGrandmotherMoving)
            isGrandmotherMoving = MoveTowardsWaypoint(grandmother, grandmotherWaypoints, ref grandmotherWaypointIndex, ref grandmotherAtWaypoint, ref isGrandmotherMoving, 1);

        wolfAnim.SetBool("wolfRun", isWolfRunning);
        wolfAnim.SetBool("wolfWalk", isWolfWalking);
        lumberjackAnim.SetBool("lumberjackRun", isLumberjackRunning);
        lumberjackAnim.SetBool("lumberjackWalk", isLumberjackWalking);
        grandmotherAnim.SetBool("grandmotherWalk", isGrandmotherMoving);

        move.SetActive(!(isCutscene || isMoving || mainMenu.activeInHierarchy || !basket.activeInHierarchy));
    }

    private void PlayMovementAudio(AudioSource source, AudioClip clip)
    {
        if (source == null || clip == null) return;

        if (source.clip != clip)
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
    }

    private void StopMovementAudio(AudioSource source)
    {
        if (source != null && source.isPlaying)
            source.Stop();
    }

    public void StartWolfWalking()
    {
        if (wolfWaypointIndex < wolfWaypoints.Count)
        {
            wolfAtWaypoint = false;
            isWolfWalking = true;
            PlayMovementAudio(wolfSource, outsideWalking);
        }
    }

    public void StartWolfRunning()
    {
        if (wolfWaypointIndex < wolfWaypoints.Count)
        {
            wolfAtWaypoint = false;
            isWolfRunning = true;
            PlayMovementAudio(disguisedWolfSource, running);
        }
    }

    public void StartLumberjackWalking()
    {
        if (lumberjackWaypointIndex < lumberjackWaypoints.Count)
        {
            lumberjackAtWaypoint = false;
            isLumberjackWalking = true;
            PlayMovementAudio(lumberjackSource, outsideWalking);
        }
    }

    public void StartLumberjackRunning()
    {
        if (lumberjackWaypointIndex < lumberjackWaypoints.Count)
        {
            lumberjackAtWaypoint = false;
            isLumberjackRunning = true;
            PlayMovementAudio(lumberjackSource, running);
        }
    }

    public void StartGrandmotherMoving()
    {
        if (grandmotherWaypointIndex < grandmotherWaypoints.Count)
        {
            grandmotherAtWaypoint = false;
            isGrandmotherMoving = true;
            PlayMovementAudio(grandmotherSource, insideWalking);
        }
    }

    private bool MoveTowardsWaypoint(
        Transform character,
        List<Transform> waypoints,
        ref int waypointIndex,
        ref bool atWaypoint,
        ref bool isMovingFlag,
        float moveSpeed)
    {
        if (waypoints == null || waypoints.Count == 0 || atWaypoint)
            return false;

        Vector3 targetPosition = waypoints[waypointIndex].position;
        targetPosition.y = character.position.y;

        character.position = Vector3.MoveTowards(
            character.position,
            targetPosition,
            moveSpeed * Time.deltaTime);

        Vector3 direction = targetPosition - character.position;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            character.rotation = Quaternion.Lerp(character.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (Vector3.Distance(character.position, targetPosition) <= 0.05f)
        {
            atWaypoint = true;
            isMovingFlag = false;

            if (character == wolf) StopMovementAudio(wolfSource);
            else if (character == disguisedWolf) StopMovementAudio(disguisedWolfSource);
            else if (character == lumberjack) StopMovementAudio(lumberjackSource);
            else if (character == grandmother) StopMovementAudio(grandmotherSource);

            waypointIndex++;
            if (waypointIndex >= waypoints.Count)
                waypointIndex = waypoints.Count - 1;
        }

        return isMovingFlag;
    }
}