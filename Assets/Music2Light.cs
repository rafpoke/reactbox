using UnityEngine;

public class Music2Light : MonoBehaviour
{
    public Light sceneLight1;  // A luz que você deseja mudar a cor
    public Light sceneLight2;  // A luz que você deseja mudar a cor
    public Light MainLight;    // A luz que você deseja mudar a cor

    public int sampleSize = 1024;  // Número de amostras a serem analisadas (deve ser uma potência de 2)
    public int MaxFreq = 5000;
    public FFTWindow fftWindow = FFTWindow.Rectangular;  // Tipo de janela FFT, pode ajustar para melhor precisão

    public AudioClipSwitcher2 audioClipSwitcher;

    public Color cor_main;

    public float freq;

    private Vector3 scaleChange;
    private Vector3 baseScale;


    private AudioSource audioSource;
    private float[] spectrumData;

    public float intensidade;
    private float intensidade_aux;

    private int count;

    void Start()
    {
        // Configura o AudioSource para usar a música pré-salva
       audioSource = audioClipSwitcher.audioSource;

        if (audioSource == null)
        {
            Debug.LogError("Primeiro AudioSource não atribuído!");
        }
            //audioSource.clip = musicClip;
            //audioSource.loop = true;  // Faz a música tocar continuamente
            //audioSource.Play();       // Começa a tocar o áudio
        

        // Inicializa o array de amostras
        spectrumData = new float[sampleSize];
        count = 0;

    }

    void Update()
    {
        count++;
        // Pega os dados de espectro de áudio (FFT)
        if (audioSource != null){
            audioSource.GetSpectrumData(spectrumData, 0, fftWindow);

            // Calcula a frequência dominante
            freq = GetDominantFrequency(spectrumData);

            if (count == 10)
            {
                // Muda a cor da luz com base na frequência dominante
                ChangeLightColorBasedOnFrequency(freq);
                sceneLight1.intensity = intensidade_aux;
                sceneLight2.intensity = intensidade_aux;
                MainLight.intensity = intensidade;

                
                //Debug.LogError("Intensidade atual :" + intensidade);
                
                
                count = 0;
            }
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

        intensidade = Mathf.Sqrt(sum*sampleSize) ;
        if (intensidade >= 8) {
            intensidade = 8;
        }

        intensidade_aux = (sum*sampleSize) * 0.2f;
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
        float hue = Mathf.InverseLerp(20, MaxFreq-500, frequency);  // Frequência mapeada de 20Hz a 20000Hz
        sceneLight1.color = Color.HSVToRGB(hue, 1f, 1f);  // Converte o valor da frequência para cor
        sceneLight2.color = Color.HSVToRGB(hue, 1f, 1f);  // Converte o valor da frequência para cor

        float hue_main = Mathf.InverseLerp(20, MaxFreq, frequency);  // Frequência mapeada de 20Hz a 20000Hz
        MainLight.color = Color.HSVToRGB(hue_main, 1f, 1f);    // Converte o valor da frequência para cor
        cor_main = Color.HSVToRGB(hue_main, 1f, 1f);    // Converte o valor da frequência para cor

    }
}