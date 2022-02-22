using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DanielLochner.Assets.SimpleScrollSnap {
    public class ScrollController : MonoBehaviour
    {
        private SimpleScrollSnap sss;
        public GameObject header;

        //header sprites
        public Sprite Water;
        public Sprite WaterDecor;
        public Sprite Foliage;
        public Sprite FoliageDecor;
        public Sprite Other;

        public Sprite Basic;
        public Sprite Tree;
        public Sprite Dart;

        public SimpleScrollSnap catalogScroll;
        // Start is called before the first frame update
        void Start()
        {

        }

        public void changeHeader(string scrollType) {
            if(scrollType == "Catalog") {
                sss = catalogScroll;
                if(sss.CurrentPanel == 0 || sss.CurrentPanel == 1 || sss.CurrentPanel == 2) {
                    header.GetComponent<Image> ().sprite = Basic;
                } else if(sss.CurrentPanel == 3 || sss.CurrentPanel == 4) {
                    header.GetComponent<Image> ().sprite = Tree;
                } else if(sss.CurrentPanel == 5 || sss.CurrentPanel == 6) {
                    header.GetComponent<Image> ().sprite = Dart;
                }
            } else {
                if(scrollType == "Store")
                  sss = GameObject.Find("Store").transform.Find("ScrollList").GetComponent<SimpleScrollSnap>();
                else if(scrollType == "Inventory")
                  sss = GameObject.Find("Inventory").transform.Find("ScrollList").GetComponent<SimpleScrollSnap>();
                if(sss.CurrentPanel == 0 || sss.CurrentPanel == 1) {
                    header.GetComponent<Image> ().sprite = Water;
                    header.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2(300, 100);
                } else if(sss.CurrentPanel == 2 || sss.CurrentPanel == 3) {
                    header.GetComponent<Image> ().sprite = WaterDecor;
                    header.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2(500, 100);
                } else if(sss.CurrentPanel == 4) {
                    header.GetComponent<Image> ().sprite = Foliage;
                    header.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2(300, 100);
                }  else if(sss.CurrentPanel == 5 || sss.CurrentPanel == 6) {
                    header.GetComponent<Image> ().sprite = FoliageDecor;
                    header.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2(500, 100);
                } else if(sss.CurrentPanel == 7 || sss.CurrentPanel == 8) {
                    header.GetComponent<Image> ().sprite = Other;
                    header.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2(300, 100);
                }
            }
        }
    }
}
