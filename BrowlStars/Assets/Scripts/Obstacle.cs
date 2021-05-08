using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   public class Obstacle : MonoBehaviour
    {
        public int damage;
        public float range;
        private void Update()
        {
            Damaging();
        }
        void Damaging()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Player"));
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Gamer>().ReceiveDamage(damage);
        }
        enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Patroler>().ReceiveDamage(damage);
        }
        }
    }

