using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TheEnd : MonoBehaviour
{
    private float scrollSpeed = 0.1f;

    private bool hasWaited;
    private bool hasFadedIn;
    private bool hasFadedOut;

    public CanvasGroup canvasGroup;
    public Transform player;

    public AudioSource audioSource;
    public AudioClip music;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hasFadedIn = true;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (hasWaited)
            Scroll();
    }

    IEnumerator FadeCanvasGroup(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    IEnumerator Wait(float duration)
    {
        hasWaited = false;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        hasWaited = true;
    }

    void Scroll()
    {
        if (transform.position.y < player.transform.position.y + 5.5f)
        {
            transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
        }
        else if (!hasFadedOut)
        {
            hasFadedOut = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        audioSource.PlayOneShot(music);
        yield return StartCoroutine(FadeCanvasGroup(1f, 1f));
        yield return StartCoroutine(Wait(2f));
    }

    IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeCanvasGroup(0f, 5f));
        SceneManager.LoadScene("SampleScene");
    }
}