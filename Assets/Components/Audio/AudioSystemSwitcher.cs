#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public  class AudioSystemSwitcher : MonoBehaviour
{

    [SerializeField] private bool usingUnityAudioSystem = false;
    [SerializeField] private AkInitializer akInitializer;
    [SerializeField] private AkBank akBank;
    void Awake()
    {

    }

    void Update()
    {
        // Enable Unity native audio system, if we are on WebGL. Else disable it.
#if UNITY_WEBGL
        if (!usingUnityAudioSystem)
        {
            SelectAudioSystem();
            usingUnityAudioSystem = true;
        }
#else
        if(usingUnityAudioSystem)
                {
                    SelectAudioSystem();
                    usingUnityAudioSystem = false;
                }
#endif
    }
#if UNITY_WEBGL

    // Enables Unity native audio
    private void SelectAudioSystem()
    {
        Debug.Log("lol");
        const string AudioSettingsAssetPath = "ProjectSettings/AudioManager.asset";
        SerializedObject audioManager = new SerializedObject(UnityEditor.AssetDatabase.LoadAllAssetsAtPath(AudioSettingsAssetPath)[0]);
        SerializedProperty m_DisableAudio = audioManager.FindProperty("m_DisableAudio");
        m_DisableAudio.boolValue = false;
        audioManager.ApplyModifiedProperties();

        akInitializer.enabled = false;
        akBank.enabled = false;

    }
#else

    // Disables Unity native audio
    private void SelectAudioSystem()
    {
        const string AudioSettingsAssetPath = "ProjectSettings/AudioManager.asset";
        SerializedObject audioManager = new SerializedObject(UnityEditor.AssetDatabase.LoadAllAssetsAtPath(AudioSettingsAssetPath)[0]);
        SerializedProperty m_DisableAudio = audioManager.FindProperty("m_DisableAudio");
        m_DisableAudio.boolValue = true;
        audioManager.ApplyModifiedProperties();
        
        akInitializer.enabled = true;
        akBank.enabled = true;
    }
    
#endif
}

#endif