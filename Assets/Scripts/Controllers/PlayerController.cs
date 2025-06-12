using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float SteerSpeed = 100f;
    [SerializeField] private float MoveSpeed = 5f;

    private float speedModifier = 1.0f;
    private const float MAX_SPEED_MODIFIER = 1.8f;
    private const float MIN_SPEED_MODIFIER = 0.65f;

    private SpriteRenderer spriteRenderer;
    private Color32 defaultColor;
    private readonly Color32 COLOR_GREEN = Color.green;

    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private const float STEER_SPEED_DIRECTION = -1f;

    private float movementInput;
    private float steerInput;

    private bool alreadyPenalized = false;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.defaultColor = this.spriteRenderer.color;
    }

    private void Update()
    {
        this.movementInput = Input.GetAxis(VERTICAL_AXIS);
        this.steerInput = Input.GetAxis(HORIZONTAL_AXIS);
    }

    private void FixedUpdate()
    {
        if (this.movementInput != 0)
        {
            transform.Translate(0, this.movementInput * (this.MoveSpeed * this.speedModifier) * Time.fixedDeltaTime, 0);
        }

        if (this.steerInput != 0)
        {
            transform.Rotate(0, 0, this.steerInput * (this.SteerSpeed * this.speedModifier) * STEER_SPEED_DIRECTION * Time.fixedDeltaTime);
        }
    }

    public void PackagePickup(Component sender, object data)
    {
        Debug.Log($"Pickup: {data}");
        this.spriteRenderer.color = this.COLOR_GREEN;
    }

    public void PackageDelivered(Component sender, object data)
    {
        Debug.Log($"Delivered: {data}");
        this.spriteRenderer.color = this.defaultColor;
    }

    public void SpeedUp(Component sender, object data)
    {
        this.speedModifier = Mathf.Min(this.speedModifier + 0.2f, MAX_SPEED_MODIFIER);
    }

    public void SpeedDown(Component sender, object data)
    {
        this.speedModifier = Mathf.Max(this.speedModifier - 0.1f, MIN_SPEED_MODIFIER);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyPenalized) return;

        if (other.CompareTag("Obstacle"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ReduceScore(10);
                Debug.Log("Engel çarpýldý! -10 puan");
            }
            PenalizeTemporarily();
        }
        else if (other.CompareTag("NoEntry"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ReduceScore(20);
                Debug.Log("Girilmez alana girildi! -20 puan");
            }
            PenalizeTemporarily();
        }
    }

    private void PenalizeTemporarily()
    {
        alreadyPenalized = true;
        Invoke("ResetPenalty", 1.5f); // 1.5 saniyede birden fazla puan kesilmez
    }

    private void ResetPenalty()
    {
        alreadyPenalized = false;
    }
}

