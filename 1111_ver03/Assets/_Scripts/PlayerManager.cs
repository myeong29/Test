using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Urachacha
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields

        [Tooltip("The current Health of our player")]
        public float Gauge = 1f;
        
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        #region Private Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField] private GameObject playerUiPrefab;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float gravity;

        [SerializeField] private bool isGrounded;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundMask;

        // 20211124 추가
        [SerializeField] private bool nearEnemy;
        [SerializeField] private LayerMask enemyMask;
        //

        private Vector3 moveDirection;
        private float directionY;

        private CharacterController controller;
        private Animator anim;
        private Camera cam;

        Vector3 remotePos;
        Quaternion remoteRot;
        Quaternion remoteCamRot;

        #endregion

        #region MonoBehaviour CallBacks

        public void Awake()
        {
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Start() 
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponentInChildren<Animator>();
            
            if (this.playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(this.playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }

        public void Update()
        {
            if (photonView.IsMine)
            {
                //Camera.main.gameObject.SetActive(false);
                cam = GetComponentInChildren<Camera>();
                cam.tag = "MainCamera";
                gameObject.layer = LayerMask.NameToLayer("Player");
                gameObject.name = "Player";

                Move();

                if (this.Gauge <= 0f)
                {
                    //
                }
            }
            else
            {

                gameObject.layer = LayerMask.NameToLayer("Enemy");
                gameObject.name = "Enemy";
                cam = GetComponentInChildren<Camera>();
                cam.enabled = false;

                transform.position = Vector3.Lerp(transform.position, remotePos, 10 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, remoteRot, 10 * Time.deltaTime);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, remoteCamRot, 10 * Time.deltaTime);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
        }

        /// <param name="other">Other.</param>
        public void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
        }

        #if !UNITY_5_4_OR_NEWER
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
    
        #endif

        #endregion

        #region Private Methods
        
        void Move()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
            nearEnemy = Physics.CheckSphere(transform.position, 1.0f, enemyMask);

            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (moveDirection != Vector3.zero)
            {   
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            if (isGrounded)
            {
                anim.SetBool("isGrounded", true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }   
            }

            // 20211124 추가
            if (nearEnemy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    print("Get Enemy");
                }
            }
            //

            directionY -= gravity * Time.deltaTime;

            moveDirection.y = directionY;
            controller.Move(moveDirection * Time.deltaTime);
        }

        void Idle()
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed", 0, -0.1f, Time.deltaTime);
        }

        void Run()
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed", 1, -0.1f, Time.deltaTime);
        }

        void Jump()
        {
            directionY = jumpSpeed;
            anim.SetTrigger("isJump");
        }

        #endregion

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this.Gauge);
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(cam.transform.rotation);
            }
            else
            {
                this.Gauge = (float)stream.ReceiveNext();
                remotePos = (Vector3)stream.ReceiveNext();
                remoteRot = (Quaternion)stream.ReceiveNext();
                remoteCamRot = (Quaternion)stream.ReceiveNext();
            }
        }

        #endregion
    }
}