using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class InputHelper
    {
        public static Vector2 NormalizeInputs(Vector2 inputs)
        {
            var nInputs = inputs.normalized;

            if (nInputs.x > 0.5f) { nInputs.x = 1; }
            else if (nInputs.x <= 0.5f && nInputs.x >= -0.5f) { nInputs.x = 0; }
            else if (nInputs.x < -0.5f) { nInputs.x = -1; }

            if (nInputs.y > 0.5f) { nInputs.y = 1; }
            else if (nInputs.y <= 0.5f && nInputs.y >= -0.5f) { nInputs.y = 0; }
            else if (nInputs.y < -0.5f) { nInputs.y = -1; }

            return nInputs;
        }
    }
}
