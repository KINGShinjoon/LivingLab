using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Canvas canvas;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.LookAt(other.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
