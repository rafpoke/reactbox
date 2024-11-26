using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music2Features : MonoBehaviour
{
 
    public int sampleSize = 1024;  // Número de amostras a serem analisadas (deve ser uma potência de 2)
    
    public FFTWindow fftWindow = FFTWindow.Rectangular;  // Tipo de janela FFT, pode ajustar para melhor precisão

    public AudioClipSwitcher2 audioClipSwitcher;


    public float freq;



    private AudioSource audioSource;
    private float[] spectrumData;

    public float intensidade;

    public float intensidade_bass;
    public float intensidade_mid;
    public float intensidade_hi;


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
        

    }

    void Update()
    {
        
        // Pega os dados de espectro de áudio (FFT)
        if (audioSource != null){
            audioSource.GetSpectrumData(spectrumData, 0, fftWindow);

            // Calcula a frequência dominante
            freq = GetDominantFrequency(spectrumData);

            
        }
        
        
    }

    float GetDominantFrequency(float[] spectrum)
    {
        float maxVal = 0;
        int maxIndex = 0;

        

        float sum_bass = 0.0f;
        float sum_mid = 0.0f;
        float sum_hi = 0.0f;
        // Encontra a frequência dominante
        for (int i = 0; i < spectrum.Length; i++)
        {   
            if (i <=6) // para o sample rate de 48kHz e considerando bass até 250Hz
            {
                sum_bass += spectrum[i] * spectrum[i];
            }
            else if (i >6 && i<=87) // mid considerando entre 250Hz e 4kHz
            {
                sum_mid += spectrum[i] * spectrum[i];
            }
            else
            {
                sum_hi += spectrum[i] * spectrum[i];
            }
            
            if (spectrum[i] > maxVal)
            {
                maxVal = spectrum[i];
                maxIndex = i;
            }
        }

        // ajustar os valores
        intensidade_bass = Mathf.Sqrt(sum_bass*sampleSize) ;
        intensidade_mid = Mathf.Sqrt(sum_mid*sampleSize) ;
        intensidade_hi = Mathf.Sqrt(sum_hi*sampleSize) ;

        // limitar os valores
        if (intensidade_bass >= 8) {
            intensidade_bass = 8;
        }
        if (intensidade_mid >= 8) {
            intensidade_mid = 8;
        }
        if (intensidade_hi >= 8) {
            intensidade_hi = 8;
        }

        // intensidade total
        intensidade = intensidade_bass + intensidade_mid + intensidade_hi;
        
        // Converte o índice da FFT para frequência (em Hz)
        float freqN = maxIndex;  // Índice normalizado
        float frequency = freqN * AudioSettings.outputSampleRate / 2 / spectrum.Length;

        return frequency;
    }

    
}