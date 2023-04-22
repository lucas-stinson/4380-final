using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPhysicsMaterial : MonoBehaviour
{
    public int targetLayer;
    public PhysicMaterial materialToApply;

    private void Start()
    {
        Collider[] collidersOnLayer = Physics.OverlapSphere(transform.position, Mathf.Infinity, 1 << targetLayer);
        foreach (Collider collider in collidersOnLayer)
        {
            collider.material = materialToApply;
        }
    }
}
