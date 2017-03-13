using UnityEngine;

namespace Academy.HoloToolkit.Unity
{
    /// <summary>
    /// CursorManager class takes Cursor GameObjects.
    /// One that is on Holograms and another off Holograms.
    /// Shows the appropriate Cursor when a Hologram is hit.
    /// Places the appropriate Cursor at the hit position.
    /// Matches the Cursor normal to the hit surface.
    /// </summary>
    public class CursorManager : Singleton<CursorManager>
    {
        [Tooltip("Drag the Cursor object to show when it hits a hologram.")]
        public GameObject CursorOnHolograms;

        [Tooltip("Drag the Cursor object to show when it does not hit a hologram.")]
        public GameObject CursorOffHolograms;

        void Awake()
        {
            if (CursorOnHolograms == null || CursorOffHolograms == null)
            {
                return;
            }

            // Hide the Cursors to begin with.
            CursorOnHolograms.SetActive(false);
            CursorOffHolograms.SetActive(false);

            SetLayerCollisions();
        }

        // This is important so our interactible objects don't collide with each other
        // when we change their sizes using gestures.
        private static void SetLayerCollisions()
        {
            int maxLayers = 31;
            // To protect apps that don't have an Interactible layer in their project.
            int interactibleLayer = LayerMask.NameToLayer("Interactible");

            if (interactibleLayer < 0 || interactibleLayer > maxLayers)
            {
                return;
            }

            // Ignore all collisions with UI except for Cursor collisions.
            // Unity has 31 possible layers.  There is no way to get this value in code.
            for (int i = 0; i < maxLayers; i++)
            {
                // Ensure the Interactible objects do not collide with other layers.
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Interactible"), i, true);
            }

            // Ensures the Cursor can collide with the Interactible objects only.
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Interactible"), LayerMask.NameToLayer("Cursor"), false);
        }

        void Update()
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.b */

            if (GazeManager.Instance == null || CursorOnHolograms == null || CursorOffHolograms == null)
            {
                return;
            }

            if (GazeManager.Instance.Hit)
            {
                // 2.b: SetActive true the CursorOnHolograms to show cursor.
                CursorOnHolograms.SetActive(true);
                // 2.b: SetActive false the CursorOffHolograms hide cursor.
                CursorOffHolograms.SetActive(false);
            }
            else
            {
                // 2.b: SetActive true CursorOffHolograms to show cursor.
                CursorOffHolograms.SetActive(true);
                // 2.b: SetActive false CursorOnHolograms to hide cursor.
                CursorOnHolograms.SetActive(false);
            }

            // 2.b: Assign gameObject's transform position equals GazeManager's instance Position.
            gameObject.transform.position = GazeManager.Instance.Position;

            // 2.b: Assign gameObject's transform up vector equals GazeManager's instance Normal.
            gameObject.transform.up = GazeManager.Instance.Normal;
        }
    }
}