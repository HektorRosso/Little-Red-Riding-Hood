using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject move;
    public Cutscenes cutscenes;

    [HideInInspector]
    public bool isCutscene;
    [HideInInspector]
    public bool isMoving;

    [HideInInspector]
    public bool isWolfWalking;
    [HideInInspector]
    public bool isWolfRunning;
    [HideInInspector]
    public bool isLumberjackWalking;
    [HideInInspector]
    public bool isLumberjackRunning;
    [HideInInspector]
    public bool isGrandmotherMoving;

    private bool wolfAtWaypoint;
    private bool lumberjackAtWaypoint;
    private bool grandmotherAtWaypoint;

    private int wolfWaypointIndex;
    private int lumberjackWaypointIndex;
    private int grandmotherWaypointIndex;

    [Header("UI")]
    public GameObject mainMenu;
    public GameObject theEnd;
    public GameObject credits;

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

    void Start()
    {
        motherAnim = mother.GetComponentInChildren<Animator>();
        wolfAnim = wolf.GetComponentInChildren<Animator>();
        disguisedWolfAnim = disguisedWolf.GetComponentInChildren<Animator>();
        lumberjackAnim = lumberjack.GetComponentInChildren<Animator>();
        grandmotherAnim = grandmother.GetComponentInChildren<Animator>();

        mainMenu.SetActive(true);
    }

    void Update()
    {
        isMoving = (isWolfWalking || isWolfRunning || isLumberjackWalking || isLumberjackRunning || isGrandmotherMoving);

        if (isWolfWalking)
        {
            isWolfWalking = MoveTowardsWaypoint(wolf, wolfWaypoints, ref wolfWaypointIndex, ref wolfAtWaypoint, ref isWolfWalking, 2);
        }

        if (isWolfRunning)
        {
            isWolfRunning = MoveTowardsWaypoint(disguisedWolf, wolfWaypoints, ref wolfWaypointIndex, ref wolfAtWaypoint, ref isWolfRunning, 4);
        }

        if (isLumberjackWalking)
        {
            isLumberjackWalking = MoveTowardsWaypoint(lumberjack, lumberjackWaypoints, ref lumberjackWaypointIndex, ref lumberjackAtWaypoint, ref isLumberjackWalking, 2);
        }

        if (isLumberjackRunning)
        {
            isLumberjackRunning = MoveTowardsWaypoint(lumberjack, lumberjackWaypoints, ref lumberjackWaypointIndex, ref lumberjackAtWaypoint, ref isLumberjackRunning, 4);
        }

        if (isGrandmotherMoving)
        {
            isGrandmotherMoving = MoveTowardsWaypoint(grandmother, grandmotherWaypoints, ref grandmotherWaypointIndex, ref grandmotherAtWaypoint, ref isGrandmotherMoving, 1);
        }

        wolfAnim.SetBool("wolfRun", isWolfRunning);
        lumberjackAnim.SetBool("lumberjackRun", isLumberjackRunning);

        wolfAnim.SetBool("wolfWalk", isWolfWalking);
        lumberjackAnim.SetBool("lumberjackWalk", isLumberjackWalking);

        grandmotherAnim.SetBool("grandmotherWalk", isGrandmotherMoving);

        if (isCutscene == true || isMoving || mainMenu.activeInHierarchy == true || theEnd.activeInHierarchy == true)
        {
            move.SetActive(false);
        }
        else
        {
            move.SetActive(true);
        }
    }

    public void StartWolfRunning()
    {
        if (wolfWaypointIndex < wolfWaypoints.Count)
        {
            wolfAtWaypoint = false;
            StartMoving(ref isWolfRunning);
        }
    }

    public void StartWolfWalking()
    {
        if (wolfWaypointIndex < wolfWaypoints.Count)
        {
            wolfAtWaypoint = false;
            StartMoving(ref isWolfWalking);
        }
    }

    public void StartLumberjackRunning()
    {
        if (lumberjackWaypointIndex < lumberjackWaypoints.Count)
        {
            lumberjackAtWaypoint = false;
            StartMoving(ref isLumberjackRunning);
        }
    }

    public void StartLumberjackWalking()
    {
        if (lumberjackWaypointIndex < lumberjackWaypoints.Count)
        {
            lumberjackAtWaypoint = false;
            StartMoving(ref isLumberjackWalking);
        }
    }
    public void StartGrandmotherMoving()
    {
        if (grandmotherWaypointIndex < grandmotherWaypoints.Count)
        {
            grandmotherAtWaypoint = false;
            StartMoving(ref isGrandmotherMoving);
        }
    }

    private void StartMoving(ref bool isMovingFlag)
    {
        isMovingFlag = true;
    }

    private bool MoveTowardsWaypoint(Transform character,List<Transform> waypoints,ref int waypointIndex,ref bool atWaypoint,ref bool isMovingFlag,float moveSpeed)
    {
        if (waypoints == null || waypoints.Count == 0 || atWaypoint)
            return false;

        moveSpeed *= Time.deltaTime;

        Vector3 targetPosition = waypoints[waypointIndex].position;
        targetPosition.y = character.position.y;

        character.position = Vector3.MoveTowards(
            character.position,
            targetPosition,
            moveSpeed);

        Vector3 direction = targetPosition - character.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            character.rotation = Quaternion.Lerp(character.rotation,targetRotation,Time.deltaTime * 10f);
        }

        if (Vector3.Distance(character.position, targetPosition) <= 0.05f)
        {
            atWaypoint = true;
            isMovingFlag = false;

            waypointIndex++;
            if (waypointIndex >= waypoints.Count)
                waypointIndex = waypoints.Count - 1;
        }
        return isMovingFlag;
    }
}