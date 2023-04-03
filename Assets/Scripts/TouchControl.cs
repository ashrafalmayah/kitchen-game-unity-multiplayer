using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour
{
    public static TouchControl Instance { get; private set; }

    [SerializeField]private Button pressAnyKeyToContinueButton;
    public event EventHandler OnTouchInteractAction;
    public event EventHandler OnTouchInteractAlternateAction;

    [SerializeField]private Button pickupDropButton;
    [SerializeField]private Button cuttingButton;


    private void Awake() {
        Instance = this;

        pickupDropButton.onClick.AddListener(() => {
            OnTouchInteractAction?.Invoke(this , EventArgs.Empty);
        });
        cuttingButton.onClick.AddListener(() => {
            OnTouchInteractAlternateAction?.Invoke(this , EventArgs.Empty);
        });
        pressAnyKeyToContinueButton.onClick.AddListener(() => {
            OnTouchInteractAction?.Invoke(this , EventArgs.Empty);
        });
    }
}
