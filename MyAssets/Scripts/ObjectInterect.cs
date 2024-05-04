using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInterect : MonoBehaviour
{
    public Chapter1Scene scriptChapter1Scene;
    public GuideScript scriptGuideScript;
    bool canInteract = false;
    Collider2D currentCollider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (scriptChapter1Scene.isDialogue)
                return;
            if (currentCollider.gameObject.tag == "Photo")
                scriptChapter1Scene.TriggerObjectPhoto();
            else if (currentCollider.gameObject.tag == "Certificate")
                scriptChapter1Scene.TriggerObjectCertificate();
            else if (currentCollider.gameObject.tag == "Parent")
                scriptChapter1Scene.TriggerObjectParent();
            else if (currentCollider.gameObject.tag == "Door")
                scriptChapter1Scene.TriggerObjectDoor();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Door" || other.gameObject.tag == "Photo" || other.gameObject.tag == "Certificate" || other.gameObject.tag == "Parent")
        {
            scriptGuideScript.FadeInGuideInteraction();
            canInteract = true;
            currentCollider = other;
        }
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     //key input E
    //     if (Input.GetKeyDown(KeyCode.E) && canInteract)
    //     {
    //         if (scriptChapter1Scene.isDialogue)
    //             return;
    //         if (other.gameObject.tag == "Photo")
    //             scriptChapter1Scene.TriggerObjectPhoto();
    //         else if (other.gameObject.tag == "Certificate")
    //             scriptChapter1Scene.TriggerObjectCertificate();
    //         else if (other.gameObject.tag == "Parent")
    //             scriptChapter1Scene.TriggerObjectParent();
    //         else if (other.gameObject.tag == "Door")
    //             scriptChapter1Scene.TriggerObjectDoor();
    //     }
    // }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Door" || other.gameObject.tag == "Photo" || other.gameObject.tag == "Certificate" || other.gameObject.tag == "Parent")
        {
            scriptGuideScript.FadeOutGuideInteraction();
            canInteract = false;
            currentCollider = null;
        }
    }
}
