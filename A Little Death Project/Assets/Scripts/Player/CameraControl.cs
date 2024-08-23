using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HACE QUE LA CAMARA SIGA AL PLAYER EN PERSPECTIVA TOPDOWN SIN ROTAR CON �L.

public class CameraControl : MonoBehaviour
{
    // Se obtiene la ubicaci�n del jugador y se determinan valores con los que regular el movimiento
    // de la c�mara.
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _camOffset;
    [SerializeField] float _followDistance;
    [SerializeField] float _camMoveSpeed;
    [SerializeField] public Quaternion camRotation;
    public Vector3 pos;

    private void Update()
    {

        // Para obtener el movimiento de la c�mara se utiliza un vector cuyo valor interpola entre:
        // - Su posici�n actual
        // - La posici�n del player, de la cual logra distanciarse por el offset XYZ y la distancia de
        // seguimiento en relaci�n a la espalda del jugador (por eso el -transform.forward)
        // - Todo lo cual se actualiza cada frame a la velocidad de movimiento que hayamos establecido.
        if (_player != null)
        {
            pos = Vector3.Lerp(transform.position, _player.position + _camOffset + -transform.forward
                                     * _followDistance, _camMoveSpeed * Time.deltaTime);

            //Una vez hayamos obtenido ese vector, se lo iguala a la posici�n de la c�mara.
            transform.position = pos;

            // La rotaci�n depende m�s de nosotros que del player, asi que simplemente se crea un quaternion
            // com�n y corriente para ajustarla.
            transform.rotation = camRotation;
        }
    }

}
