﻿using UnityEngine;

[AddComponentMenu("Ring Engine/Player/Player Animation")]
[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    public Transform meshHolder, mesh;
    public float meshDamping = 12;

    [HideInInspector]
    public int idleActionIDNameHash,
        doIdleActionNameHash,
        absSpeedNameHash,
        horizontalSpeedNameHash,
        directionNameHash,
        ballNameHash,
        verticalSpeedNameHash,
        jumpNameHash,
        groundedNameHash,
        brakeNameHash,
        doubleJumpNameHash,
        grindStartNameHash,
        grindEndNameHash,
        dotVUpTForwardNameHash,
        springNameHash,
        groundDistanceNameHash,
        lightSpeedDashNameHash,
        hommingNameHash,
        hurtNameHash,
        stompNameHash,
        deadNameHash,
        ironPole2DStartNameHash,
        ironPole2DEndNameHash,
        pulleyStartNameHash,
        pulleyEndNameHash,
        upReelStartNameHash,
        upReelEndNameHash,
        dashRingStartNameHash,
        dashRingEndNameHash,
        jumpBoardStartNameHash,
        jumpBoardEndNameHash,
        BallNameHash,
        wallJumpStartNameHash,
        underwaterNameHash,
        squatNameHash;

    //Components
    public Animator animator;
    Player player;

    //Duration of idle loop
    public int maxIdleLoops = 3;
    private int idleLoops;
    //Number of idle actions
    public int idleActions;

    public Transform leftMouth;
    public Transform rightMouth;

    public Transform leftFeet;
    public Transform rightFeet;

    //Rigidbody absolute velocity
    float absSpeed;
    float verticalSpeed;
    float horizontalSpeed;

    int idleActionID = 0;
    Vector3 leftStickDirection;
    float direction;
    bool spring;
    bool lightSpeedDash;
    bool homming;
    public bool underwater;
    bool squat;

    private bool isSnowboarding;

    public HingeJoint joint;

    public SixWayDirections dir = new SixWayDirections();

    GameObject closestEnemy;
    new Rigidbody rigidbody;

    private void StateSpindashStart()
    {
        mesh.gameObject.SetActive(false);
    }

    void StateWaterSwirlGetPlayerStart()
    {
        animator.ResetTrigger("HipHopSlideEnd");

        animator.SetTrigger("HipHopSlideStart");
    }

    void StateWaterSwirlGetPlayerEnd()
    {
        animator.SetTrigger("HipHopSlideEnd");
    }

    private void StateRocketStart()
    {
        animator.SetTrigger("StateRocketStart");
    }

    private void StateRocketEnd()
    {
        animator.SetTrigger("StateRocketEnd");
    }

    void StateSnowBoardStart()
    {
        animator.SetTrigger("Snow Board Start");
    }
    private void GrindDamage()
    {
        animator.SetTrigger("Do Damage Action");
    }

    private void StateJumpCollisionStart()
    {
        if (player.isSnowboarding)
        {
            animator.SetInteger("TrickID", Random.Range(0, 8));
            animator.SetTrigger("DoJumpBoardTrick");
        }
    }

    void StateSnowBoardEnd()
    {

    }

    void StateSnowBoardJumpStart()
    {

        animator.SetTrigger("Snowboard Jump");
    }

    void StateSnowBoardDriftStart()
    {
        animator.ResetTrigger("DriftEnd");
        animator.SetTrigger("DriftStart");
    }
    void StateSnowBoardDriftEnd()
    {
        animator.SetTrigger("DriftEnd");
    }

    void StateSnowBoardGrindStart()
    {
        animator.SetTrigger("Snowboard Grind Start");
    }

    void StateSnowBoardGrindEnd()
    {
        if (!player.isGrinding)
        {
            animator.SetTrigger("Snowboard Grind End");
        }

    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        //animator = GetComponent<Animator>();
        idleActionIDNameHash = Animator.StringToHash("Idle Action ID");
        doIdleActionNameHash = Animator.StringToHash("Do Idle Action");
        absSpeedNameHash = Animator.StringToHash("Abs Speed");
        horizontalSpeedNameHash = Animator.StringToHash("Horizontal Speed");
        directionNameHash = Animator.StringToHash("Direction");
        ballNameHash = Animator.StringToHash("JumpBall");
        groundedNameHash = Animator.StringToHash("Grounded");
        verticalSpeedNameHash = Animator.StringToHash("Vertical Speed");
        jumpNameHash = Animator.StringToHash("Jump");
        brakeNameHash = Animator.StringToHash("Brake");
        doubleJumpNameHash = Animator.StringToHash("Double Jump");
        grindStartNameHash = Animator.StringToHash("Grind Start");
        grindEndNameHash = Animator.StringToHash("Grind End");
        dotVUpTForwardNameHash = Animator.StringToHash("Dot VUp TForward");

        groundDistanceNameHash = Animator.StringToHash("Ground Distance");
        lightSpeedDashNameHash = Animator.StringToHash("LightSpeedDash");
        hommingNameHash = Animator.StringToHash("Homming");
        hurtNameHash = Animator.StringToHash("Hurt");
        stompNameHash = Animator.StringToHash("Stomp");
        deadNameHash = Animator.StringToHash("Dead");
        ironPole2DStartNameHash = Animator.StringToHash("IronPole2DStart");
        ironPole2DEndNameHash = Animator.StringToHash("IronPole2DEnd");
        pulleyStartNameHash = Animator.StringToHash("PulleyStart");
        pulleyEndNameHash = Animator.StringToHash("PulleyEnd");
        upReelEndNameHash = Animator.StringToHash("UpReelEnd");
        upReelStartNameHash = Animator.StringToHash("UpReelStart");
        dashRingStartNameHash = Animator.StringToHash("DashRingStart");
        dashRingEndNameHash = Animator.StringToHash("DashRingEnd");
        jumpBoardStartNameHash = Animator.StringToHash("Jump Board Start");
        jumpBoardEndNameHash = Animator.StringToHash("Jump Board End");
        BallNameHash = Animator.StringToHash("Ball");
        wallJumpStartNameHash = Animator.StringToHash("WallJumpStart");
        underwaterNameHash = Animator.StringToHash("Underwater");
        squatNameHash = Animator.StringToHash("Squat");
    }
    Vector3 cameraRelative;
    public float tangent;
    Transform cameraMain;

    private Vector3 lastTangent;
    private Vector3 curTangent;

    private float smoothTangent;

    private void OnEnable()
    {
        meshHolder.rotation = Quaternion.identity;
        mesh.transform.rotation = transform.rotation;
    }

    // Use this for initialization
    void Start()
    {
        cameraMain = Camera.main.transform;
        //animator = GetComponent<Animator>();
    }

    private void StateBalloonStart()
    {
        animator.SetTrigger("Balloon Start");
    }

    private void StateStumbleStart()
    {
        animator.SetTrigger("Stumble Start");
    }

    private void StateStumbleEnd()
    {
        animator.SetTrigger("Stumble End");
    }

    void StateQuickstepLeftStart()
    {
        QuickStepLeft();
    }

    void StateQuickstepRightStart()
    {
        QuickStepRight();
    }

    void QuickStepLeft()
    {
        animator.SetTrigger("Quick Step Left");
    }

    void TrickJumpSuccess()
    {
        animator.ResetTrigger("TrickSuccess");
        animator.SetTrigger("TrickSuccess");
    }
    void QuickStepRight()
    {
        animator.SetTrigger("Quick Step Right");
    }

    void StateFlyStart()
    {
        animator.SetTrigger("Fly");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsBoosting", player.isBoosting);

        animator.SetFloat("Abs Left Stick", player.absoluteLeftStick);

        if (player.stateMachine.currentStateName == "StateGrind" || player.stateMachine.currentStateName == "StateSnowBoard")
        {
            if (Input.GetButtonDown(XboxButton.B))
            {
                squat = true;
            }

            if (Input.GetButtonUp(XboxButton.B))
            {
                squat = false;
            }
        }
        else
        {
            squat = false;
        }

        Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity).normalized;

        //animator.SetFloat("xSpeed", localVelocity.x);
        //animator.SetFloat("zSpeed", localVelocity.z);
        //animator.SetFloat("ySpeed", localVelocity.y);

        //Get absolute Rigidbody velocity
        absSpeed = rigidbody.velocity.magnitude;
        horizontalSpeed = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).magnitude;
        verticalSpeed = rigidbody.velocity.y;

        leftStickDirection = VectorExtension.InputDirection(Input.GetAxis(XboxAxis.LeftStickX), Input.GetAxis(XboxAxis.LeftStickY), transform.up);

        if (player.speedMode == SpeedMode.High)
        {
            tangent = Vector3.Dot(lastTangent, -transform.right) * 10;
        }
        else
        {
            tangent = Vector3.Dot(leftStickDirection, transform.right);
        }

        smoothTangent = Mathf.Lerp(smoothTangent, tangent, 30 * Time.deltaTime);
        //Debug.Log(smoothTangent);

        animator.SetFloat("Tangent", tangent);
        animator.SetFloat("SmoothTangent", smoothTangent);

        lastTangent = Vector3.Lerp(lastTangent, transform.forward, 10 * Time.deltaTime);

        animator.SetBool("Underwater", player.underwater);

        //if(player.aligninput)
        animator.SetFloat(directionNameHash, direction);
        animator.SetBool(groundedNameHash, player.IsGrounded());
        animator.SetFloat(verticalSpeedNameHash, verticalSpeed);
        animator.SetBool(brakeNameHash, player.isBraking);
        animator.SetFloat(horizontalSpeedNameHash, horizontalSpeed);
        animator.SetBool(squatNameHash, squat);
        //animator.SetFloat("BalanceDir", joint.velocity);
        //Reset idle actions
        if (absSpeed > 0.1f)
        {
            idleActionID = 0;
        }

        UpdateAnimator();

        animator.SetFloat("RawDirection", player.driftStartDirection);

        closestEnemy = gameObject.Closest(GameObject.FindGameObjectsWithTag("Enemy"), 0.1f, 10, true);

        if (closestEnemy)
        {

        }

        dir = VectorExtension.Direction(transform, player.hitPoint);

        animator.SetInteger("DamageDirection", (int)dir);
        animator.SetFloat("LeftStickX", Input.GetAxis(XboxAxis.LeftStickX));

    }

    //Update animator parameters
    void UpdateAnimator()
    {
        animator.SetFloat("Abs Speed", absSpeed);
        animator.SetFloat(dotVUpTForwardNameHash, Vector3.Dot(Vector3.up, transform.forward));
        animator.SetFloat(groundDistanceNameHash, player.GetGroundInformation().distance);
        animator.SetBool(lightSpeedDashNameHash, lightSpeedDash);
        animator.SetBool(hommingNameHash, homming);
        //animator.SetBool(underwaterNameHash, underwater);

    }

    void StateAdlibTrickJumpStart()
    {
        animator.SetTrigger("TrickJump");
    }

    void StateChangeToSuperSonicStart()
    {
        animator.SetTrigger("ChangeStart");

    }

    void Reset()
    {
        //animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
    }

    void LateUpdate()
    {
        ChangeMouthSide(transform, leftMouth, rightMouth);

        meshHolder.rotation = Quaternion.identity;
        mesh.transform.rotation = Quaternion.Lerp(mesh.transform.rotation, player.meshRotation, meshDamping * Time.deltaTime);
     //   Debug.Log("LateUpdated");



        //if(player.stateMachine.currentStateName == "StateIdle")
        //{
        //    leftFeet.rotation = Quaternion.FromToRotation(leftFeet.right, player.GetGroundInformation().normal) * leftFeet.rotation;
        //    rightFeet.rotation = Quaternion.FromToRotation(rightFeet.right, player.GetGroundInformation().normal) * rightFeet.rotation;
        //}
    }

    private void ChangeMouthSide(Transform target, Transform leftMouth, Transform rightMouth)
    {
        if (Vector3.Dot(target.right, Camera.main.transform.forward) < 0)
        {
            leftMouth.localScale = Vector3.zero;
            rightMouth.localScale = Vector3.one;
        }
        else
        {
            leftMouth.localScale = Vector3.one;
            rightMouth.localScale = Vector3.zero;
        }
    }

    void StateIdle()
    {

    }

    void StateRollStart()
    {
        animator.SetBool(BallNameHash, true);
        meshHolder.localPosition = new Vector3(0, -0.2f, 0);
    }

    void StateRollEnd()
    {
        animator.SetBool(BallNameHash, false);
        meshHolder.localPosition = Vector3.zero;
    }

    public void StateDriftStart()
    {
        animator.SetTrigger("DriftStart");
    }

    public void StateDriftEnd()
    {
        animator.SetTrigger("DriftEnd");
    }

    void StateAirboostStart()
    {
        animator.SetTrigger("Airboost Start");
    }

    public void IdleLoop()
    {
        idleLoops++;

        //If idle loop time has passed
        if (idleLoops == maxIdleLoops)
        {
            //Reset last time when loop
            idleLoops = 0;

            //Switch to next idle action           
            if (idleActionID < idleActions)
            {
                //Set idle action ID and Trigger the Animator           
                animator.SetInteger(idleActionIDNameHash, idleActionID);
                animator.SetTrigger(doIdleActionNameHash);

                idleActionID++;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AGUA"))
        {
            animator.speed = 0.7f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.speed = 1f;
    }

    void StateSquatKickStart()
    {
        animator.SetTrigger("SquatKick");
        squat = true;
    }

    void StateSlidingStart()
    {
        animator.SetBool("Sliding", true);
    }

    void StateSlidingEnd()
    {
        animator.SetBool("Sliding", false);
    }

    void StateFallDeadStart()
    {
        if (Player.instance.drown)
        {
            animator.SetTrigger("Drown");
        }
    }

    void StateBallStart()
    {
        animator.SetTrigger(ballNameHash);
    }

    void StateSkydiveStart()
    {
        animator.SetTrigger("SkyDiveStart");
    }

    void StateJumpStart()
    {
        animator.SetTrigger(jumpNameHash);
    }

    void StateHurdleStart()
    {
        animator.SetTrigger("Hurdle");
    }

    void StateDoubleJumpStart()
    {
        animator.SetTrigger(doubleJumpNameHash);
    }

    #region Grind
    private void StateGrindStart()
    {
        if (player.stateMachine.lastStateName != "StateGrindJump"
            && player.stateMachine.lastStateName != "StateGrindSwitchLeft"
            && player.stateMachine.lastStateName != "StateGrindSwitchRight")
        {
            animator.SetTrigger("Grind Start");
        }
    }
    private void StateGrindEnd()
    {
        if (player.stateMachine.nextStateName != "StateGrindJump"
            && player.stateMachine.nextStateName != "StateGrindSwitchLeft"
            && player.stateMachine.nextStateName != "StateGrindSwitchRight")
        {
            animator.SetTrigger("Grind End");
        }
    }
    private void StateGrindSwitchLeftStart()
    {
        animator.SetTrigger("Grind Switch Left");
    }
    private void StateGrindSwitchRightStart()
    {
        animator.SetTrigger("Grind Switch Right");
    }
    private void StateGrindJumpStart()
    {
        animator.SetTrigger("Grind Jump");
    }
    private void StateGrindJumpEnd()
    {
        if (player.stateMachine.nextStateName == "StateFall")
        {
            animator.SetTrigger("Grind End");
        }
    }
    #endregion

    //void StateWallJumpStart()
    //{
    //    animator.SetTrigger("WallJumpStart");
    //}

    //void StateWallJumpEnd()
    //{
    //    animator.SetTrigger("WallJump");
    //}

    void StateSquatStart()
    {
        squat = true;
    }
    void StateSquatEnd()
    {
        squat = false;
    }

    void StateCrawlingStart()
    {
        squat = true;
    }

    void StateSpringStart()
    {
        animator.SetTrigger("SpringStart");
    }

    void StateSpringEnd()
    {
        animator.SetTrigger("SpringEnd");
    }

    void StateWideSpringStart()
    {
        spring = true;
    }

    void StateWideSpringEnd()
    {
        spring = false;
    }

    void StatePushingStart()
    {
        animator.SetTrigger("Pushing");
    }

    void StateSelectCanonStart()
    {
        animator.SetTrigger("Select Canon Start");
    }

    void StateSelectCanonEndID(int ID)
    {
        animator.SetInteger("Select Canon ID", ID);
        animator.SetTrigger("Select Canon End");
    }

    void StateLightSpeedDashStart()
    {
        lightSpeedDash = true;
    }

    void StateLightSpeedDashEnd()
    {
        lightSpeedDash = false;
    }

    void StateHomingStart()
    {
        animator.SetInteger("HommingTrickID", 0);
        animator.SetTrigger("Homming Start");
        homming = true;
    }

    void StateHomingEnd()
    {
        homming = false;
    }

    void StateFallStart()
    {
        if (player.stateMachine.currentStateName == "StateGrindSingle")
        {
            animator.SetTrigger("Grind End");
        }
    }

    void StateHomingTrickStart()
    {
        animator.SetInteger("HommingTrickID", Random.Range(1, 6));
        animator.SetTrigger("DoHommingTrick");
    }

    void StateHurtStart()
    {
        animator.SetTrigger("Hurt");
    }

    void StateDieStart()
    {
        animator.SetTrigger(deadNameHash);
    }

    void StateStompStart()
    {
        animator.SetTrigger(stompNameHash);
    }

    void StateIronPole2DStart()
    {
        animator.SetTrigger(ironPole2DStartNameHash);
    }

    void StateIronPole2DEnd()
    {
        animator.SetTrigger(ironPole2DEndNameHash);
    }

    void StatePulleyStart()
    {
        animator.ResetTrigger(pulleyEndNameHash);
        animator.SetTrigger(pulleyStartNameHash);
    }

    void StatePulleyEnd()
    {
        animator.SetTrigger(pulleyEndNameHash);
    }

    void StateUpReelStart()
    {
        animator.SetTrigger(upReelStartNameHash);
    }

    void StateUpReelEnd()
    {
        animator.SetTrigger(upReelEndNameHash);
    }

    void StateDashRingStart()
    {
        animator.SetTrigger(dashRingStartNameHash);
    }

    void StateDashRingEnd()
    {
        animator.SetTrigger(dashRingEndNameHash);
    }

    void StateRainbowRingStart()
    {
        animator.SetTrigger(dashRingStartNameHash);
    }

    void StateRainbowRingEnd()
    {
        animator.SetTrigger(dashRingEndNameHash);
    }

    void StateJumpBoardStart()
    {
        animator.SetTrigger(jumpBoardStartNameHash);
    }

    void StateJumpBoardEnd()
    {
        animator.SetTrigger(jumpBoardEndNameHash);
    }

    void StateCanonStart()
    {
        animator.SetBool(BallNameHash, true);
    }

    void GrindJump()
    {
        animator.SetTrigger("GrindJump");
    }

    void StateBubbleBreathStart()
    {
        animator.SetTrigger("BubbleBreath");
    }

    void StateCanonEnd()
    {
        animator.SetBool(BallNameHash, false);
    }

    void StateWallJumpStart()
    {
        animator.SetTrigger(wallJumpStartNameHash);
    }

    void StateWallJumpEnd()
    {
        if (!player.IsGrounded())
        {
            if (player.contactPoint.point == Vector3.zero)
            {
                animator.SetTrigger("WallLose");
            }
            else
            {
                animator.SetTrigger("WallJumpEnd");
            }

        }

    }

    void StateRopeStart()
    {
        animator.SetTrigger("Rope");
    }

    void StateRopeEnd()
    {
        animator.SetTrigger("Rope");
    }

    void StateStruggleStart()
    {
        print("Start");
        animator.SetTrigger("StruggleStart");
    }

    void StateStruggleEnd()
    {
        print("End");
        animator.SetTrigger("StruggleEnd");
    }

    void StateCarryStart()
    {
        animator.SetTrigger("CarryStart");
    }
    void StateCarryEnd()
    {
        animator.SetTrigger("CarryEnd");
    }

    void StateFloatStart()
    {
        animator.SetTrigger("Float Start");
    }

    void StateFloatEnd()
    {
        animator.SetTrigger("Float End");
    }


    private void StateJumpPoleStart()
    {
        StateSpringStart();
    }

    private void StateJumpPoleEnd()
    {
        StateSpringEnd();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.sharedMaterial)
        {
            if (collision.collider.sharedMaterial.name == "Ice")
            {
                animator.SetTrigger("State Skate Start");
            }
        }
    }
}
