
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Presets;
#endif
using UnityEngine;

public class ASSWwiseSoundBank : MonoBehaviour
{
#if !NO_WWISE    
    Preset akBankPreset;
    AkBank akBank;

    void Awake()
    {
        akBank = gameObject.AddComponent<AkBank>();
        akBankPreset.ApplyTo(akBank);
    }
#endif
}
