using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("Menus")]
    [SerializeField] GameObject quitMenu;

    [Header("Tool Buttons")]
    [Tooltip("Toggle on = Editing mode\nToggle off = Play Mode")]
    [SerializeField] Toggle isEditing;
    [SerializeField] List<GameObject> editToolButtons;
    [SerializeField] List<GameObject> playToolButtons;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        quitMenu.SetActive(false);

        //Activate first edit tool
        SetActiveTool(1);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            quitMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetActiveTool(int _id)
    {
        isolateTool(_id);
    }

    void isolateTool(int _id)
    {
        if (isEditing.isOn)
        {
            // Deactivate Play tools.
            foreach (GameObject tool in playToolButtons)
            {
                if(tool.activeSelf)
                tool.GetComponent<ToolController>().SetToolActive(false);
            }

            // Activate selected Edit tool and deactivate others
            foreach (GameObject button in editToolButtons)
            {
                if (_id != button.GetComponent<ToolController>().ButtonId)
                {
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    button.GetComponent<ToolController>().SetToolActive(false);
                }
                else
                {
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    button.GetComponent<ToolController>().SetToolActive(true);
                }
            }
        }
        else
        {
            // Deactivate Edit tools.
            foreach (GameObject tool in editToolButtons)
            {
                if (tool.activeSelf)
                    tool.GetComponent<ToolController>().SetToolActive(false);
            }

            // Activate selected Play tool and deactivate others
            foreach (GameObject button in playToolButtons)
            {
                if (_id != button.GetComponent<ToolController>().ButtonId)
                {
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    button.GetComponent<ToolController>().SetToolActive(false);
                }
                else
                {
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    button.GetComponent<ToolController>().SetToolActive(true);
                }
            }
        }
    }
}
