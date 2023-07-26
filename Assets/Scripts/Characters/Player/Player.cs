using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class Player : MonoBehaviour
    {

        public float maxSpeed = 50f;
        public float acc = 200f;
        public float jump = 100f;
        [Range(-1f, 1f)]
        public float cameraOffset = 0.5f;

        public bool startBuilding = false;

        public Vector4 cameraBounds = new();

        Rigidbody2D body;
        new Collider2D collider;
        PlayerInput input;
        public PlayerRenderer display;

        Canvas buildingMenu;

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out body);
            TryGetComponent(out collider);
            TryGetComponent(out input);
            display = GetComponentInChildren<PlayerRenderer>();

            buildingMenu = Resources.FindObjectsOfTypeAll<Canvas>().First(c => c.name == "BuildingMenu");

            // Ignore collisions on the character layer
            Physics2D.IgnoreLayerCollision(3, 3);

            if (startBuilding)
            {
                OnStartBuilding();
            }
        }

        // Update is called once per frame
        void Update()
        {
            ProcessInput();
            if (input.currentActionMap.name == "GamePlay") SetCameraPosition(transform.position + new Vector3(0, CAMERA.orthographicSize * cameraOffset, 0));
        }

        void ProcessInput()
        {
            switch (input.currentActionMap.name)
            {
                case "GamePlay":
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        display.WriteValues();
                        SceneUtil.Open(Scenes.CHARACTER_CUSTOMISATION);
                        return;
                    }
                    float move = GetAction("Move").ReadValue<float>() * (GetAction("Sprint").IsPressed() ? 2 : 1);
                    if (Mathf.Abs(body.velocity.x) > Mathf.Abs(maxSpeed * move))
                    {
                        SlowPlayer();
                    }
                    else
                    {
                        body.velocity = new Vector2(Maths.AbsMin(body.velocity.x + (Time.deltaTime * acc * move), maxSpeed * move), body.velocity.y);
                    }
                    break;

                case "Building":
                    Vector2 m = maxSpeed * Time.deltaTime * GetAction("MoveCamera").ReadValue<Vector2>();
                    MoveCamera(m);
                    if(body.velocity.x != 0) SlowPlayer();
                    break;
            }
        }

        public void OnStartBuilding()
        {
            input.SwitchCurrentActionMap("Building");
            buildingMenu.gameObject.SetActive(true);
            House.ShowAll(true);
            Camera.main.GetComponentInChildren<Grid>(true).gameObject.SetActive(true);
        }

        public void OnJump()
        {
            if(IsGrounded()) body.AddForce(new(0, jump));
        }

        bool IsGrounded()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];
            return collider.Raycast(Vector2.down, hits, collider.bounds.extents.y + 0.1f) > 0;
        }

        public void SlowPlayer()
        {
            body.velocity = new Vector2(body.velocity.x - Maths.AbsMin(Time.deltaTime * acc * body.velocity.x.Sign(), body.velocity.x), body.velocity.y);
        }

        public bool IsBuilding()
        {
            return input.currentActionMap.name == "Building";
        }

        public void MoveCamera(Vector2 move)
        {
            SetCameraPosition(CAMERA.transform.position + new Vector3(move.x, move.y));
        }

        public void SetCameraPosition(Vector2 pos)
        {
            var (min, max) = GetCameraBounds();
            CAMERA.transform.position = new Vector3(Mathf.Clamp(pos.x, min.x, max.x), Mathf.Clamp(pos.y, min.y, max.y), CAMERA.transform.position.z);
        }

        public Tuple<Vector2, Vector2> GetCameraBounds()
        {
            Vector2 extents = new(CAMERA.orthographicSize * CAMERA.aspect, CAMERA.orthographicSize);
            return Tuple.Create(new Vector2(cameraBounds.x + extents.x, cameraBounds.y + extents.y), new Vector2(cameraBounds.z - extents.x, cameraBounds.w - extents.y));
        }

        public InputAction GetAction(string name)
        {
            return input.actions[name];
        }

        public static Camera CAMERA
        {
            get
            {
                return Camera.main;
            }
        }

        public static Player INSTANCE
        {
            get
            {
                return FindObjectOfType<Player>();
            }
        }
    }
}