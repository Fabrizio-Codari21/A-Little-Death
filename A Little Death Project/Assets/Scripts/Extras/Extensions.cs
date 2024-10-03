using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    // Devuelve una lista de WaitForSeconds que deberia permitir realizar acciones a lo largo del tiempo en una corrutina.
    //
    // Su uso seria algo asi como:
    // foreach (var step in WhileWaiting(2, 0.2f))
    // {
    //   // lo que sea que quieras hacer
    //   yield return step;
    // }
    public static List<WaitForSeconds> WhileWaiting(float seconds, float stepLength = 0)
    {
        var remaining = seconds;
        var step = (stepLength != 0) ? seconds / stepLength : 1;

        List<WaitForSeconds> stepList = new();

        int watchdog = 100000;
        while (remaining > 0)
        {
            if (watchdog <= 0) break;
            stepList.Add(new WaitForSeconds(remaining > step ? step : remaining));
            remaining -= step;
            watchdog--;
        }

        return stepList;
    }

    // Se llama cuando queremos realizar una accion repetida cada X tiempo.
    public static IEnumerator SteppedExecution(float duration, float stepLength, Action ExecuteOnEachStep)
    {
        var stepList = WhileWaiting(duration, stepLength);

        foreach (var step in stepList)
        {
            ExecuteOnEachStep();
            yield return step;
        }
    }

    // Se llama cuando queremos realizar una serie de acciones separadas por un intervalo de X tiempo.
    public static IEnumerator MultiSteppedExecution(float duration, float stepLength, Action[] ListOfSteppedExecutions)
    {
        var stepList = WhileWaiting(duration, stepLength);

        foreach (var step in stepList)
        {
            ListOfSteppedExecutions[stepList.IndexOf(step)]();
            yield return step;
        }
    }

    // Devuelve verdadero tras pasar X tiempo.
    // Deberia permitir esperar cierto tiempo por fuera de una corrutina.
    public static bool Wait(float timeToWait, bool useFixed = false)
    {
        int watchdog = 100000;
        var delta = useFixed ? Time.fixedDeltaTime : Time.deltaTime;
        var t = timeToWait;

        var shouldItRecur = t >= 0 ? t > 0 : t < 0;

        if (watchdog <= 0) { Debug.LogWarning("There was an error while waiting."); return false; }
        watchdog--;

        return shouldItRecur ? Wait(t -= delta, useFixed) : true;
    }

    // Devuelve verdadero hasta que pase X tiempo.
    public static bool Until(float timeToWait, bool useFixed = false) 
    {
        return !Wait(timeToWait, useFixed); 
    }

    // Realiza una accion cuando la condicion de espera previa se cumple.
    public static Action Then(this bool wait, Action exec)
    {
        if (wait) return exec; else return null;
    }
}
