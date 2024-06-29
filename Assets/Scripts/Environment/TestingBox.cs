using System.Collections;
using System.Collections.Generic;
using Freelf.Character.Interfaces;
using TMPro;
using UnityEngine;

namespace Freelf.Environment 
{
    public class TestingBox : MonoBehaviour, IDamageable
    {
        public int health = 100;
        public TMP_Text healthText;
        private int currentHealth;
        public CharacterController controller;
        public float speed = 5f;

        private void Start()
        {
            currentHealth = health;
            healthText.text = currentHealth.ToString();
        }

        private void Update()
        {
            controller.Move(Vector3.forward * Time.deltaTime * speed);
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthText.text = currentHealth.ToString();
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}