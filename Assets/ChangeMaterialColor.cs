using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    public Material material;  // Referência ao material que você quer mudar
    public Music2Color cores;
    public Color cor_aux;

    void Start()
    {
        if (material != null)
        {
            cor_aux = cores.cor_main;
            material.color = cores.cor_main;  // Altera a cor do material
            // nao entendi

        }
        else
        {
            Debug.LogWarning("Nenhum material foi atribuído.");
        }
    }


void Update()
    {

        if (material != null)
        {
            cor_aux = cores.cor_main;
            material.color = cores.cor_main;  // Altera a cor do material
            // nao entendi

        }
    }
}