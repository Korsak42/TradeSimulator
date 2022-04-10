using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettlement 
{
    void ChangeHappy(float _crimeRate);
    void ChangeHealth(float _piety);
    void ChangeInfrastructure(float _infrastructure);
    float GetCrimeRate();
    float GetInfrastucture();
    float GetPiety();
    int GetWorkArea();

}
