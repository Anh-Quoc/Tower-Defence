
using UnityEngine;

public class SparkAnimator : MonoBehaviour
{

    public void OnShootingStart()
    {
        GetComponent<Animator>().SetBool("isShooting", true);
    }

    public void OnShootingEnd()
    {
        GetComponent<Animator>().SetBool("isShooting", false);
    }
}