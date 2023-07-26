using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PineyPiney.Manage
{

    public class NewRoomButton : MonoBehaviour
    {

        public NPC owner;

        Image image;

        // Start is called before the first frame update
        void Start()
        {
            var renderer = owner.spriteRenderer;

            TryGetComponent(out image);
            image.sprite = renderer.sprite;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick()
        {
            Builder.INSTANCE.MakeNewRoom(owner);
        }
    }
}