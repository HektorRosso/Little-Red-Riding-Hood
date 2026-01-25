using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    public Cutscenes cutscenes;

    public GameObject wolfEndFirstMeetingPoint;
    public GameObject disguisedWolfEndFirstMeetingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "WolfStartFirstMeetingPoint")
                cutscenes.Hello();
                wolfEndFirstMeetingPoint.SetActive(true);

            if (gameObject.name == "DisguisedWolfStartFirstMeetingPoint")
                cutscenes.Grandma();
                disguisedWolfEndFirstMeetingPoint.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
