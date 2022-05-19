using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class MaskController : MonoBehaviour
{
    public static MaskController instance;

    public enum MaskType
    {
        None,
        Eyebrows,
        Nose,
        Face
    }

    [Header("P3D Paintable Textures")]
    [SerializeField] P3dPaintableTexture albedoTexture;
    [SerializeField] P3dPaintableTexture normalTexture;

    [Header("Local Masks")]
    [SerializeField] Texture2D eyebrowsMask;
    [SerializeField] Texture2D noseMask;
    [SerializeField] Texture2D faceMask;
    [SerializeField] MaskType maskType;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// Updates current loaded LocalMask in the P3dPaintableTexture components.
    /// </summary>
    /// <param name="newMaskType">
    /// Sets mask type.
    /// </param>
    public void UpdateMaskType(MaskType newMaskType)
    {
        maskType = newMaskType;

        switch(maskType)
        {
            case MaskType.None:
                {
                    SetLocalMask(null);
                    break;
                }
            case MaskType.Eyebrows:
                {
                    SetLocalMask(eyebrowsMask);


                    break;
                }
            case MaskType.Nose:
                {
                    SetLocalMask(noseMask);


                    break;
                }
            case MaskType.Face:
                {
                    SetLocalMask(faceMask);


                    break;
                }
                
        }

        Debug.Log($"<Mask Controller> mask changed to {maskType}");
    }

    void SetLocalMask(Texture2D _localMaskTexture)
    {
        albedoTexture.LocalMaskTexture = _localMaskTexture;
        normalTexture.LocalMaskTexture = _localMaskTexture;
    }
}
