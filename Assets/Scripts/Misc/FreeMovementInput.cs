using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Misc
{
    public class FreeMovementInput : MonoBehaviour
    {
        [SerializeField] private Vector3 directionCoefficients = Vector3.one;
        [SerializeField] private float baseMoveSpeed = 1;
        [SerializeField] private float runCoefficient = 2;

        private Vector3 lastPointerPos;

        private void Start()
        {
            lastPointerPos = Input.mousePosition;
        }

        private void Update()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var xMove = Input.GetAxis("Horizontal");
            var zMove = Input.GetAxis("Vertical");
            var yMove = Input.GetAxis("Elevate");
            var moveInput = new Vector3(xMove, yMove, zMove);
            Move(moveInput, Input.GetAxis("Run") > 0);
        }

        public void Move(Vector3 moveInput, bool run)
        {
            var speed = directionCoefficients * (baseMoveSpeed * (run ? runCoefficient : 1) * Time.deltaTime);
            speed.Scale(moveInput);
            transform.position += transform.TransformDirection(speed);
        }
    }
}