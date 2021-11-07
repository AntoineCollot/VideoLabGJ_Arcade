using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
    GameObject sender;
    public void Fire(GameObject sender, Vector3 direction, float speed)
    {
        GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        this.sender = sender;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null)
            return;

        //Ignore collision with the sender's layer
        if (other.gameObject.layer == sender.layer)
            return;

        if(other.CompareTag("ShooterPlayer"))
        {
            ShooterManager.Instance.PlayerDamaged();
        }

        Destroy(gameObject);
    }
}
