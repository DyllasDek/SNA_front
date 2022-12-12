using System;
using DefaultNamespace;
using UnityEngine;

public class GameChangeState : MonoBehaviour
{
        [SerializeField] private GameObject LoseCanvas;
        [SerializeField] private GameObject HomeCanvas;
        [SerializeField] private GameObject TopScoreCanvas;
        [SerializeField] private GameObject Level;
        [SerializeField] private Transform GarbageTransform;
        
        
        
        
        
        private void Start()
        {

                Level.SetActive(true);
                Health.Instance.Died += LoadScoreScene;
                Level.SetActive(false);
        }

        public void LoadScoreScene()
        {
                Level.SetActive(false);
                HomeCanvas.SetActive(false);
                LoseCanvas.SetActive(true);
                TopScoreCanvas.SetActive(false);
                ClearGarbage();
                Database.Instance.SendPlayerProfileToServer();
        }

        public void LoadTopScoreScene()
        {
                Level.SetActive(false);
                HomeCanvas.SetActive(false);
                LoseCanvas.SetActive(false);
                TopScoreCanvas.SetActive(true);
        }
        private void ClearGarbage()
        {
                GarbageTransform.gameObject.SetActive(false);
        }

        public void LoadHomeScene()
        {
                
                HomeCanvas.SetActive(true);
                LoseCanvas.SetActive(false);
                Level.SetActive(false);
                TopScoreCanvas.SetActive(false);
                ClearGarbage();
        }

        public void LoadGameScene()
        {
                HomeCanvas.SetActive(false);
                LoseCanvas.SetActive(false);
                Level.SetActive(true);
                TopScoreCanvas.SetActive(false);
                GarbageTransform.gameObject.SetActive(true);
        }
}