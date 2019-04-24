using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Zenject;


namespace GeomCatch
{
    public class CoinsManager : MonoBehaviour, IListener
    {
        [Inject] EventManager eventManager;

        [SerializeField] private Coin squareCoin;
        [SerializeField] private Coin roundCoin;
        [SerializeField] private Coin starCoin;

        [SerializeField] private float fallingSpeed;
        [SerializeField] private int value;
        [SerializeField] private float speed;
        [SerializeField] private float buffer;
        [SerializeField] private float height;

        private List<Coin> coins = new List<Coin>();
        private Coin currentCoin;

        private float leftEdge;
        private float rightEdge;


        // Start is called before the first frame update
        void Start()
        {
            eventManager.AddListener(EventType.playerTap, this);
            eventManager.AddListener(EventType.coinCached, this);
            SetEdges();
            InitializeCoins();
            NewCoin();
        }

        private void SetEdges()
        {
            Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
            leftEdge = leftmost.x + buffer;
            rightEdge = rightmost.x - buffer;
        }

        private void InitializeCoins()
        {
            System.Type myType = this.GetType();
            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = myType.GetFields(myFlags);

            foreach (FieldInfo field in fields)
            {
                Coin prefab = field.GetValue(this) as Coin;
                if (prefab != null)
                {
                    var coinInstance = Instantiate(prefab, this.transform);
                    coins.Add(coinInstance);
                    coinInstance.SetCoin(value, speed, fallingSpeed, leftEdge, rightEdge, eventManager);
                    coinInstance.gameObject.SetActive(false);
                }
            }
        }

        private void NewCoin()
        {
            if (coins.Count == 0) { return; }

            int coinNumber = Random.Range(0, coins.Count);
            if(coins[coinNumber] == currentCoin)
            {
                NewCoin();
                return;
            }
            currentCoin = coins[coinNumber];
            currentCoin.gameObject.SetActive(true);
            currentCoin.transform.position = new Vector2(rightEdge + buffer, height);
            currentCoin.Init();
        }

        public void OnEvent(EventType eventType, Component sender, Object param = null)
        {
            if (eventType == EventType.playerTap && currentCoin != null)
            {
                currentCoin.FallCoin();
            }
            else if(eventType == EventType.coinCached)
            {
                NewCoin();
            }
        }
    } 
}

