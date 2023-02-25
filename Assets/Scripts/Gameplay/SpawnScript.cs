using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class SpawnScript : MonoBehaviour
{
    ///////////////////////START FUNCTION//////////////////////////
    public TeleportationAnchor anchor = null;
    public TeleportationProvider provider = null;

    private void Awake()
    {
        if (anchor && provider)
        {
            TeleportRequest request = CreateRequest();
            provider.QueueTeleportRequest(request);
        }
    }

    private TeleportRequest CreateRequest()
    {
        Transform anchorTransform = anchor.teleportAnchorTransform;

        TeleportRequest request = new TeleportRequest()
        {
            requestTime = Time.time,
            matchOrientation = anchor.matchOrientation,

            destinationPosition = anchorTransform.position,
            destinationRotation = anchorTransform.rotation
        };

        return request;
    }
}
