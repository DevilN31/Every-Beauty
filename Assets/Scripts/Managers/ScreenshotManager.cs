using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace NatiTsim
{
    public class ScreenshotManager : MonoBehaviour
    {
        public static ScreenshotManager instance;

        [SerializeField]
        [Tooltip("Name of the folder where the scrennshots will be saved.")]
        string folderName = "Screenshots";

        [SerializeField]
        [Tooltip("If higher then 1, multiplies resolution by given number.\nNOTE: this will make the proccess longer.")]
        int superSize = 1;

        [SerializeField]
        [Tooltip("The texture you get if file not found")]
        Texture2D defaultTexture;

        [SerializeField]
        [Tooltip("Paths to all screenshots")]
        string[] images = null;

        [SerializeField]
        [Tooltip("Ui Elements To Hide while photo is taken")]
        List<GameObject> uiElementsToHide;

        [SerializeField]
        Image backgroundFrame;

        // Folder reference
        DirectoryInfo screenshotsFolder;

        string fileName = String.Empty;
        string defaultFileName;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            defaultFileName = $"/{Application.productName} {DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.png";
            backgroundFrame.gameObject.SetActive(false);

            if (!Directory.Exists(Application.persistentDataPath + CheckFolderName()))
                screenshotsFolder = CreateFolder(CheckFolderName());
            else
                screenshotsFolder = new DirectoryInfo(Application.persistentDataPath + CheckFolderName());

            GetAllImagesFromScreenshotFolder();
        }

        /// <summary>
        /// Takes screenshot of current screen.
        /// NOTE: Ui Elements to hide list should be set BEFORE.
        /// </summary>
        /// <param name="_fileName">
        /// Sets Image file name.
        /// </param>
        /// <returns></returns>
        private IEnumerator TakeScreenshot(string _fileName)
        {

            foreach (GameObject element in uiElementsToHide)
            {
                element.SetActive(false);
            }

            backgroundFrame.color = Color.white;

            backgroundFrame.gameObject.SetActive(true);

            yield return new WaitForEndOfFrame();

            if (_fileName == String.Empty) // If fileName is empty => set to a default file name
            {
                fileName = defaultFileName;
            }
            else // Else => use _levelName
            {
                fileName = $"/{_fileName}.png";
            }

            if (Application.isMobilePlatform)
                ScreenCapture.CaptureScreenshot(CheckFolderName() + fileName, superSize); // On mobile Application.persistentDataPath is added automatically. 
            else
                ScreenCapture.CaptureScreenshot(screenshotsFolder.FullName + fileName, superSize);

            Debug.Log($"<ScreenshotManager> Created a screenshot at {screenshotsFolder.FullName + fileName}");

            yield return new WaitForEndOfFrame();

            backgroundFrame.gameObject.SetActive(false);

            foreach (GameObject element in uiElementsToHide)
            {
                element.SetActive(true);
            }

            GetAllImagesFromScreenshotFolder();
        }

        /// <summary>
        /// Starts TakeScreenshot coroutine
        /// </summary>
        /// <param name="_fileName">
        /// Sets name for Image file.
        /// </param>
        public void StartScreenshot(string _fileName)
        {
            StartCoroutine(TakeScreenshot(_fileName));
        }

        /// <summary>
        /// Returs correct form of a Folder Name( adds a '/' before the name if not added).
        /// </summary>
        /// <returns>
        /// Correct folder Name format as String.
        /// </returns>
        private string CheckFolderName() 
        {
            if (folderName[0] != '/')
            {
                folderName = folderName.Insert(0, "/");
            }

            return folderName;
        }

        /// <summary>
        /// Creates a folder in Persistent Data Path location.
        /// </summary>
        /// <param name="directoryName">
        /// Folder Name
        /// </param>
        /// <returns></returns>
        private DirectoryInfo CreateFolder(string directoryName)
        {
            return Directory.CreateDirectory(Application.persistentDataPath + directoryName);
        }

        /// <summary>
        /// Load all Image Names from Screenshot folder to the Images array
        /// </summary>
        void GetAllImagesFromScreenshotFolder()
        {
            images = Directory.GetFiles(screenshotsFolder.FullName, "*.png");
        }

        /// <summary>
        /// Returns a Texture2D from previously saved Image.
        /// If file doesn't exist, returns the default Texture2D that was set.
        /// </summary>
        /// <param name="imagePath">
        /// Image file name
        /// </param>
        /// <returns></returns>
        public Texture2D CreateTextureFromImage(string imagePath)
        {
            Texture2D texture = defaultTexture;
            byte[] imageBytes = null;

            if (File.Exists(imagePath))
            {
                imageBytes = File.ReadAllBytes(imagePath);
                texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
                texture.LoadImage(imageBytes);
            }

            return texture;
        }


        ///<summary>
        /// Returns a Sprite from previously saved Texture2D.
        /// If file doesn't exist, returns the default Texture2D that was set.
        ///</summary>   
        public Sprite LoadimageToUi(string _fileName)
        {
            Texture2D tempTexture = CreateTextureFromImage("");
            Sprite tempSprite = Sprite.Create(tempTexture, new Rect(0, 0, tempTexture.width, tempTexture.height), Vector2.zero);

            // Debug.Log($"<ScreenshotManager> {$"{screenshotsFolder.FullName}/{_fileName}.png"}");

            if (_fileName != String.Empty && File.Exists($"{screenshotsFolder.FullName}/{_fileName}.png"))
            {
                tempTexture = CreateTextureFromImage($"{screenshotsFolder.FullName}/{_fileName}.png");
                tempSprite = Sprite.Create(tempTexture, new Rect(0, 0, tempTexture.width, tempTexture.height), Vector2.zero);
            }

            return tempSprite;
        }
    }
}
