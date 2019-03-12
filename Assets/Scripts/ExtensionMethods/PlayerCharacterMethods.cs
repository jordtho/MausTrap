using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.ExtensionMethods
{
    public static class PlayerCharacterMethods
    {
        public static IInteractable ParseInteraction(this IPlayerCharacter playerCharacter)
        {
            RaycastHit2D hit = Physics2D.Linecast(
                (playerCharacter.Rigidbody.position + playerCharacter.Collider.offset) + (playerCharacter.CharacterFacingVector * .625f) + (new Vector2(playerCharacter.CharacterFacingVector.y, playerCharacter.CharacterFacingVector.x) * .375f),
                (playerCharacter.Rigidbody.position + playerCharacter.Collider.offset) + (playerCharacter.CharacterFacingVector * .625f) - (new Vector2(playerCharacter.CharacterFacingVector.y, playerCharacter.CharacterFacingVector.x) * .375f),
                1 << LayerMask.NameToLayer("Object"));
            if (hit.collider != null) { return hit.collider.GetComponent<Interactable>(); }
            else { return null; }
        }

        public static void InvincibilityFrames(this IPlayerCharacter playerCharacter) => throw new NotImplementedException();
    }
}
