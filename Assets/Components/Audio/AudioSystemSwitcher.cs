/* Audio system switcher between Wwise and Unity audio.
 * By default configured to enable Unity audio and disable Wwise when WebGL is used and vice versa.
 *
 * Usage:
 * Place one instance into a scene. After than audio system switches between Wwise and Unity automatically
 * based on active platform in Unity.
 *  
 * Disabling StartUpCheckDone boolean in inspector will force new init, in case something goes wrong.
 * 
 * In addition to this component you need to prevent Wwise from compiling Wwise assembly definitions.
 * Easiest way is to search Project explorer with "t:Asmdef Wwise" and exclude WebGL in all platforms.
 * (Note that some are only compiled for editor already)
 * */

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public  class AudioSystemSwitcher : MonoBehaviour
{
#if !NO_WWISE
    [SerializeField] private AkInitializer akInitializer;
#endif
    [SerializeField] private bool usingUnityAudioSystem = false;
    [SerializeField] private bool startUpCheckDone = false;

    void Update()
    {
        // Assign akIinitializer and select proper audio system on "fake init"
        if(!startUpCheckDone)
        {
#if !NO_WWISE
            if (akInitializer == null)
            {
                akInitializer = GameObject.FindObjectOfType<AkInitializer>();
            }
#endif            
            SelectAudioSystem();
            startUpCheckDone = true;
        }

        // Enable Unity native audio system, if we are on WebGL. Else disable it.
#if UNITY_WEBGL
        if (!usingUnityAudioSystem)
        {
            SelectAudioSystem();
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


    private void SelectAudioSystem()
    {
        // Enables Unity native audio
        const string AudioSettingsAssetPath = "ProjectSettings/AudioManager.asset";
        SerializedObject audioManager = new SerializedObject(UnityEditor.AssetDatabase.LoadAllAssetsAtPath(AudioSettingsAssetPath)[0]);
        SerializedProperty m_DisableAudio = audioManager.FindProperty("m_DisableAudio");
        m_DisableAudio.boolValue = false;
        audioManager.ApplyModifiedProperties();
#if !NO_WWISE
        if (akInitializer!=null) akInitializer.enabled = false;
#endif
        usingUnityAudioSystem = true;

    }
#else

    
    private void SelectAudioSystem()
    {
        // Disables Unity native audio
        const string AudioSettingsAssetPath = "ProjectSettings/AudioManager.asset";
        SerializedObject audioManager = new SerializedObject(UnityEditor.AssetDatabase.LoadAllAssetsAtPath(AudioSettingsAssetPath)[0]);
        SerializedProperty m_DisableAudio = audioManager.FindProperty("m_DisableAudio");
        m_DisableAudio.boolValue = true;
        audioManager.ApplyModifiedProperties();
        
        akInitializer.enabled = true;
        usingUnityAudioSystem = false;
    }
    
#endif
}

#endif