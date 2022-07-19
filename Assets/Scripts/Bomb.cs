using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int count = 0;
    public int waitTime = 1;
    public bool start = false;
    public GameObject explosion;

    private GameObject bomb;
    //Checks if the bomb is on the ground and starts countdown to explosion
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "floor" && start == false)
        {
            bomb = this.gameObject;
            bomb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            start = true;
            InvokeRepeating("CountdownToExplosion", 0f, 5f);
        }
    }
    //Controls  how many times the countdown to explosion runs
    private void CountdownToExplosion()
    {
        Debug.Log("Here");
        if (count >= waitTime)
        {
            CancelInvoke();
            Explode();
        }
        count++;

    }
    //Gets the position of the bomb, gets all objects in the exploson area (front, back, and both sides of the bomb), destroies them and destroies the bomb
    private void Explode()
    {
        
        float x = bomb.transform.position.x;
        float y = 1.009f;
        float z = bomb.transform.position.z;

        //Sets the explosion area (front, back, and both sides of the bomb)
        Vector3 toDestroyXP = new Vector3(x + 1, y, z);
        Vector3 toDestroyXM = new Vector3(x - 1, y, z);
        Vector3 toDestroyZP = new Vector3(x, y, z + 1);
        Vector3 toDestroyZM = new Vector3(x, y, z - 1);
        //Gets the objects on the explosion area
        Collider[] toDestroyXPCollider = Physics.OverlapSphere(toDestroyXP, 0.1f);
        Collider[] toDestroyXMCollider = Physics.OverlapSphere(toDestroyXM, 0.1f);
        Collider[] toDestroyZPCollider = Physics.OverlapSphere(toDestroyZP, 0.1f);
        Collider[] toDestroyZMCollider = Physics.OverlapSphere(toDestroyZM, 0.1f);
        //Concenrate all objects on one Array
        ArrayList toDestroyAll = new ArrayList{toDestroyXMCollider, toDestroyXPCollider, toDestroyZMCollider, toDestroyZPCollider};
        //Runs the arrays and destroies the objects
        foreach(Collider[] toDestroyPart in toDestroyAll)
        {
            if (toDestroyPart.Length > 0)
            {
                foreach (Collider toDestroy in toDestroyPart)
                {
                    if (toDestroy.gameObject.tag == "npc" || toDestroy.gameObject.tag == "player")
                    {
                        //Put damage here and point count to the one that created this bomb
                    }
                    Destroy(toDestroy.gameObject);
                }
            }
        }
        Vector3 explosionPos = new Vector3(bomb.transform.position.x, bomb.transform.position.y, bomb.transform.position.z);
        Instantiate(explosion, explosionPos, Quaternion.identity);
        Destroy(bomb);
    }
}
