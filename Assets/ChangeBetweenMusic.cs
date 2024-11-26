using UnityEngine;

public class AudioClipSwitcher : MonoBehaviour
{
    public AudioClip audioClip1;  // Primeiro AudioClip
    public AudioClip audioClip2;  // Segundo AudioClip
    public AudioSource audioSource;
    private float savedTimeClip1 = 0f;  // Posição salva do primeiro áudio
    private float savedTimeClip2 = 0f;  // Posição salva do primeiro áudio
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
            if (audioClip2 != null)
            {
                audioSource.clip = audioClip2;
                audioSource.time = savedTimeClip2;  // Retoma do ponto salvo
                audioSource.Play();  // Começa a tocar o segundo áudio do início
            }
            else
            {
                Debug.LogError("Segundo AudioClip não atribuído!");
            }
        }
        else
        {
            
            
            savedTimeClip2 = audioSource.time;
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
