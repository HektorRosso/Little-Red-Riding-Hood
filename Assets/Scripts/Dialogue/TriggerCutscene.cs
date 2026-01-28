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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (gameObject.name == "WolfStartFirstMeetingPoint")
        {
            cutscenes.Hello();
            wolfEndFirstMeetingPoint.SetActive(true);
        }
        else if (gameObject.name == "DisguisedWolfStartFirstMeetingPoint")
        {
            cutscenes.Grandma();
            disguisedWolfEndFirstMeetingPoint.SetActive(true);
            lumberjackStartFirstMeetingPoint.SetActive(true);
            lumberjackBorder1.SetActive(true);
            lumberjackBorder2.SetActive(true);
        }
        else if (gameObject.name == "LumberjackStartFirstMeetingPoint")
        {
            lumberjackBorder1.SetActive(false);
            lumberjackBorder2.SetActive(false);
            cutscenes.Help();
        }

        gameObject.SetActive(false);
    }
}
