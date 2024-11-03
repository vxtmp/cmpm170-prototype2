using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{

    [SerializeField]
    private GameObject playerObj;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
