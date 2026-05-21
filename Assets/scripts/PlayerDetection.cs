using UnityEngine;
using Random = System.Random;

public class PlayerDetection : MonoBehaviour
{
    public Animator anim;

    public static Random rnd = new Random();

    private void OnTriggerEnter(Collider other)
    {
        if (rnd.Next(3) == 0) 
        {  
            anim.SetTrigger("PlayerDetected");
        }
    }
}
