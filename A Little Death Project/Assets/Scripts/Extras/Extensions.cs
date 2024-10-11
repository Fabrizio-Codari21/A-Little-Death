using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    #region TIME UTILITIES

    // Devuelve una lista de WaitForSeconds que deberia permitir realizar acciones a lo largo del tiempo en una corrutina.
    //
    // Su uso seria algo asi como:
    // foreach (var step in WhileWaiting(2, 0.2f))
    // {
    //   // lo que sea que quieras hacer
    //   yield return step;
    // }
    public static List<WaitForSeconds> BuildTimeSpan(float seconds, float stepLength = 0)
    {
        var remaining = seconds;


        List<WaitForSeconds> stepList = new();

        int watchdog = 100000;
        while (remaining > 0)
        {
            if (watchdog <= 0) break;

            if (stepLength != 0) { stepList.Add(new WaitForSeconds(remaining > stepLength ? stepLength : remaining)); remaining -= stepLength; }
            else { stepList.Add(null); remaining -= Time.fixedUnscaledDeltaTime;}

            //Debug.Log(remaining);
            watchdog--;
        }

        //Debug.Log(stepList.Count);
        return stepList;
    }

    
    // Se llama cuando queremos realizar una accion repetida cada X tiempo.
    public static void SteppedExecution(this MonoBehaviour starter, float duration, float stepLength, Action ExecuteOnEachStep) 
        => starter.StartCoroutine(SteppedExecution(duration, stepLength, ExecuteOnEachStep));
    
    public static IEnumerator SteppedExecution(float duration, float stepLength, Action ExecuteOnEachStep)
    {
        var stepList = BuildTimeSpan(duration, stepLength);

        foreach (var step in stepList)
        {
            ExecuteOnEachStep();
            yield return step;
        }
    }

    // Ejecuta una accion hasta que pase X tiempo.
    public static void ExecuteUntil(this MonoBehaviour starter, float timeLimit, Action Exec)
    {
        starter.StartCoroutine(SteppedExecution(timeLimit * 4, 0, Exec));
    }

    // Se llama cuando queremos realizar una serie de acciones separadas por un intervalo de X tiempo.
    public static void MultiSteppedExecution(this MonoBehaviour starter, float duration, float stepLength, Action[] ListOfSteppedExecutions)
        => starter.StartCoroutine(MultiSteppedExecution(duration, stepLength, ListOfSteppedExecutions));
   
    public static IEnumerator MultiSteppedExecution(float duration, float stepLength, Action[] ListOfSteppedExecutions)
    {
        var stepList = BuildTimeSpan(duration, stepLength);

        foreach (var step in stepList)
        {
            ListOfSteppedExecutions[stepList.IndexOf(step)]();
            yield return step;
        }
    }
 
    // Ejecuta una accion despues de X tiempo.
    public static void WaitAndThen(this MonoBehaviour starter, float timeToWait, Action ExecuteAfterwards)
    {
        starter.StartCoroutine(WaitAndThen(timeToWait, ExecuteAfterwards));

    }

    public static IEnumerator WaitAndThen(float timeToWait, Action ExecuteAfterwards)
    {
        yield return new WaitForSeconds(timeToWait);
        ExecuteAfterwards();
    }

    // Ejecuta hasta que una condicion se cumpla.
    public static void ExecuteUntilTrue(this MonoBehaviour starter, Func<bool> condition, Action Exec)
        => starter.StartCoroutine(ExecuteByCondition(condition, Exec, false));

    // Ejecuta despues de que una condicion se haya cumplido.
    public static void ExecuteAfterTrue(this MonoBehaviour starter, Func<bool> condition, Action Exec)
        => starter.StartCoroutine(ExecuteByCondition(condition, Exec, true));

    // Si la condicion se cumple dentro del tiempo estipulado, se realiza la accion.
    public static void QuickTimeEvent(this MonoBehaviour starter, float timeLimit, Func<bool> doneWithinTime, Action Exec)
        => starter.StartCoroutine(ExecuteByCondition(doneWithinTime, Exec, true, timeLimit));

    public static IEnumerator ExecuteByCondition(Func<bool> condition, Action Exec, bool requireCondition = true, float span = 999)
    {
        var timeSpan = BuildTimeSpan(span);

        if (requireCondition) foreach (var frame in timeSpan)
            {
                if (!condition()) yield return frame;
                else
                {
                    Exec();
                    break;
                }

            }
        else foreach (var frame in timeSpan)
            {
                if (condition()) break;
                else
                {
                    Exec();
                    yield return frame;

                }
            }
    }


    #endregion
}

