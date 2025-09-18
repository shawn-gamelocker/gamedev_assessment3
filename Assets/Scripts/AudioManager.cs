using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource introSource;
    public AudioSource ghostNormalSource;

    void Start()
    {
        PlayIntro();
    }

    void PlayIntro()
    {
        introSource.Play();
        // Start coroutine to switch to ghost normal after 3 seconds or when intro finishes
        StartCoroutine(SwitchToGhostNormal());
    }

    System.Collections.IEnumerator SwitchToGhostNormal()
    {
        float waitTime = Mathf.Min(introSource.clip.length, 3f);
        yield return new WaitForSeconds(waitTime);

        introSource.Stop();
        ghostNormalSource.Play();
    }
}