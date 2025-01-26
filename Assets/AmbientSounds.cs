using System.Collections.Generic;
using FMODUnity;
using NUnit.Framework;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    public List<EventReference> AmbientSoundEventRefs = new List<EventReference>();

    public float MaxAmbientTime = 20f;
    public float MinAmbientTime = 5f;

    private float randomTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomTimer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        randomTimer = randomTimer - Time.deltaTime;

        if (randomTimer < 0)
        {
            int randomIndex = Random.Range(0, AmbientSoundEventRefs.Count);
            AudioManager.Instance.PlayOneShot(AmbientSoundEventRefs[randomIndex]);
            StartTimer();
        }
    }

    private void StartTimer()
    {
        randomTimer = Random.Range(MinAmbientTime, MaxAmbientTime);
    }
}
