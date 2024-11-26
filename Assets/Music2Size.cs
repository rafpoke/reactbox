using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music2Size : MonoBehaviour
{
 
    public Music2Color aux_intensidade;

    private float Escala;

    public float fator_x = 0.5f; // o objeto será escalado em x caso fator seja diferente de 0.0f
    public float fator_y = 0.5f;
    public float fator_z = 0.0f;

    public float fator_size = 0.5f;
    public float fator_escala = 0.1f;

    public GameObject Objeto;

    private Vector3 vec_aux;

    void Start()
    {
        Objeto = gameObject; // gameObject supostamente passa o objeto ao qual o script está atrelado como parametro

        vec_aux = new Vector3(fator_x, fator_y, fator_z); // escala só no plano XY


    }

    void Update()
    {
        Escala = aux_intensidade.intensidade;
        transform.localScale = vec_aux * (fator_size + fator_escala*Escala); // a função transform local scale deve afetar o objeto atrelado ao script
        
        
    }

   
}