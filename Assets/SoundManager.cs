using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public List<SoundContainer> soundContainers;
    private void Awake() {
        if(instance == null)
            instance = this;
        else Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddSoundContainers(List<SoundContainer> _soundContainers){
        soundContainers = _soundContainers;
    }

    private void OnEnable() {
        ArticyManager.OnDialogEnded += OnDialogEnded;
    }
    private void OnDisable() {
        ArticyManager.OnDialogEnded -= OnDialogEnded;
    }
    // Update is called once per frame
    void OnDialogEnded()
    {
        foreach (SoundContainer soundContainer in soundContainers)
        {
            if(GetGlobalVariable.Int(soundContainer.emotionMeterString) > 0) return;
        }
    }
}
