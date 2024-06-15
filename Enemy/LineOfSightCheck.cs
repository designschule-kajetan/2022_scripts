using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightCheck : MonoBehaviour
{
    private GameObject targetObject;
    private Coroutine detectPlayer;

    [SerializeField] private SimpleEnemyBehaviour seb;

    [SerializeField] private LayerMask layerCovers;

    [SerializeField] private float angleViewField = 60;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetObject = other.gameObject;
            detectPlayer = StartCoroutine(DetectPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targetObject = null;
            StopCoroutine(detectPlayer);
        }
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 direction = targetObject.transform.position - transform.position;
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);
            float targetAngle = Vector3.Angle(transform.forward, direction);

            bool isNotSeen = targetAngle > angleViewField || IsCharacterCovered(direction, distance);
            
            Debug.Log("Is Character covered? " + isNotSeen);
            if (!isNotSeen)
            {
                seb.hasTarget = true;
            }
            else
            {
                seb.hasTarget = false;
            }
        }
    }

    bool IsCharacterCovered(Vector3 targetDirection, float distanceToTarget)
    {
        RaycastHit[] hits = new RaycastHit[5];
        
        Ray ray = new Ray(transform.position, targetDirection);
        
        int amountOfHits = Physics.RaycastNonAlloc(ray, hits, distanceToTarget, layerCovers);
        
        if (amountOfHits > 0)
        {
            return true;
        }
        
        return false;
    }
}
