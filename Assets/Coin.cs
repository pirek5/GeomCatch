using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeomCatch
{
    public enum Shape { square, round, star }

    public class Coin : MonoBehaviour
    {
        //dependencies
        private EventManager eventManager;
        private Rigidbody2D rigidBody;

        //set in editor
        [SerializeField] private Shape coinShape;

        private int value;
        private float speed;
        private float fallingSpeed;
        private int currentValue;
        private float leftEdge;
        private float rightEdge;

        public void SetCoin(int value, float speed, float fallingSpeed,float leftEdge, float rightEdge, EventManager eventManager) // to other class
        {
            rigidBody = GetComponent<Rigidbody2D>();
            this.eventManager = eventManager;
            this.value = value;
            this.speed = speed;
            this.fallingSpeed = fallingSpeed;
            this.leftEdge = leftEdge;
            this.rightEdge = rightEdge;
        }

        public void Init()
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.isKinematic = true;
            currentValue = value;
            StartCoroutine(HorizontalMovementCoroutine());
        }

        private IEnumerator HorizontalMovementCoroutine()
        {
            float posX;
            int direction = -1;
            while (currentValue > 0)
            {
                posX = transform.position.x + speed * direction * Time.deltaTime;
                transform.position = new Vector2(posX, transform.position.y);
                if (direction == -1 && transform.position.x < leftEdge)
                {
                    direction = 1;
                    currentValue--;
                }
                else if (direction == 1 && transform.position.x > rightEdge)
                {
                    direction = -1;
                    currentValue--;
                }
                yield return null;
            }
            gameObject.SetActive(false); // to coinManager
        }

        public void FallCoin()
        {
            StopAllCoroutines();
            rigidBody.isKinematic = false;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            print("coin cached");
            eventManager.PostNotification(EventType.coinCached, this);
        }

    }

}


