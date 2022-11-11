/* Adds Wwise listener component. Part of AudioSystemSwitcher. 
 *
 * Use this instead of AkAudioListener.
 */


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ASSWwiseListener : MonoBehaviour
{
#if !NO_WWISE
    [SerializeField] bool IsDefaultListener = true;
    void Awake()
    {
        AkAudioListener listener = gameObject.AddComponent<AkAudioListener>();
        listener.isDefaultListener = IsDefaultListener;
    }
#endif
}

