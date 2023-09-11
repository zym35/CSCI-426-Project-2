using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private bool isCoroutineRunning = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Truck" && !isCoroutineRunning)
        {
            StartCoroutine(RemoveFromStackEverySecond(collider.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Truck")
        {
            isCoroutineRunning = false;
        }
    }

    IEnumerator RemoveFromStackEverySecond(GameObject truck)
    {
        isCoroutineRunning = true;

        while (isCoroutineRunning)
        {
            truck.GetComponent<TruckManager>().removeFromStack();
            yield return new WaitForSeconds(1f);
        }
    }
}
