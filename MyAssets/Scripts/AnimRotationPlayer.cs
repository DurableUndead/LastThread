using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotationPlayer : MonoBehaviour
{
    PlayerMovement scriptPlayerMovement;
    public GameObject sideWalkMirrorGO;
    public GameObject idleMirrorGO;
    public GameObject sideButFreezeGO;

    public void Start()
    {
        scriptPlayerMovement = GetComponent<PlayerMovement>();
    }
    public void mirrorAlanRotation()
    {
        StartCoroutine(PlayerRotationZ());
    }
    IEnumerator PlayerRotationZ()
    {
        float targetZRotation = -180f;
        float currentZRotation = transform.rotation.eulerAngles.z;

        // Menyesuaikan currentZRotation ke dalam rentang -180 hingga 180 derajat
        if (currentZRotation > 180f)
            currentZRotation -= 360f;

        while (currentZRotation > targetZRotation)
        {
            if (currentZRotation > -120f)
                currentZRotation -= 50f * Time.deltaTime;
            else if (currentZRotation > -150f)
                currentZRotation -= 40f * Time.deltaTime;
            else if (currentZRotation > targetZRotation)
                currentZRotation -= 30f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, currentZRotation);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, targetZRotation);
        sideWalkMirrorGO.SetActive(false);
        idleMirrorGO.SetActive(true);
        sideButFreezeGO.SetActive(false);
        scriptPlayerMovement.canMove = true;
    }
}