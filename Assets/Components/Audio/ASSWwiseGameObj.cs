/* Adds Wwise AkGameObj. Part of AudioSystemSwitcher. 
 *
 * Use this instead of AkGameObj.
 * Using regular AkGameObj would cause errors, if Wwise plugin is not installed.
 * 
 * IsRoomAware feature untested.
 * 
 * Supports only default listener.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASSWwiseGameObj : MonoBehaviour
{
#if !NO_WWISE
    [SerializeField] bool IsEnvironmentAware = true;
    [SerializeField] bool ApplyPositionOffset = false;
    [SerializeField] bool IsRoomAware = false;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    void Awake()
    {
        AkGameObj gameObj = gameObject.AddComponent<AkGameObj>();

        if(IsRoomAware)
        {
            gameObject.AddComponent<AkRoomAwareObject>();
        }

        gameObj.isEnvironmentAware = IsEnvironmentAware;

        if(ApplyPositionOffset)
        {
            gameObj.m_positionOffsetData.positionOffset = positionOffset;
        }
        else
        {
            gameObj.m_positionOffsetData = null;
        }

    }

#endif
}
