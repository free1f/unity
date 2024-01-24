using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public HealthStat HealthStat;
    private int _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = HealthStat.MaxValue;
        HealthStat.Change(_currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)) {
            _currentHealth -= 10;
            HealthStat.Change(_currentHealth);
        }
        if(Input.GetKeyDown(KeyCode.J)) {
            _currentHealth += 10;
            HealthStat.Change(_currentHealth);
        }
    }
}
