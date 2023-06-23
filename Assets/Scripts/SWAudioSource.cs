using UnityEngine;
using SWNetwork;

[RequireComponent(typeof(AudioSource))]
public class SWAudioSource : MonoBehaviour {

    AudioSource _audioSource;

    [SerializeField] NetworkID networkId;
    [SerializeField] SyncPropertyAgent syncPropertyAgent;
    [SerializeField] string stateBoolKey = "AudioSource";

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play() {
        syncPropertyAgent.Modify(stateBoolKey, true);
    }

    public void Stop() {
        syncPropertyAgent.Modify(stateBoolKey, false);
    }

    public void OnPlaySynced() {
        if (_audioSource == null)
            return;

        bool state = syncPropertyAgent.GetPropertyWithName(stateBoolKey).GetBoolValue();

        if (state)
            _audioSource.Play();
        else
            _audioSource.Stop();
    }
}