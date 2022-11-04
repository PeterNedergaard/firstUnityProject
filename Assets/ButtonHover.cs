using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour
{
    private float durationFrameCount = 3;
    private float elapsedFrames;
    private float hoverSize = 1.1f;
    private bool hover;
    
    void Awake()
    {
        
    }
    
    void Update()
    {
        if (hover && (elapsedFrames + 1) % (durationFrameCount + 1) <= durationFrameCount)
        {
            float interpolationRatio = elapsedFrames / durationFrameCount;

            Vector3 interpolatedSize = Vector3.Lerp(Vector3.one, Vector3.one * hoverSize, interpolationRatio);

            elapsedFrames += 1;

            transform.localScale = interpolatedSize;
        }
    }

    
    public void HoverAction()
    {
        hover = true;
    }
    
    public void LeaveAction()
    {
        hover = false;
        elapsedFrames = 0;
        transform.localScale = Vector3.one;
    }
}
