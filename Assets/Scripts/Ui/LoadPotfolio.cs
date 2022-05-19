using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPotfolio : MonoBehaviour
{
    [SerializeField] Image beforeImage;
    [SerializeField] Image afterImage;

    // Update portfolio pictures.
    private void OnEnable()
    {
        beforeImage.sprite = NatiTsim.ScreenshotManager.instance.LoadimageToUi("Before");
        afterImage.sprite = NatiTsim.ScreenshotManager.instance.LoadimageToUi("After");
    }
}
