using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class Ingredient : MonoBehaviour
    {
        private bool IsBurning = false;
        private bool IsSliced = false;
        private bool IsCooked;
        private bool IsBurned;
        public float BurnResistTime;
        public float CookTime;
        private float OngoingBurningTime = 0f;
        public Sprite CookedSprite;
        public Sprite BurntSprite;

        void Update()
        {
            /*if (IsBurning && IsBurned != true)
            {
                IngredientBurn();
            }*/
        }

        public void IngredientBurn()
        {
            /*OngoingBurningTime += Time.deltaTime;

            if(OngoingBurningTime == BurnResistTime)
            {
                IsBurned = true;
                IsBurning = false;
                IsCooked = false;
                <code that changes the image to burnt sprite>
            }
            
             if(OngoingBurningTime == CookTime)
            {
                IsCooked = true;                
                <code that changes the image to cooked sprite>
               
            }*/
        }

        public void Slice()
        {

        }

        public void PutOnStickSlot()
        {
            
        }

        /*void OnCollisionEnter2D(Collider2D other)
        {
            if (IsSliced && IsBurned != true && other.CompareTag("Shish"))
            {
               PutOnStickSlot();
            }
            if (IsSliced != true && IsBurned != true && other.CompareTag("Knife"))
            {
               Slice();
            }
        }*/
    }
}
