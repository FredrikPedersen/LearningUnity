using UnityEngine;
using UnityEngine.SceneManagement;
[DisallowMultipleComponent]
public class Rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float thrustPower = 15f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelCompleteSound;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;


    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive) 
        { 
            RespondToThrustInput();
            ResondToRotateInput();
        }
    }

    private void OnCollisionEnter(Collision collision) 
        {

        if (state != State.Alive) 
        {
            return;
        }
        
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartFailureSequence();
                break;

        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteSound);
        successParticle.Play();
        Invoke("LoadNextLevel", 1f); //Parameterize time
    }

    private void StartFailureSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        deathParticle.Play();
        audioSource.PlayOneShot(deathSound);
        Invoke("LoadFirstLevel", 2f);
    }

    private void LoadNextLevel() 
    {
        SceneManager.LoadScene(1); //TODO Allow for more than 2 levels
    }

    private void LoadFirstLevel() 
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) 
        { //can thrust while rotating
            ApplyThrust();
        } else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustPower);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticle.Play();
    }

    private void ResondToRotateInput()
    {

        rigidbody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }
}
