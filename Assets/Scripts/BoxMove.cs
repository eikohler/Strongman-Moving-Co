using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    private float gravityBase;
    public float gravityMultiplyer;
    public float fallingThreshold;
    [SerializeField] private Rigidbody2D rb;


    private void Start() {
        gravityBase = rb.gravityScale;
    }

    // Update is called once per frame
    private void Update(){
        if(rb.velocity.y < fallingThreshold){
            rb.gravityScale = gravityBase * gravityMultiplyer;
        }else{
            rb.gravityScale = gravityBase;
        }
    }
}
