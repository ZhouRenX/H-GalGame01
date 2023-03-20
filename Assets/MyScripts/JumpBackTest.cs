using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBackTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("JumpBack", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void JumpBack()
    {
        MyAdvEngineController.Instance.JumpScenario("JumpBack");
    }
}
