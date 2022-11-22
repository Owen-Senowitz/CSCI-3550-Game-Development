using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth;


    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        
    }
   public void AddHealth(float _value)
    {
        
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    void Update()
    {
        if(currentHealth == 0)
        {
            SceneManager.LoadScene("project");
        }
    }
    
}
