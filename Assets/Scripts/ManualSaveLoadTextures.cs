using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;


public class ManualSaveLoadTextures : MonoBehaviour
{
    [SerializeField] P3dPaintableTexture mainTexture;
    [SerializeField] P3dPaintableTexture normalTexture;

    [SerializeField] string mainTextureSaveName = "MainSave";
    [SerializeField] string normalTextureSaveName = "NoramlSave";

    void Update()
    {
        /*
         * FOR DEBUGING IN EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveTextureState();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadTextureState();
        }
        */
    }

    /// <summary>
    /// Calls P3dPaintableTexture Save method.
    /// </summary>
    public void SaveTextureState()
    {
        mainTexture.Save(mainTextureSaveName);
        normalTexture.Save(normalTextureSaveName);
    }

    /// <summary>
    /// Calls P3dPaintableTexture Load methos.
    /// </summary>
    public void LoadTextureState()
    {
        mainTexture.Load(mainTextureSaveName);
        normalTexture.Load(normalTextureSaveName);
    }
}
