using UnityEngine;

public class CloseOpen : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpen;

    public void DoorClosed()
    {
        doorClosed.SetActive(true);
        doorOpen.SetActive(false);
    }

    public void DoorOpen()
    {
        doorClosed.SetActive(false);
        doorOpen.SetActive(true);
    }
}
