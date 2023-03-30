using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField]private Image image;
    // [SerializeField]private KitchenObjectSO kitchenObjectSO;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO){
        // this.kitchenObjectSO = kitchenObjectSO;
        image.sprite = kitchenObjectSO.sprite;
    }
}
