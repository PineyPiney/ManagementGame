using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PineyPiney.Manage
{
    public class NPC : MonoBehaviour
    {
        public Animator animator { get; protected set; }
        public Rigidbody2D body { get; protected set; }
        public SpriteRenderer spriteRenderer { get; protected set; }

        protected bool sitting = false;
        protected bool talking = false;

        protected House house = null;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            body = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            talking = Input.GetKey(KeyCode.E);
            SetAnimatorValues();
            
            spriteRenderer.flipX = body.velocity.x < 0;
        }

        void SetAnimatorValues()
        {
            animator.SetBool("talking", talking);
            animator.SetBool("sitting", sitting);
            animator.SetBool("walking", body.velocity.x != 0f);
        }
    }
}