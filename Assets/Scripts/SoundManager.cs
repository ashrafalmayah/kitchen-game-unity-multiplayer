using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [SerializeField]private AudioClipRefsSO audioClipRefsSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedupObject += Player_OnPickedupObject;
        BaseCounter.OnAnyObjectDroped += BaseCounter_OnAnyObjectDroped;
        TrashCounter.OnAnyTrashed += TrashCounter_OnAnyTrashed;
    }

    private void TrashCounter_OnAnyTrashed(object sender, System.EventArgs e){
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash , trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectDroped(object sender, System.EventArgs e){
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop , baseCounter.transform.position);
    }

    private void Player_OnPickedupObject(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.objectPickup , Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop , cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.deliveryFail , DeliveryManager.Instance.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.deliverySuccess , DeliveryManager.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray , Vector3 position , float volume = 1f){
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)] , position , volume);
    }

    private void PlaySound(AudioClip audioClip,Vector3 position , float volume = 1f){
        AudioSource.PlayClipAtPoint(audioClip , position , volume);
    }

    public void PlayFootStepSound(Vector3 position , float volume = 1f){
        PlaySound(audioClipRefsSO.footStep , position , volume);
    }
}
