using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class AwaitKeybindInput : MonoBehaviour
{
    [SerializeField] private InputActionReference changingAction;
    [SerializeField] private GameObject bindText;
    [SerializeField] private InputActionAsset inputControls;
    public string targetBindingName;

    public void WaitForButtonBind()
    {
        InputSystem.onAnyButtonPress.CallOnce(control => SetControl(control.name));
        int bindingIndex = changingAction.action.bindings.IndexOf(x => x.isPartOfComposite && x.name == targetBindingName);
        Debug.Log("Binding index: " + bindingIndex);
        RebindingOperation rebindingOperation = changingAction.action.PerformInteractiveRebinding()
            .WithTargetBinding(bindingIndex)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(context => CompleteKeybind());
    }

    private void CompleteKeybind()
    {
        Debug.Log("Kibind re-bound");
    }

    private void SetControl(string controlName)
    {
        bindText.GetComponent<TMPro.TextMeshProUGUI>().text = controlName;
    }
}
