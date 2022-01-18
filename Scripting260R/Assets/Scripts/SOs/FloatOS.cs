using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatOS : ScriptableObject
{
    public int value;

    public void ChangeValue(int number)
    {
        value += number;
    }
}
