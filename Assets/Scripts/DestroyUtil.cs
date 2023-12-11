using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DestroyUtil : MonoBehaviour
{
    private Animator animate;
    void Start()
    {
        animate = GetComponent<Animator>();
    }
    public void DestroyHelper()
    {
        animate.Play("destruction");
        Destroy(this.gameObject, 0.15f);
        //Destroy(gameObject);
    }
}
