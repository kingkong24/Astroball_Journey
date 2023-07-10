    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    public class AIMovement : MonoBehaviour
    {
        [Header("타겟")]
        [SerializeField] Transform transform_target;

        [Header("골프채")]
        [SerializeField] Transform transform_golfClub;

        [Header("속도 및 힘")]
        [SerializeField] float speed = 2.0f;
        [SerializeField] float angularSpeed = 60.0f;
        [SerializeField] float force = 500.0f;
        [SerializeField] float rotateSpeed = 360.0f;

        [Header("소리")]
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip audioClip;

        [Header("확인용")]
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] float timer;
        [SerializeField] Vector3 rotateAxis;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource.volume = GameManager.instance.MasterVolumes * GameManager.instance.SFXVolumes;
            TrackingStop();
            StartCoroutine(TurningCrazy());
        }

        private void Update()
        {
            navMeshAgent.SetDestination(transform_target.position);
        }

        /// <summary>
        /// 타겟을 쫓습니다.
        /// </summary>
        public void TrackingStart()
        {
            navMeshAgent.speed = speed;
            navMeshAgent.angularSpeed = angularSpeed;
        }

        /// <summary>
        /// 타겟을 쫓기를 멈춥니다.
        /// </summary>
        public void TrackingStop()
        {
            navMeshAgent.speed = 0;
            navMeshAgent.angularSpeed = 0;
        }

        IEnumerator TurningCrazy()
        {
            while(true)
            {
                timer += Time.deltaTime;
                if(timer > 0.5f)
                {
                    rotateAxis = Random.insideUnitSphere.normalized;
                    transform_golfClub.rotation = Quaternion.FromToRotation(Vector3.right, rotateAxis);
                }

                transform_golfClub.Rotate(Vector3.right, speed * Time.deltaTime);

                yield return null;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Ball"))
            {
                audioSource.PlayOneShot(audioClip);
                Vector3 forceDirection = new(-col.transform.position.x, 100f, -col.transform.position.z);
                forceDirection = forceDirection.normalized;
                col.attachedRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
                TrackingStop();
            }
        }

    }
