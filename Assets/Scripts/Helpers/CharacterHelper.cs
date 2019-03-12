using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class CharacterHelper
    {
        public static Vector2 GetFacing(InputDirection direction)
        {
            var facing = new Vector2();

            switch (direction)
            {
                case InputDirection.DOWN: { facing = Vector2.down; } break;
                case InputDirection.UP: { facing = Vector2.up; } break;
                case InputDirection.LEFT: { facing = Vector2.left; } break;
                case InputDirection.RIGHT: { facing = Vector2.right; } break;
                default: { Debug.Log("Could not update Facing in CharacterHelper.GetFacing()"); } break;
            }

            return facing;
        }

        public static float GetRotation(InputDirection direction)
        {
            float rotation = 0f;

            switch (direction)
            {
                case InputDirection.DOWN: { rotation = 180f; } break;
                case InputDirection.UP: { rotation = 0f; } break;
                case InputDirection.LEFT: { rotation = 90f; } break;
                case InputDirection.RIGHT: { rotation = 270f; } break;
                default: { Debug.Log("Could not update Facing in CharacterHelper.GetFacing()"); } break;
            }

            return rotation;
        }
    }
}
