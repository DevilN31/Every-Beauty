using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolController : MonoBehaviour
{
    [SerializeField] GameObject connectedTool;
    [SerializeField] int buttonId;
    [SerializeField] MaskController.MaskType maskType;

    private void OnEnable()
    {
        Button _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>UiManager.instance.SetActiveTool(buttonId));
    }

    private void OnDisable()
    {
        Button _button = GetComponent<Button>();
        _button.onClick.RemoveListener(() => UiManager.instance.SetActiveTool(buttonId));
    }

    public int ButtonId
    {
        get { return buttonId; }
    }

    public void SetToolActive(bool isActive)
    {
        connectedTool.SetActive(isActive);

        if(isActive) // Update mask only if this tool is active
        MaskController.instance.UpdateMaskType(maskType);
    }
}
