using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PineyPiney.Util;

namespace PineyPiney.Manage
{

    public class House : Building
    {

        public NPC owner;

        public GameObject background { get { return transform.Find("Background").gameObject; } }
        public GameObject graphic { get { return background.transform.Find("OwnerGraphic").gameObject; } }
        public GameObject outside { get { return transform.Find("Outside").gameObject; } }
        public GameObject inside { get { return transform.Find("Inside").gameObject; } }

        public Surface floor { get { return outside.GetComponent<Surface>(); } }

        // Start is called before the first frame update
        override protected void Start()
        {
            LookIn(Builder.INSTANCE.isInside);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadFrom(BuildingOutline outline)
        {
            owner = outline.owner;

            transform.localScale = outline.transform.localScale;
            transform.position = outline.transform.position;

            background.TryGetComponent(out Renderer m);
            m.material.color = new(0.56f, 0f, 0f, 0.15f);

            graphic.TryGetComponent(out SpriteRenderer renderer);
            renderer.sprite = outline.owner.spriteRenderer.sprite;
            Vector2 scale = (new Vector2(1f, 1f) / transform.localScale) * Mathf.Min(transform.localScale.x / renderer.sprite.bounds.size.x, transform.localScale.y / renderer.sprite.bounds.size.y);
            graphic.transform.localScale = scale;

            var wall = outside.GetComponentInChildren<Wall>();
            wall.SetSize(transform.localScale / 4);

            wall = inside.GetComponentInChildren<Wall>();
            wall.SetSize(transform.localScale / 4);

            inside.GetComponent<Surface>().SetValues(-0.5f, 0.5f, -0.5f, true);
            outside.GetComponent<Surface>().SetValues(-0.5f, 0.5f, -0.5f, true);
        }

        public void LookIn(bool b)
        {
            inside.SetActive(b);
            outside.SetActive(!b);
        }

        public static void ShowAll(bool show)
        {
            foreach (var h in Resources.FindObjectsOfTypeAll<House>())
            {
                GameObject g;
                try { g = h.background; }
                catch { continue;  }
                if(g != null) g.SetActive(show);
            }
        }

        public static House GetNearest(Vector2 p)
        {
            House[] houses = FindObjectsOfType<House>();
            if(houses.Length == 0) return null;
            var (nearest, distance) = (houses[0], float.MaxValue);
            foreach(House house in houses)
            {
                float dx = p.x - Mathf.Clamp(p.x, house.transform.position.x - (house.transform.lossyScale.x / 2), house.transform.position.x + (house.transform.lossyScale.x / 2));
                float dy = p.y - Mathf.Clamp(p.y, house.transform.position.y - (house.transform.lossyScale.y / 2), house.transform.position.y + (house.transform.lossyScale.y / 2));

                if (dx == 0 && dy == 0) return house;

                float d = Mathf.Sqrt(dx * dx + dy * dy);
                if(d < distance) (nearest, distance) = (house, d);
            }
            return nearest;
        }
    }
}