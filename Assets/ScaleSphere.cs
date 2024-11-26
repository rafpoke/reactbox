using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScaler : MonoBehaviour
{
    // Variável pública para definir o tamanho da esfera (valor de escala)
    private float sphereScale;

    public Music2Light aux_intensidade;

    public GameObject sphere;

    private int count = 0;

    // Atualiza a escala da esfera no início do jogo
    void Start()
    {   
  
        
    }

    void Update()
    {
        sphereScale = aux_intensidade.intensidade;

        count++;
        // Altera a escala do Transform da esfera baseado no valor fornecido
        if (count == 10)
        {
            UpdateScale();
            count = 0;
        }
        
    }

    // Função para atualizar a escala da esfera
    public void UpdateScale()
    {
        // Atualiza a escala de acordo com o valor da variável sphereScale
        transform.localScale = Vector3.one * (1 + 0.1f*sphereScale);
    }
}
