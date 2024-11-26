using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightEasy : MonoBehaviour
{
    // Variável pública para definir o tamanho da esfera (valor de escala)

    public Music2Light aux_intensidade;

    public Light Luz1;

    private int count = 0;

    private Color cor;

    private float aux_freq;

    // Atualiza a escala da esfera no início do jogo
    void Start()
    {   
        
        
    }

    void Update()
    {
        

        count++;
        // Altera a escala do Transform da esfera baseado no valor fornecido
        if (count == 10)
        {   
            aux_freq = aux_intensidade.freq;
            ChangeLightColorBasedOnFrequency(aux_freq);
            UpdateScale();
            count = 0;
        }
        
    }

    // Função para atualizar a escala da esfera
    public void UpdateScale()
    {
        // Atualiza a escala de acordo com o valor da variável sphereScale
        Luz1.color = cor;
    }

   void ChangeLightColorBasedOnFrequency(float frequency)
    {
        float hue_main = Mathf.InverseLerp(20, 5000, 4000-frequency);  // Frequência mapeada de 20Hz a 20000Hz
        cor = Color.HSVToRGB(hue_main, 1f, 1f);    // Converte o valor da frequência para cor

    }

}

