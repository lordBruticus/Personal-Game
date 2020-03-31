using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class OcculusGazeInputModule : PointerInputModule
{
    //1
    public GameObject reticle;
    //2
    public Transform centerEyeTransform;
    //3
    public float reticleSizeMultiplier = 0.02f; // The size of the reticle will get scaled with this value
                                                //4
    private PointerEventData pointerEventData;
    //5
    private RaycastResult currentRaycast;
    //6
    private GameObject currentLookAtHandler;


    public override void Process()
    {
        HandleLook();
        HandleSelection();
    }

    void HandleLook()
    {
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }

        pointerEventData.position = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f)); // Set a virtual pointer to the center of the screen
        List<RaycastResult> raycastResults = new List<RaycastResult>(); // A list to hold all the raycast results
        eventSystem.RaycastAll(pointerEventData, raycastResults); // Do a raycast using all enabled raycasters in the scene
        currentRaycast = pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults); // Get the first hit an set both the local and pointerEventData results

        reticle.transform.position = centerEyeTransform.position + (centerEyeTransform.forward * currentRaycast.distance); // Move reticle
        float reticleSize = currentRaycast.distance * reticleSizeMultiplier;
        reticle.transform.localScale = new Vector3(reticleSize, reticleSize, reticleSize); //Scale reticle so it's always the same size

        ProcessMove(pointerEventData); // Pass the pointer data to the event system so entering and exiting of objects is detected
    }

    void HandleSelection()
    {
        if (pointerEventData.pointerEnter != null)
        {
            //Get the OnPointerClick handler of the entered object
            currentLookAtHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerEventData.pointerEnter);

            if (currentLookAtHandler != null && OVRInput.GetDown(OVRInput.Button.One))
            {
                // Object in sight with a OnPointerClick handler & pressed the main button
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            currentLookAtHandler = null;
        }
    }

}
