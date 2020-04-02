using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;

    Health target;

    // Start is called before the first frame update
    public void SetTarget(Health target) 
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform == null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private Vector3 GetAimLocation() 
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.transform.position;
        return target.transform.position + Vector3.up * targetCapsule.height/2;
    }
}
