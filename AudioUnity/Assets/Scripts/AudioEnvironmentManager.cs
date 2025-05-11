using UnityEngine;
using UnityEngine.Audio;

public class AudioEnvironmentManager : MonoBehaviour
{
    [Header("Audio Mixer Settings")]
    public AudioMixer mixer;
    public string indoorSnapshot = "Indoor";
    public string outdoorSnapshot = "Outdoor";
    public float transitionTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioMixerSnapshot snapshot = mixer.FindSnapshot(indoorSnapshot);
            if (snapshot != null)
                snapshot.TransitionTo(transitionTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioMixerSnapshot snapshot = mixer.FindSnapshot(outdoorSnapshot);
            if (snapshot != null)
                snapshot.TransitionTo(transitionTime);
        }
    }
} 