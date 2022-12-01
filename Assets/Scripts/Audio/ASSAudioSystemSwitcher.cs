/* Audio system switcher between Wwise and Unity audio.
 * By default configured to enable Unity audio and disable Wwise when WebGL is used and vice versa.
 *
 * Features:
 * 
 * Makes it possible to switch between native Unity audio and Wwise in editor.
 * Handles Wwise component creation and deletion so that project can also be used and compiled without
 * Wwise plugin.
 * 
 * Related components:
 * ASSWwiseGameObj - Handles adding and removing AkGameObj, when Wwise is used. Use instead of AkGameObj
 * ASSWwiseListener - Handles adding and removing AkAudioListener. Use instead of AkAudioListener
 * ASSWwiseSoundBank - Handles loading soundbank.
 * SimpleAudioWrapper - Wrapper for audio events. Plays either Wwise events or Unity Audio events depending
 *  of platform
 * SimpleAudioWrapperSoundBank - SoundBank for SimpleAudioWrapper.
 * 
 * Usage:
 * 
 * Add NO_WWISE preprocessor definition for non-Wwise platforms.
 * (Project Settings - Player - Script Compilation - Scripting Define Symbols
 *
 * Place one instance into a scene. After that audio system switches between Wwise and Unity automatically
 * based on active platform in Unity.
 *  
 * Create preset for Wwise soundbank (AkBank Component)
 * 
 * Add these to Wwise Sound Bank array in inspector.
 * 
 * Non-scenespecific soundbanks are not supported at the moment.
 * 
 * Remove AkInitializer from scene. One will be created, when game mode starts.
 * 
 * Replace AkGameObj components with ASSWiseGameObj components.
 * 
 * Replace AkAudioListener components with ASSWwiseListener components.
 * 
 * Remove all Wwise prefabs from scene. Wwise has likely created some for you.
 * 
 * In Project Settings - Wwise Editor:
 *  Disable "Create WWiseGlobal GameObject"
 *  Disable "Add Listener to Main Camera"
 *  
 * Disabling StartUpCheckDone boolean in inspector will force new init, in case something goes wrong.
 * 
 * In addition to this component you need to prevent Wwise from compiling Wwise assembly definitions.
 * Easiest way is to search Project explorer with "t:Asmdef Wwise" and exclude WebGL in all platforms.
 * (Note that some are only compiled for editor already)
 * */


using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Presets;
#endif
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public  class ASSAudioSystemSwitcher : MonoBehaviour
{
    
    [SerializeField] private bool usingUnityAudioSystem = false;
    [SerializeField] private bool startUpCheckDone = false;

#if !NO_WWISE
    [SerializeField] private Preset akBankPreset;
    AkBank akBank;
    void Awake()
    {
        // Create soundbanks and initializer when game is playing. Destroy them when we have stopped.
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
#else
        if(Application.isPlaying)
        {
#endif
            this.AddComponent<AkInitializer>();
            akBank = this.AddComponent<AkBank>();
            akBankPreset.ApplyTo(akBank);
      
        }
        else
        {
            DestroyImmediate(gameObject.GetComponent<AkInitializer>());
            akBank = null;
            DestroyImmediate(gameObject.GetComponent<AkBank>());            
        }
    }

#endif

#if UNITY_EDITOR

    void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            // Assign akIinitializer and select proper audio system on "fake init"
            if (!startUpCheckDone)
            {
                SelectAudioSystem();
                startUpCheckDone = true;
            }

            // Enable Unity native audio system, if we are on WebGL. Else disable it.
#if NO_WWISE
            if (!usingUnityAudioSystem)
            {
                SelectAudioSystem();
            }
#else
            if (usingUnityAudioSystem)
            {
                SelectAudioSystem();
                usingUnityAudioSystem = false;
            }
#endif
        }
    }

#if NO_WWISE

    private void SelectAudioSystem()
    {
        // Enables Unity native audio
        const string AudioSettingsAssetPath = "ProjectSettings/AudioManager.asset";
        SerializedObject audioManager = new SerializedObject(UnityEditor.AssetDatabase.LoadAllAssetsAtPath(AudioSettingsAssetPath)[0]);
        SerializedProperty m_DisableAudio = audioManager.FindProperty("m_DisableAudio");
        m_DisableAudio.boolValue = false;
        audioManager.ApplyModifiedProperties();

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

        usingUnityAudioSystem = false;
    }

#endif
#endif
}


