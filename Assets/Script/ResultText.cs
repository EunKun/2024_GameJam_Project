using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultText : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("ResultOpen");
    }
}
