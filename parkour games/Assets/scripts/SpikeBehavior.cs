using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    //ÊÇ·ñ¸úËæ
    public bool isAlive = true;
    public bool isFollowing = false;

    //¸úËæµÄ½ÇÉ«
    public Transform target;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing && isAlive)
        {
            transform.position = new Vector2(target.position.x, transform.position.y);
        }
        
    }
}
