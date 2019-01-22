using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private float m_StepInterval;
        [SerializeField] public Animator m_Animator;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private float m_StepCycle;
        private float m_NextStep;
        private AudioSource m_AudioSource;
        public Vector3 m_direccion;
        private PlayerStats m_playerStats;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_AudioSource = GetComponent<AudioSource>();
            m_playerStats = GetComponent<PlayerStats>();
        }

        private void LateUpdate()
        {
            Vector3 pos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            transform.position = pos;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.y = 0.0f;
            m_MoveDir.z = desiredMove.z*speed;
            

            //m_CharacterController.SimpleMove(m_MoveDir * Time.fixedDeltaTime);
            

            m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            //ProgressStepCycle(speed);
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + speed)*Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }

        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = 0;
            float vertical = 0;
            horizontal = CrossPlatformInputManager.GetAxis("Horizontal" + this.m_playerStats.id);
            horizontal = Math.Sign(horizontal);

            vertical = CrossPlatformInputManager.GetAxis("Vertical" + this.m_playerStats.id);
            vertical = Math.Sign(vertical);


            // set the desired speed to be walking or running
            speed = m_playerStats.speed/10;
            m_Input = new Vector2(horizontal, vertical);

            if (m_Input.sqrMagnitude > 0)
            {
                // normalize input if it exceeds 1 in combined length:
                if (m_Input.sqrMagnitude > 1)
                {
                    m_Input.Normalize();
                }
                m_Animator.SetFloat("moveX", Math.Sign(horizontal));
                m_Animator.SetFloat("moveY", Math.Sign(vertical));
                m_direccion = new Vector3(Math.Sign(horizontal), 0f, Math.Sign(vertical));

            }
            m_Animator.SetBool("isWalking", m_Input.sqrMagnitude > 0);
        }

    }
}
