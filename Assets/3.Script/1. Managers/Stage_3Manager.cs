using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_3Manager : MonoBehaviour
{
    [Header("설정")]
    [Range(0, 1.0f)]
    [SerializeField] float dragPlayer;
    [Range(0, 1.0f)]
    [SerializeField] float AngularDragPlayer;

    [Header("확인용")]
    [SerializeField] Rigidbody rigidbody_player;

    private void Start()
    {
        GameObject ballObject = GameObject.FindWithTag("Ball");
        if (ballObject != null)
        {
            rigidbody_player = ballObject.GetComponent<Rigidbody>();
            rigidbody_player.drag = dragPlayer;
            rigidbody_player.angularDrag = AngularDragPlayer;
        }
    }
}
