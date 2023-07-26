using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PineyPiney.Manage
{

    public class HouseButton : MonoBehaviour
    {

        public List<GameObject> deactivate;

        public NPC owner;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick()
        {
            deactivate.ForEach(x => x.SetActive(false));
            Builder.INSTANCE.MakeNewRoom(owner);
        }
    }
}