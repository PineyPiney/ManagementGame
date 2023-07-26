using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class Builder : MonoBehaviour
    {

        public BuildingItemButton placingButton = null;
        public BuildingItem placing = null;
        public BuildingItem editing = null;

        public BuildingOutline outline = null;

        public bool IsPlacing { get { return placing != null; } }
        public bool IsEditing { get { return editing != null; } }

        public bool isInside = false;

        public float snapAccuracy = 0.2f;

        PlayerInput input;

        ColourSelector selector;
        Canvas buildingMenu;

        // Start is called before the first frame update
        void Start()
        {
            transform.parent.TryGetComponent(out input);
            
            selector = Resources.FindObjectsOfTypeAll<ColourSelector>().First();
            buildingMenu = Resources.FindObjectsOfTypeAll<Canvas>().First(c => c.name == "BuildingMenu");

            AddCallback("PlaceBuilding", OnPlaceBuilding);
            AddCallback("ToggleView", OnToggleView);
            AddCallback("StopPlacing", OnStopPlacing);
            AddCallback("SetColour", OnSetColour);
            AddCallback("DeleteSelected", OnDeleteSelected);
            AddCallback("StopBuilding", OnStopBuilding);

            InputAction drawRoom = input.actions["DrawRoom"];
            drawRoom.started += StartRoom;
            drawRoom.canceled += FinishRoom;
        }

        // Update is called once per frame
        void Update()
        {
            if (placing != null)
            {
                var (p, t) = placing.GetPlacementPosition(snapAccuracy);
                placing.transform.position = p;
                placing.transform.parent = t;
            }
        }

        public void OnPlaceBuilding()
        {
            if (placing != null && selector.gameObject.activeInHierarchy == false)
            {
                BuildingItem item = Instantiate(placing);
                item.transform.SetParent(placing.transform.parent, false);
                item.Place();
            }
        }

        public void OnToggleView()
        {
            isInside = !isInside;
            foreach(House h in Resources.FindObjectsOfTypeAll<House>())
            {
                h.LookIn(isInside);
            }
        }

        public void OnStopPlacing()
        {
            placingButton = null;
            if (placing != null)
            {
                Destroy(placing.gameObject);
                placing = null;
            }

            selector.gameObject.SetActive(false);
        }

        public void OnSetColour()
        {
            if (selector.isActiveAndEnabled) selector.gameObject.SetActive(false);
            else
            {
                if (placing != null)
                {
                    selector.SetEditing(placing);
                    selector.gameObject.SetActive(true);
                }
                else if(editing != null)
                {
                    selector.SetEditing(editing);
                    selector.gameObject.SetActive(true);
                }
            }
        }

        public void OnDeleteSelected()
        {
            if (editing != null)
            {
                Destroy(editing.gameObject);
                editing = null;
            }
        }

        public void OnStopBuilding()
        {
            input.SwitchCurrentActionMap("GamePlay");
            buildingMenu.gameObject.SetActive(false);
            OnStopPlacing();
            editing = null;
            House.ShowAll(false);
            Camera.main.GetComponentInChildren<Grid>().gameObject.SetActive(false);
        }

        public void MakeNewRoom(NPC owner)
        {
            input.SwitchCurrentActionMap("NewRoom");
            outline = Instantiate(Prefabs.BUILDING_OUTLINE);
            outline.transform.position += new Vector3(0, 0, 0.1f);
            outline.owner = owner;
        }

        void StartRoom(InputAction.CallbackContext ctx)
        {
            outline.SetStart();
        }

        void FinishRoom(InputAction.CallbackContext ctx)
        {
            House newHouse = Instantiate(Prefabs.HOUSE);
            newHouse.LoadFrom(outline);

            Destroy(outline.gameObject);
            input.SwitchCurrentActionMap("Building");
        }

        public static Builder INSTANCE
        {
            get
            {
                return FindObjectOfType<Builder>();
            }
        }

        public void AddCallback(string name, Action func)
        {
            input.actions[name].started += (ctx) => func();
        }
    }
}