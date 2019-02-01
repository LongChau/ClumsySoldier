using UnityEngine;
// Contain declaration for Conditional attribute
using System.Diagnostics;
// Prevent Type conflict with System.Diagnostics.Log
using Debug = UnityEngine.Debug;

/// <summary>
/// Debug log
/// </summary>
public class Log
{
    /// <summary>
    /// Default debug message
    /// </summary>
    /// <param name="message"></param>
    [Conditional("ENABLE_LOG")]
    public static void Info(object message)
    {
#if UNITY_EDITOR
        Debug.Log("Info : " + message);
#endif
    }

    [Conditional("ENABLE_LOG")]
    public static void Warning(object message)
    {
#if UNITY_EDITOR
        Debug.LogWarning("Warning : " + message);
#endif
    }

    [Conditional("ENABLE_LOG")]
    public static void Warning(object message, Object context)
    {
#if UNITY_EDITOR
        Debug.LogWarning("Warning : " + message, context);
#endif
    }

    [Conditional("ENABLE_LOG")]
    public static void Error(object message)
    {
#if UNITY_EDITOR
        Debug.LogError("Error : " + message);
#endif
    }
}
