using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//en caso de que no tenga el componente se añade 
[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    public Transform player;
    private bool isPlayerInRange;
    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position,direction);
            //dibujar/crear un gizmos
            Debug.DrawRay(transform.position,direction,Color.green,Time.deltaTime,true);
            RaycastHit raycastHit;
            // al poner la palabra reservada out
            // you pass the function a variable that the function will set for you 
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position,0.1f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,player.position + Vector3.up);
    }
}
