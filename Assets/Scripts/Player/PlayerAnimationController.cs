using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private float horizontalInput;
    private float verticalInput;
    private bool prevIsIdle;
    private bool prevIsWalking;
    private bool prevIsDamage;
    private bool prevIsDead;

    public static bool isIdle = false;
    public static bool isWalking = false;
    public static bool isDamage = false;
    public static bool isDead = false;
    public string orientation = "";
    private bool showGUI = false;

    void Start()
    {
        prevIsIdle = isIdle;
        prevIsWalking = isWalking;
        prevIsDamage = isDamage;
        prevIsDead = isDead;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for key and controller input to get horizontal and vertical input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        UpdateMovementAnimation();

        // Reset orientation when not walking
        if (!isWalking)
        {
            orientation = "";
        }

        // Check for state changes and update animator accordingly
        if (isIdle != prevIsIdle || isWalking != prevIsWalking || isDamage != prevIsDamage || isDead != prevIsDead)
        {
            if (isIdle != prevIsIdle)
            {
                CheckSingleTrueVariable(isIdle);
                if (isIdle)
                {
                    SetAnimatorIdle();
                }
            }

            if (isWalking != prevIsWalking)
            {
                CheckSingleTrueVariable(isWalking);
                if (isWalking)
                {
                    SetAnimatorWalking();
                }
            }

            if (isDamage != prevIsDamage)
            {
                CheckSingleTrueVariable(isDamage);
            }

            if (isDead != prevIsDead)
            {
                CheckSingleTrueVariable(isDead);
            }
        }

        // Update previous states
        prevIsIdle = isIdle;
        prevIsWalking = isWalking;
        prevIsDamage = isDamage;
        prevIsDead = isDead;

        // Toggle GUI visibility with the BackQuote key
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            showGUI = !showGUI;
        }
    }

    private void UpdateMovementAnimation()
    {
        isIdle = Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f);
        isWalking = !isIdle;

        // Determine direction based on input values (optional)
        if (isWalking)
        {
            if (horizontalInput > 0)
            {
                orientation = "Right";
            }
            else if (horizontalInput < 0)
            {
                orientation = "Left";
            }
            else if (verticalInput > 0)
            {
                orientation = "Up";
            }
            else if (verticalInput < 0)
            {
                orientation = "Down";
            }
        }

        // Update animator parameters accordingly for different movement directions
        if (isIdle)
        {
            SetAnimatorIdle();
        }
        else if (isWalking)
        {
            SetAnimatorWalking();
        }
    }

    private void CheckSingleTrueVariable(bool currentVariable)
    {
        int trueCount = 0;
        if (isIdle) trueCount++;
        if (isWalking) trueCount++;
        if (isDamage) trueCount++;
        if (isDead) trueCount++;
        if (trueCount > 1)
        {
            Debug.Log("OOPS");
        }
    }

    private void SetAnimatorIdle()
    {
        animator.SetBool("isRunRight", false);
        animator.SetBool("isRunLeft", false);
        animator.SetBool("isRunUp", false);
        animator.SetBool("isRunDown", false);
        animator.SetBool("isIdleDown", true);
    }

    private void SetAnimatorWalking()
    {
        animator.SetBool("isRunRight", false);
        animator.SetBool("isRunLeft", false);
        animator.SetBool("isRunDown", false);
        animator.SetBool("isRunUp", false);
        animator.SetBool("isIdleDown", false);

        if (horizontalInput > 0)
        {
            animator.SetBool("isRunRight", true);
            orientation = "Right";
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("isRunLeft", true);
            orientation = "Left";
        }
        else if (verticalInput > 0)
        {
            animator.SetBool("isRunUp", true);
            orientation = "Up";
        }
        else if (verticalInput < 0)
        {
            animator.SetBool("isRunDown", true);
            orientation = "Down";
        }
        else
        {
            Debug.Log("How did you get here");
        }
    }

    private void OnGUI()
    {
        if (showGUI)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleLeft;
            style.normal.textColor = Color.white;
            GUILayout.BeginArea(new Rect(10, Screen.height - 150, 200, 150)); // Adjust the area to accommodate additional labels
            GUILayout.Label("Current Boolean Values:", style);
            GUILayout.Label("isIdle: " + isIdle, style);
            GUILayout.Label("isWalking: " + isWalking, style);
            GUILayout.Label("isDamage: " + isDamage, style);
            GUILayout.Label("isDead: " + isDead, style);

            // Determine orientation based on input values
            GUILayout.Label("Orientation: " + orientation, style);

            GUILayout.EndArea();
        }
    }

    public static void ResetAnimationController()
    {
        isIdle = false;
        isWalking = false;
        isDamage = false;
        isDead = false;
    }
}
