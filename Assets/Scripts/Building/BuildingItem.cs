using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PineyPiney.Util;

namespace PineyPiney.Manage
{

    abstract public class BuildingItem : MonoBehaviour, Buildable
    {

        public Color32 primary;
        public Color32 secondary;
        public Color32 tertiary;

        Renderer sr;

        public bool Outline 
        {
            get { return sr.material.GetFloat("_Outlined") > 0; } 
            set
            {
                sr.material.SetFloat("_Outlined", value ? 1 : 0);
            } 
        }

        // Start is called before the first frame update
        virtual protected void Start()
        {
            TryGetComponent(out sr);
            sr.material.SetColor("_Primary", primary);
            sr.material.SetColor("_Secondary", secondary);
            sr.material.SetColor("_Tertiary", tertiary);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetTexture(Texture2D texture)
        {
            sr.material.mainTexture = texture;
        }

        public void SetColour(int index, Color32 colour)
        {
            switch (index)
            {
                case 1: SetPrimary(colour); break;
                case 2: SetSecondary(colour); break;
                case 3: SetTertiary(colour); break;
            }
        }

        public Color32 GetColour(int index)
        {
            switch (index)
            {
                case 1: return primary;
                case 2: return secondary;
                case 3: return tertiary;
                default: return default;
            }
        }

        public void SetPrimary(Color32 colour)
        {
            primary = colour;
            sr.material.SetColor("_Primary", colour);
        }

        public void SetSecondary(Color32 colour)
        {
            secondary = colour;
            sr.material.SetColor("_Secondary", colour);
        }

        public void SetTertiary(Color32 colour)
        {
            tertiary = colour;
            sr.material.SetColor("_Tertiary", colour);
        }

        public void LoadFromButton(BuildingItemButton button)
        {
            primary = button.pri;
            secondary = button.sec;
            tertiary = button.ter;
        }

        public virtual (Vector3, Transform) GetPlacementPosition(float snapAccuracy, Vector2 offset = new())
        {
            Vector2 p = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            House nearestHouse = House.GetNearest(p);
            Transform parent = nearestHouse == null ? null : nearestHouse.transform;
            return (new(p.x.RoundToNearest(snapAccuracy), p.y.RoundToNearest(snapAccuracy)), parent);
        }

        public virtual void Place()
        {
            TryGetComponent(out Surface s);
            if (s != null) s.active = true;
        }

        virtual protected void OnMouseDown()
        {
            if (!Builder.INSTANCE.IsPlacing)
            {
                if(Builder.INSTANCE.IsEditing) Builder.INSTANCE.editing.Outline = false;
                if(Player.INSTANCE.IsBuilding()) Builder.INSTANCE.editing = this;
                Outline = true;
            }
        }

        public static Mesh CreateMesh()
        {
            Mesh mesh = new Mesh()
            {
                vertices = new Vector3[4]
                {
                    new Vector3(0, 0),
                    new Vector3(0, 1),
                    new Vector3(1, 1),
                    new Vector3(1, 0)
                },
                triangles = new int[6] {0, 1, 2, 1, 2, 3},
                uv = new Vector2[4]
                {
                    new(-0.2f, -0.2f),
                    new(-0.2f, -0.2f),
                    new(-0.2f, -0.2f),
                    new(-0.2f, -0.2f)
                }
            };
            return mesh;
        }
    }
}