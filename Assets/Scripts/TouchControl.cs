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
    [SerializeField]private GameObject joyStickPrefab;
    [SerializeField]private Canvas canvas;
    private RectTransform spawnedJoyStick;


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

    private void Start() {
        GameInput.Instance.OnTouchPerformed += GameInput_OnTouchPerformed;
    }


    private void Update() {
        
    }

    private void GameInput_OnTouchPerformed(object sender, EventArgs e){
        Vector2 touchPositionOnScreen = GameInput.Instance.GetTouchPotitionVector();
        float canvasScale = canvas.transform.localScale.x;
        Vector2 touchPositionInCanvasSpace = (touchPositionOnScreen / canvasScale);
        float screenCenterPositionX = canvas.GetComponent<RectTransform>().rect.width / 2;
        if(touchPositionInCanvasSpace.x < screenCenterPositionX){
            if(spawnedJoyStick == null){
                spawnedJoyStick = Instantiate(joyStickPrefab , this.gameObject.transform).GetComponent<RectTransform>();
            }
            spawnedJoyStick.anchoredPosition = touchPositionInCanvasSpace;
        }
    }

}
