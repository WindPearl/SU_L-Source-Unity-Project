using UnityEngine;

public class RainbowRingSU : GenerationsObject
{
    public float FirstSpeed = 25f;
    public bool IsChangeCameraWhenChangePath = false;
    public bool IsChangePath = false;
    public bool IsHeadToVelocity = false;
    public bool IsTo3D = false;
    public float KeepVelocityDistance = 5f;
    public float OutOfControl = 0.5f;

    public GameObject rainbowRingObject;
    public Vector3 scaleChange, positionChange;

    public AudioClip rainbowRingSound;
    public Transform startPoint;
    public ParticleSystem starParticle;
    public ParticleSystem rainbowParticle;
    public float rainbowRingCheck;

    private AudioSource audioSource;

    private float duration;
    private float outOfControl;

   // public ShowDetailsOptions.Inspector.rainbowRingObject.transform.localScale.y
    

    public override void OnValidate()
    {
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scaleChange = new Vector3(-0.103f, -0.103f, -0.103f);
        positionChange = new Vector3(0.0f, 0.0f, 0.0f);
        Debug.Log(rainbowRingObject.transform.localScale + " || Time : " + Time.deltaTime);
    }

    #region State Rainbow Ring
    private void StateRainbowRingStart()
    {
        player.transform.position = startPoint.position;
        duration = player.stateMachine.lastStateTime + PhysicsExtension.Time(KeepVelocityDistance, FirstSpeed);
        outOfControl = player.stateMachine.lastStateTime + OutOfControl;
        player.rigidbody.velocity = Vector3.zero;
        player.isBoosting = false;
    }
    private void StateRainbowRing()
    {
        if (Time.time < duration)
        {
            player.rigidbody.velocity = -transform.forward * FirstSpeed;
        }

        if (Mathf.Abs(player.rigidbody.velocity.y) > 0)
        {
            startPoint.forward = player.rigidbody.velocity.normalized;
        }

        if (Time.time < outOfControl)
        {
            player.transform.rotation = Quaternion.LookRotation(-startPoint.up, startPoint.forward);
        }
        else
        {
            player.stateMachine.ChangeState(player.StateFall, gameObject);
        }

        if (rainbowRingCheck > 3 && rainbowRingObject.transform.localScale.y == 1.1f)
        {
            rainbowRingObject.transform.localScale += scaleChange;
            rainbowRingObject.transform.position += positionChange;
        }

        if (rainbowRingObject.transform.localScale.y == 1f)
        {
            rainbowRingCheck += 1;
        }

        // Move upwards when the rainbow ring hits the floor or downwards
        // when the rainbow ring scale extends 1.0f.
        if (rainbowRingObject.transform.localScale.y < 0f || rainbowRingObject.transform.localScale.y > 1f)
        {
            scaleChange = -scaleChange;
            positionChange = -positionChange;
            Debug.Log(rainbowRingObject.transform.localScale + " || Time : " + Time.deltaTime);
        }
    }
    private void StateRainbowRingEnd()
    {
        player.canHomming = true;
        rainbowRingCheck = 0;
        //    rainbowRingObject.transform.localScale.y += (rainbowRingObject.transform.localScale.y += (rainbowRingObject.transform.localScale.y));
        //    rainbowRingObject.transform.position += 1f -(rainbowRingObject.transform.position);
    }
    #endregion State Rainbow Ring

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.playerTag))
        {
            audioSource.PlayOneShot(rainbowRingSound);
            starParticle.Play();
            rainbowParticle.Play();
            player = other.GetComponent<Player>();
            player.stateMachine.ChangeState(StateRainbowRing, gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        GizmosExtension.DrawTrajectory(startPoint.position, startPoint.forward, FirstSpeed, 2, KeepVelocityDistance);
        Gizmos.color = Color.red;
        GizmosExtension.DrawTrajectory(startPoint.position, startPoint.forward, FirstSpeed, OutOfControl, KeepVelocityDistance);
    }
}