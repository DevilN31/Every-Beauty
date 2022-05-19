using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

[RequireComponent(typeof(P3dHitScreen))]
public class ToolImageController : MonoBehaviour
{
    [SerializeField] Transform toolImage;

    [Tooltip("Add an offset for Image object")]
    [SerializeField] Vector3 positionOffset = Vector3.zero;

    void Start()
    {
        toolImage.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = default(RaycastHit);

        
        if(Physics.Raycast(ray,out hit)) // If tool hit something => show tool image
        {
            toolImage.position = hit.point + positionOffset;
            toolImage.gameObject.SetActive(true);
        }
        else
        {
            if(toolImage.gameObject.activeSelf)
                toolImage.gameObject.SetActive(false);
        }
    }
}
