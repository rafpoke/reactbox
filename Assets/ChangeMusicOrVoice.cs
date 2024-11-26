using UnityEngine;

public class AudioClipSwitcher2 : MonoBehaviour
{
    public AudioClip audioClip1;  // Primeiro AudioClip
    public AudioSource audioSource;
    private float savedTimeClip1;  // Posição salva do primeiro áudio
    private bool isPlayingClip1 = true;  // Controla qual clipe está tocando

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        


        // Inicia tocando o primeiro áudio
        if (audioClip1 != null)
        {
            audioSource.clip = audioClip1;
            Debug.LogError("Check2");
            audioSource.Play();

            Debug.LogError("rate de frequencia: " + audioClip1.frequency);
        }
        else
        {
            Debug.LogError("Primeiro AudioClip não atribuído!");
        }
        
    }

    void Update()
    {

        if (audioSource == null)
        {
            Debug.LogError("Audiosource não gerado!");
        }
        // Alterna entre os clipes ao pressionar a tecla 'Space'
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError(isPlayingClip1);
            
            SwitchAudioClip();
        }
    }

    void SwitchAudioClip()
    {
        if (isPlayingClip1)
        {
            // Salva o ponto onde o primeiro clipe parou
            
            savedTimeClip1 = audioSource.time;
            audioSource.Stop();

            // Alterna para o segundo clipe
        
            // inicia o microphone
            audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
            // mantem o microphone gravando
            audioSource.loop = false;
            while (!(Microphone.GetPosition(null) > 0)) {}  // Espera o microfone começar a gravar
            audioSource.Play();  // Começa a tocar o áudio capturado
           
            
        }
        else
        {
            
            // para o audio atual
            audioSource.Stop();
            // Retorna para o primeiro clipe na posição salva
            audioSource.clip = audioClip1;
            audioSource.time = savedTimeClip1;  // Retoma do ponto salvo
            audioSource.Play();  // Continua tocando o primeiro clipe
        }

        // Alterna o estado do controle de qual clipe está tocando
        isPlayingClip1 = !isPlayingClip1;
    }
}
