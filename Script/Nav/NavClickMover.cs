﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace surfm.tool {
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavClickMover : MonoBehaviour {

        public LayerMask mask;
        private NavMeshAgent agent;
        private IReactiveProperty<Vector3> targetPos = new ReactiveProperty<Vector3>(Vector3.zero);
        private IReactiveProperty<float> moveSpeed = new ReactiveProperty<float>(30f);
        public Func<bool> onBeforeMove = () => false;


        void Awake() {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start() {
            targetPos.Value = transform.position;
            moveSpeed.Subscribe(mv => agent.speed = mv);
        }

        protected virtual void Update() {
            onNewMoveTo();
            moveToTarget();
        }

        private void moveToTarget() {
            agent.SetDestination(targetPos.Value);
        }

        private void onNewMoveTo() {
            if (!Input.GetMouseButtonUp(0)) return;
            if (onBeforeMove()) return;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, mask)) {
                Debug.Log("Col pos=" + hit.point);
                targetPos.Value = hit.point;
            }
        }

    }
}
