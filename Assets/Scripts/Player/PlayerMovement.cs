﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Touch controls for mobile
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Vector2 touchPos, pos;
    [SerializeField] private bool cursorMode;
    [SerializeField] private float speed;
    [SerializeField] private float yOffset;
    
    public bool _isInPlayerZone;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (!cursorMode)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = _camera.ScreenToWorldPoint(touch.position);
                touchPos = new Vector2(touchPos.x, touchPos.y + yOffset);

                if (_isInPlayerZone)
                {
                    transform.position = Vector2.Lerp(transform.position, touchPos, Time.deltaTime * speed);
                }

                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector3.forward);
                if (hit.collider != null)
                {
                    if (hit.transform.CompareTag("TouchField"))
                    {
                        _isInPlayerZone = true;
                    }
                }
            }
            else if (Input.touchCount == 0)
            {
                _isInPlayerZone = false;
            }
        }
        else
        {

            if (Input.GetMouseButton(0))
            {

                pos = Input.mousePosition;
                pos = _camera.ScreenToWorldPoint(pos);
                pos = new Vector2(pos.x, pos.y + yOffset);

                if (_isInPlayerZone)
                {
                    transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * speed);

                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.forward);
                if (hit.collider != null)
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        _isInPlayerZone = true;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isInPlayerZone = false;
            }

        }
    }
}
