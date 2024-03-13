using System.Collections;
using System.Collections.Generic;
using Architecture.CodeBase.Services.GlobalData;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioFileStorage", menuName = "Static Data/Audio File Storage")]
public class AudioFileStorage : ItemGlobalData
{
    /*private Dictionary<AudioName, AudioInfo> _audioInfoMap;

    protected override void LoadData() {
        _audioInfoMap = Resources.LoadAll<AudioFile>(AssetAddresses.ResourcesPath.AUDIO_FILES).ToDictionary(audioFile => audioFile.Name, audioFile => new AudioInfo(audioFile));
    }

    public AudioInfo GetAudioInfo(AudioName audioName) {
        return _audioInfoMap[audioName];
    }*/
}
