using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

        int watchdog = 1000000;
        while (remaining > 0)
        {
            if (watchdog <= 0) break;

            if (stepLength != 0) 
            { 
                stepList.Add(new WaitForSeconds(remaining > stepLength ? stepLength : remaining)); 
                remaining -= stepLength; 
            }
            else 
            { 
                stepList.Add(null); 
                remaining -= Time.fixedUnscaledDeltaTime;
            }

            watchdog--;
        }

        return stepList;
    }

    
    // Se llama cuando queremos realizar una accion repetida cada X tiempo.
    public static void SteppedExecution(this MonoBehaviour starter, float duration, float stepLength, Action ExecuteOnEachStep) 
        => starter.StartCoroutine(SteppedExecution(duration, stepLength, ExecuteOnEachStep));

    // Ejecuta una accion hasta que pase X tiempo.
    public static void ExecuteUntil(this MonoBehaviour starter, float timeLimit, Action Exec)
        => starter.StartCoroutine(SteppedExecution(timeLimit * 4, 0, Exec));

    public static IEnumerator SteppedExecution(float duration, float stepLength, Action ExecuteOnEachStep)
    {
        var stepList = BuildTimeSpan(duration, stepLength);

        foreach (var step in stepList)
        {
            ExecuteOnEachStep();
            yield return step;
        }
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
    public static void WaitAndThen(this MonoBehaviour starter, float timeToWait, Action ExecuteAfterwards, Func<bool> cancelCondition = default)
    {
        starter.StartCoroutine(ExecuteAfter(starter, timeToWait, ExecuteAfterwards, false, cancelCondition));

    }

    // Ejecuta una accion cada X segundos (puede cancelarse si se le da una condicion).
    public static void ExecuteLooping(this MonoBehaviour starter, float timeUntilLoop, Action Exec, Func<bool> cancelCondition = default)
    {
        Exec();
        if(cancelCondition == default) starter.StartCoroutine(ExecuteAfter(starter, timeUntilLoop, Exec, true));
        else starter.StartCoroutine(ExecuteAfter(starter, timeUntilLoop, Exec, true, cancelCondition));
    }

    public static IEnumerator ExecuteAfter(MonoBehaviour starter, float timeToWait, Action ExecuteAfterwards, bool loop = false, Func<bool> cancelCondition = default)
    {
        if (cancelCondition == default) cancelCondition = () => false;
        
        yield return new WaitForSeconds(timeToWait);

        if (!cancelCondition())
        {
            if (loop) ExecuteLooping(starter, timeToWait, ExecuteAfterwards, cancelCondition);
            else ExecuteAfterwards();
        }
    }

    // Ejecuta hasta que una condicion se cumpla.
    public static IEnumerator ExecuteUntilTrue(this MonoBehaviour starter, Func<bool> condition, Action Exec)
    {
        IEnumerator exec = ExecuteByCondition(starter, condition, Exec, false);
        starter.StartCoroutine(exec);
        return exec;
    }
        

    // Ejecuta despues de que una condicion se haya cumplido.
    public static void ExecuteAfterTrue(this MonoBehaviour starter, Func<bool> condition, Action Exec)
        => starter.StartCoroutine(ExecuteByCondition(starter, condition, Exec, true));

    // Si la condicion se cumple dentro del tiempo estipulado, se realiza la accion.
    public static void QuickTimeEvent(this MonoBehaviour starter, float timeLimit, Func<bool> doneWithinTime, Action Exec)
        => starter.StartCoroutine(ExecuteByCondition(starter, doneWithinTime, Exec, true, timeLimit));

    // Ejecuta cada vez que la condicion se cumpla
    public static void ExecuteWhenever(this MonoBehaviour starter, Func<bool> condition, Action Exec, Func<bool> cancelCondition = default)
    {
        starter.StartCoroutine(ExecuteByCondition(starter,condition,Exec,true,999,true,cancelCondition));
    }

    public static IEnumerator ExecuteByCondition(MonoBehaviour starter, Func<bool> condition, Action Exec, bool requireCondition = true, float span = 999, bool repeat = false, Func<bool> cancelCondition = default)
    {
        if (cancelCondition == default) cancelCondition = () => false;
        var timeSpan = BuildTimeSpan(span);

        if (requireCondition) 
        foreach (var frame in timeSpan)
        {
            if (!condition())
            {
                if (cancelCondition()) break;
                else yield return frame;
            }
            else if (!cancelCondition())
            {
                Exec();

                if (repeat)
                { 
                    starter.StartCoroutine(ExecuteByCondition(starter, 
                                                            condition, 
                                                            Exec, 
                                                            requireCondition, 
                                                            span, 
                                                            repeat, 
                                                            cancelCondition));
                }

                break;
            }

        }
        else 
        foreach (var frame in timeSpan)
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

    // Devuelve los inputs que tengamos asignados para cada accion (esto se podria mejorar
    // con otro ScriptableObject por si despues queremos que el jugador pueda cambiarlos).
    public static bool Inputs(this MonoBehaviour x, MyInputs input) 
    {
        switch (input) 
        {
            case MyInputs.Up:
                return Input.GetKey(KeyCode.W);
            case MyInputs.Down:
                return Input.GetKey(KeyCode.S);
            case MyInputs.Left:
                return Input.GetKey(KeyCode.A);
            case MyInputs.Right:
                return Input.GetKey(KeyCode.D);

            case MyInputs.MoveSkill:
                return Input.GetKeyDown(KeyCode.Space);
            case MyInputs.PrimarySkill:
                return Input.GetMouseButtonDown(0);
            case MyInputs.SecondarySkill:
                return Input.GetMouseButtonDown(1);

            case MyInputs.Possess:
                return Input.GetKeyDown(KeyCode.Q);
            case MyInputs.UnPossess:
                return Input.GetKeyDown(KeyCode.E);

            case MyInputs.Pause:
                return Input.GetKeyDown(KeyCode.Escape);

            case MyInputs.Secret1:
                return Input.GetKeyDown(KeyCode.F1);
            case MyInputs.Secret2:
                return Input.GetKeyDown(KeyCode.F2);
            case MyInputs.Secret3:
                return Input.GetKeyDown(KeyCode.F3);
            case MyInputs.Secret4:
                return Input.GetKeyDown(KeyCode.F4);

            case MyInputs.Skip:
                return Input.GetKeyDown(KeyCode.F);

            default: return false;
        }

    }

    public static bool AsyncLoader(this MonoBehaviour x, string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        IEnumerator load = x.ExecuteUntilTrue(() => op.isDone, () =>
        {
            Debug.Log($"Loading {sceneName}: {Mathf.Clamp01(op.progress) * 100}%");
        });

        return op.isDone;
    }

    // Spawnea una entidad en el punto indicado (o, si este no existe, el mas cercano a el), le asigna una
    // referencia al objeto que lo spawneo y lo devuelve.
    public static List<SpawnPoint> SpawnPoints = new();
    public static GameObject SpawnAt(this GameObject x, SpawnPoint spawnPoint, float spawnDelay = 0f)
    {
        GameObject spawnedObject = default;

        //if (spawnPoint == default && x is SpawnPoint) spawnPoint = x as SpawnPoint; 

        spawnPoint.WaitAndThen(timeToWait: spawnDelay, () =>
        {
            if (SpawnPoints.Contains(spawnPoint))
            {
                spawnedObject = spawnPoint.enemyManager.GetFromPool
                                                        (spawnPoint.entityToSpawn.GetComponentInChildren<CharacterSkillSet>(),
                                                        spawnPoint.transform.position + new Vector3(spawnPoint.spawnOffset.x, spawnPoint.spawnOffset.y, 0),
                                                        x.transform.rotation);

                //spawnedObject = GameObject.Instantiate(x.gameObject, 
                //                                  spawnPoint.transform.position + new Vector3(spawnPoint.spawnOffset.x, spawnPoint.spawnOffset.y, 0), 
                //                                  x.transform.rotation);
            }
            else
            {
                spawnedObject = spawnPoint.enemyManager.GetFromPool
                                                        (spawnPoint.entityToSpawn.GetComponentInChildren<CharacterSkillSet>(),
                                                        spawnPoint.GetNearest(spawnPoint.gameObject, SpawnPoints).transform.position,
                                                        x.transform.rotation);

                //spawnedObject = GameObject.Instantiate(x.gameObject,
                //                                  spawnPoint.GetNearest(spawnPoint.gameObject, SpawnPoints).transform.position,
                //                                  x.transform.rotation);
            }

            var spawnable = spawnedObject.GetComponentInChildren<Spawnable>();

            spawnable.parentSpawner = spawnPoint;
            spawnable.OnSpawn();
            if(spawnPoint.spawnSound) spawnPoint.spawnSound.Play();
        },
        cancelCondition: () => false);

        return spawnedObject;
    }


    // Devuelve el objeto mas cercano a la referencia de entre una lista de objetos
    public static T GetNearest<T>(this MonoBehaviour x, GameObject nearestTo = default, List<T> among = default)
    {
        GameObject result = default;

        if (nearestTo == default) nearestTo = x.gameObject;
        //if (among == default) among = GameObject.FindObjectsOfType<T>().Select(x => x as GameObject);

        foreach (var item in among.Select(x => x as GameObject))
        {
            if (Vector3.Distance(nearestTo.transform.position, item.transform.position)
            <= Vector3.Distance(nearestTo.transform.position, result.transform.position))
            {
                result = item;
            }
        }

        return result.GetComponent<T>();
    }



}

