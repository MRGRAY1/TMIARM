using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class NullRefChecker
{
    // Note: instance is not always a MonoBehaviour

    public static void Validate(object instance)
    {
        // Cache all non-static fields both public and private
        FieldInfo[] fields = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (FieldInfo field in fields)
        {
            // If non-serialized or optional, do nothing
            if (!field.IsDefined(typeof(SerializeField), true) || field.IsDefined(typeof(OptionalAttribute), true))
            {
                continue;
            }

            // If null, log an error
            if (field.GetValue(instance) == null)
            {
                // Check if instance is a MonoBehaviour...
                if (instance is MonoBehaviour monoBehaviour)
                {
                    GameObject gameObject = monoBehaviour.gameObject;

                    Debug.LogError($"Missing assignment for field: {field.Name} in component: {instance.GetType().Name} on GameObject: " +
                        $"{monoBehaviour.gameObject}", monoBehaviour.gameObject);
                }
                // ... or a ScriptableObect
                else if (instance is ScriptableObject scriptableObject)
                {
                    Debug.LogError($"Missing assignment for field: {field.Name} on ScriptableObject:  " +
                        $"{scriptableObject.name} ({instance.GetType().Name})");
                }
                else
                {
                    Debug.LogError($"Missing assignment for field: {field.Name} in object: {instance.GetType().Name}");
                }
            }
        }
    }
}

/// <summary>
/// A custom PropertyAttribute to bypass the above Validate check.
/// </summary>
public class OptionalAttribute : PropertyAttribute
{
}
