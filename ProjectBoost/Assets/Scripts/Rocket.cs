using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField]float rcsThrust = 200f;
    [SerializeField] float thrustPower = 15f;

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
            Thrust();
            Rotate();
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
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); //Parameterize time
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 2f);
                break;

        }
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1); //TODO Allow for more than 2 levels
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            rigidbody.AddRelativeForce(Vector3.up * thrustPower);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
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
