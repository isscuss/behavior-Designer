﻿using UnityEngine;
#if CROSS_PLATFORM_INPUT
using UnityStandardAssets.CrossPlatformInput;
#endif


namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
    [TaskCategory("Basic/Input")]
    [TaskDescription("Stores the value of the specified axis and stores it in a float.")]
    public class GetAxis : Action
    {
        [Tooltip("The name of the axis")]
        public SharedString axisName;
        [Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
        public SharedFloat multiplier;
        [RequiredField]
        [Tooltip("The stored result")]
        public SharedFloat storeResult;

        public override TaskStatus OnUpdate()
        {
#if CROSS_PLATFORM_INPUT
            var axisValue = CrossPlatformInputManager.GetAxis(axisName.Value);
#else
            var axisValue = Input.GetAxis(axisName.Value);
#endif

            // if variable set to none, assume multiplier of 1
            if (!multiplier.IsNone) {
                axisValue *= multiplier.Value;
            }

            storeResult.Value = axisValue;
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            axisName = "";
            multiplier = 1.0f;
            storeResult = 0;
        }
    }
}