using UnityEngine;
using System.Collections;

namespace UnityChan
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class UnityChanControlScriptWithRgidBody : MonoBehaviour
    {
        public float animSpeed = 1.5f;
        public float lookSmoother = 3.0f;
        public bool useCurves = true;
        public float useCurvesHeight = 0.5f;

        public float forwardSpeed = 7.0f;
        public float backwardSpeed = 2.0f;
        public float rotateSpeed = 2.0f;
        public float jumpPower = 3.0f;

        private CapsuleCollider col;
        private Rigidbody rb;
        private Vector3 velocity;
        private float orgColHight;
        private Vector3 orgVectColCenter;
        private Animator anim;
        private AnimatorStateInfo currentBaseState;
        private GameObject cameraObject;

        public float mouseSensitivity = 100f; // 마우스 감도
        private float xRotation = 0f; // 위아래 카메라 회전 값
        private Transform cameraTransform; // 카메라의 Transform

        static int idleState = Animator.StringToHash("Base Layer.Idle");
        static int locoState = Animator.StringToHash("Base Layer.Locomotion");
        static int jumpState = Animator.StringToHash("Base Layer.Jump");
        static int restState = Animator.StringToHash("Base Layer.Rest");

        void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();
            cameraObject = GameObject.FindWithTag("MainCamera");
            cameraTransform = cameraObject.transform; // 카메라 Transform 가져오기
            orgColHight = col.height;
            orgVectColCenter = col.center;

            Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 고정
            Cursor.visible = false;
        }

        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            anim.SetFloat("Speed", v);
            anim.SetFloat("Direction", h);
            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            rb.useGravity = true;

            velocity = new Vector3(0, 0, v);
            velocity = transform.TransformDirection(velocity);

            if (v > 0.1)
            {
                velocity *= forwardSpeed;
            }
            else if (v < -0.1)
            {
                velocity *= backwardSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (currentBaseState.fullPathHash == locoState)
                {
                    if (!anim.IsInTransition(0))
                    {
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        anim.SetBool("Jump", true);
                    }
                }
            }

            transform.localPosition += velocity * Time.fixedDeltaTime;
            transform.Rotate(0, h * rotateSpeed, 0);

            HandleMouseLook(); // 마우스 입력 처리
        }

        void HandleMouseLook()
        {
            // 마우스 입력
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 캐릭터 회전 (Y축)
            transform.Rotate(Vector3.up * mouseX);

            // 카메라 회전 (X축)
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        void OnGUI()
        {
            GUI.Box(new Rect(Screen.width - 260, 10, 250, 150), "Interaction");
            GUI.Label(new Rect(Screen.width - 245, 30, 250, 30), "Up/Down Arrow : Go Forward/Go Back");
            GUI.Label(new Rect(Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
            GUI.Label(new Rect(Screen.width - 245, 70, 250, 30), "Mouse Movement: Look Around");
            GUI.Label(new Rect(Screen.width - 245, 90, 250, 30), "Hit Space key while Running : Jump");
            GUI.Label(new Rect(Screen.width - 245, 110, 250, 30), "Hit Space key while Stopping : Rest");
        }

        void resetCollider()
        {
            col.height = orgColHight;
            col.center = orgVectColCenter;
        }
    }
}
