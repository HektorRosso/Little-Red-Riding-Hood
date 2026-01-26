using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    public Cutscenes cutscenes;

    public GameObject wolfEndFirstMeetingPoint;

    public GameObject disguisedWolfEndFirstMeetingPoint;

    public GameObject lumberjackStartFirstMeetingPoint;
    public GameObject lumberjackEndFirstMeetingPoint;

    public GameObject lumberjackBorder1;
    public GameObject lumberjackBorder2;

    public GameObject grandmotherMeetingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "WolfStartFirstMeetingPoint")
                cutscenes.Hello();
                wolfEndFirstMeetingPoint.SetActive(true);
                gameObject.SetActive(false);

            if (gameObject.name == "DisguisedWolfStartFirstMeetingPoint")
                cutscenes.Grandma();
                disguisedWolfEndFirstMeetingPoint.SetActive(true);
                gameObject.SetActive(false);

            if (gameObject.name == "LumberjackStartFirstMeetingPoint")
                lumberjackStartFirstMeetingPoint.SetActive(false);
                lumberjackBorder1.SetActive(false);
                lumberjackBorder2.SetActive(false);
                cutscenes.Help();
                gameObject.SetActive(false);
        }
    }
}
