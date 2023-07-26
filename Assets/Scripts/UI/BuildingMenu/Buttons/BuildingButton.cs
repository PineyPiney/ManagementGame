using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PineyPiney.Manage
{

    public class BuildingButton : BuildingMenuButton
    {

        GameObject child;

        // Start is called before the first frame update
        void Start()
        {
            child = GetComponentInChildren<BuildingButtonContainer>(true).gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Activate()
        {
            child.SetActive(true);
        }

        void Deactivate()
        {
            child.SetActive(false);
        }

        public void OnClick()
        {
            Builder.INSTANCE.OnStopPlacing();

            if (child.activeInHierarchy)
            {
                Deactivate();
            }
            else
            {
                foreach(var i in transform.parent.GetComponentsInChildren<BuildingButton>())
                {
                    i.Deactivate();
                }
                Activate();
            }
        }
    }
}