
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
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
