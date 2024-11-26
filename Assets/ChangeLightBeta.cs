using UnityEngine;

public class FrequencyBasedLightColorChangerBeta : MonoBehaviour
{
    public Light sceneLight1;  // A luz que você deseja mudar a cor
    public Light sceneLight2;  // A luz que você deseja mudar a cor
    public Light MainLight;  // A luz que você deseja mudar a cor
    public int sampleSize = 1024;  // Número de amostras a serem analisadas (deve ser uma potência de 2)
    public int MaxFreq = 6000;
    public FFTWindow fftWindow = FFTWindow.Rectangular;  // Tipo de janela FFT, pode ajustar para melhor precisão
    private AudioSource audioSource;
    private float[] spectrumData;

    private float intensidade;
    private float intensidade_aux;

    private int count;

    


    void Start()
    {
        // Configura o AudioSource para capturar o som do microfone
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        //audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) {}  // Espera o microfone começar a gravar
        audioSource.Play();  // Começa a tocar o áudio capturado

        // Inicializa o array de amostras
        spectrumData = new float[sampleSize];

       

        count = 0;
    }

    void Update()
    {
        count++;
        // Pega os dados de espectro de áudio (FFT)
        audioSource.GetSpectrumData(spectrumData, 0, fftWindow);

        // Calcula a frequência dominante
        float frequency = GetDominantFrequency(spectrumData);

        if (count == 10){
            // Muda a cor da luz com base na frequência dominante
        ChangeLightColorBasedOnFrequency(frequency);
        sceneLight1.intensity = intensidade_aux;
        sceneLight2.intensity = intensidade_aux;
        MainLight.intensity = intensidade;
        Debug.Log("O valor atual da variável é: " + intensidade);
        count = 0;
        }
        
    }

    float GetDominantFrequency(float[] spectrum)
    {
        float maxVal = 0;
        int maxIndex = 0;

        

        float sum = 0;
        // Encontra a frequência dominante
        for (int i = 0; i < spectrum.Length; i++)
        {   
            sum += spectrum[i] * spectrum[i];
            if (spectrum[i] > maxVal)
            {
                maxVal = spectrum[i];
                maxIndex = i;
            }
        }

        intensidade = Mathf.Sqrt(sum*sampleSize) * 20 + 1;
        if (intensidade >= 8) {
            intensidade = 8;
        }

        intensidade_aux = Mathf.Sqrt(sum*sampleSize) * 13 + 1;
        if (intensidade_aux >= 8) {
            intensidade_aux = 8;
        }
        // Converte o índice da FFT para frequência (em Hz)
        float freqN = maxIndex;  // Índice normalizado
        float frequency = freqN * AudioSettings.outputSampleRate / 2 / spectrum.Length;

        return frequency;
    }

    void ChangeLightColorBasedOnFrequency(float frequency)
    {   

        // Normaliza a frequência para um intervalo de cores (ajustar de acordo com a faixa de frequência esperada)
        float hue = Mathf.InverseLerp(20, MaxFreq, frequency);  // Frequência mapeada de 20Hz a 2000Hz
        sceneLight1.color = Color.HSVToRGB(hue, 1f, 1f); // Converte o valor da frequência para cor
        sceneLight2.color = Color.HSVToRGB(hue, 1f, 1f); // Converte o valor da frequência para cor
        MainLight.color = Color.HSVToRGB(hue, 1f, 1f); // Converte o valor da frequência para cor
    }
}