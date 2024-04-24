using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSwitch : MonoBehaviour
{
    private TileSwitch[] tileSwitches;    // Array to hold all TileSwitch scripts in the scene
    private TileSwitch2[] tileSwitches2;  // Array to hold all TileSwitch2 scripts in the scene

    void Awake()
    {
        // Find all TileSwitch and TileSwitch2 scripts in the scene
        tileSwitches = FindObjectsOfType<TileSwitch>();
        tileSwitches2 = FindObjectsOfType<TileSwitch2>();

        // Disable all found scripts
        DisableComponents();
    }

    void DisableComponents()
    {
        foreach (var switchScript in tileSwitches)
        {
            switchScript.enabled = false;  // Disables the TileSwitch script
        }
        foreach (var switchScript2 in tileSwitches2)
        {
            switchScript2.enabled = false; // Disables the TileSwitch2 script

            // Also disable BoxCollider2D and SpriteRenderer for these objects
            var boxCollider = switchScript2.GetComponent<BoxCollider2D>();
            var spriteRenderer = switchScript2.GetComponent<SpriteRenderer>();
            if (boxCollider) boxCollider.enabled = false;
            if (spriteRenderer) spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reactivate all the scripts when the player collides with this object
            ReactivateComponents();
            Destroy(gameObject);  // Destroy the current object after reactivating components
        }
    }

    void ReactivateComponents()
    {
        foreach (var switchScript in tileSwitches)
        {
            switchScript.enabled = true;  // Enables the TileSwitch script
        }
        foreach (var switchScript2 in tileSwitches2)
        {
            switchScript2.enabled = true;  // Enables the TileSwitch2 script

            // Re-enable BoxCollider2D and SpriteRenderer if TileSwitch2 script is enabled
            var boxCollider = switchScript2.GetComponent<BoxCollider2D>();
            var spriteRenderer = switchScript2.GetComponent<SpriteRenderer>();
            if (boxCollider) boxCollider.enabled = true;
            if (spriteRenderer) spriteRenderer.enabled = true;
        }
    }
}

