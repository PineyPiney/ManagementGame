using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PineyPiney.Manage
{

    public class BuildingMenu : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            foreach(var menu in GetComponentsInChildren<BuildingButtonContainer>())
            {
                menu.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}