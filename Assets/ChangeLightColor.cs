using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour
{
    // Start is called before the first frame update
    private Light luz_main;

    public Music2Color cor_fonte;
    void Start()
    {
        luz_main = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        luz_main.color = cor_fonte.cor_main;
    }
}
