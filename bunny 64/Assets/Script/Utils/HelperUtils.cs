using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtils 
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 GetScreenLowerBoundWorldPosition()
    {
        Vector3 vec = GetWorldPositionWithZ(Vector3.zero, Camera.main);
        vec.z = 0;
        return vec;
    }

    public static Vector3 GetScreenUpperBoundWorldPosition()
    {
        Vector3 vec = GetWorldPositionWithZ(new Vector3(Screen.width, Screen.height, 0), Camera.main);
        vec.z = 0;
        return vec;
    }


    #region Validation
    public static bool ValidateCheckEmptyString(Object thisObject, string fileName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.Log(fileName + " is empty and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    public static bool ValidateCheckNullValue(Object thisObject, string fileName, UnityEngine.Object objectToCheck)
    {
        if (objectToCheck == null)
        {
            Debug.Log(fileName + " is null " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fileName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        if (enumerableObjectToCheck == null)
        {
            Debug.Log(fileName + " is null " + thisObject.name.ToString());
            return true;
        }

        foreach (var item in enumerableObjectToCheck)
        {
            if (item == null)
            {
                Debug.Log(fileName + " has null value in object " + thisObject.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if (count == 0)
        {
            Debug.Log(fileName + " has no values in object " + thisObject.name.ToString());
            error = true;
        }

        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObject, string fileName, int valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                error = true;
            }
        }

        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObject, string fileName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                error = true;
            }
        }

        return error;
    }

    public static bool ValidateCheckPositiveRange(Object thisObject, string fieldNameMinimum, float valueToCheckMinimum, string fieldNameMaximum, float valueToCheckMaximum, bool isZeroAllowed)
    {
        bool error = false;
        if (valueToCheckMinimum > valueToCheckMaximum)
        {
            Debug.Log(fieldNameMinimum + " must be less or equal to " + fieldNameMaximum + " in object " + thisObject.name.ToString());
            error = true;
        }

        if (ValidateCheckPositiveValue(thisObject, fieldNameMinimum, valueToCheckMinimum, isZeroAllowed)) error = true;
        if (ValidateCheckPositiveValue(thisObject, fieldNameMaximum, valueToCheckMaximum, isZeroAllowed)) error = true;

        return error;
    }
    #endregion Validation
}
