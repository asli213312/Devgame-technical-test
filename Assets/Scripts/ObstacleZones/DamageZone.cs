using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageZone : AbstractZone
{
    [SerializeField] private DamageZoneConfig config;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IZoneInteractable context) == false) return;
        
        if (context is PlayerController playerController && playerController.Model.IsInvinsibility == false) 
        {
            other.gameObject.SetActive(false);
        }

        InvokeTrigger();
    }
}