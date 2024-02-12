/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                                     //
//  ____ ___      .__  __           _________            .___.__                   ________      .__    .___       ._. //
// |    |   \____ |__|/  |_ ___.__. \_   ___ \  ____   __| _/|__| ____    ____    /  _____/ __ __|__| __| _/____   | | //
// |    |   /    \|  \   __<   |  | /    \  \/ /  _ \ / __ | |  |/    \  / ___\  /   \  ___|  |  \  |/ __ |/ __ \  | | //
// |    |  /   |  \  ||  |  \___  | \     \___(  <_> ) /_/ | |  |  \|  \/ /_/  > \    \_\  \  |  /  / /_/ \  ___/   \| //
// |______/|___|  /__||__|  / ____|  \______  /\____/\____ | |__|___|  /\___  /   \______  /____/|__\____ |\___  >  __ //
//              \/          \/              \/            \/         \//_____/           \/              \/    \/   \/ //
//                                                                                                                     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Disclaimer: This is only a guide and example coding convention for our finals project, EMC205!

// Using Lines:
// - Keep using lines at the top of the file
// - using should be outside namespaces
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// CLASSES or STRUCTS:
// - Name them with nouns or noun phrases.
// - Avoid prefixes.
// - Avoid abbreviation.
// - One MonoBehaviour per file only. If you have a MonoBehaviour in a file, the source file name must match.
public class StyleExample : MonoBehaviour
{
    // FIELDS:
    // - Avoid special characters (backslashes, symbols, Unicode characters) - these could interefere with command line tools
    // - Use nouns for names, but prefix booleans with a verb.
    // - Use meaningful names. Make names searchable and pronounceable.
    // - DO NOT abbreviate (unless it's math).
    // - Use Pascal case for public fields. 
    // - Use camel case for private variables.
    // - Add an underscore (_) in front of private fields to differentiate from local variables
    // - Make sure to be consistent with our style guide!

    private int _elapsedTimeInDays;
    int _elapsedTime;

    // Use [SerializeField] attribute if you want to display a private field in the Inspector.
    // Booleans ask a question that can be answered with true or false.
    [SerializeField]
    private bool _isPlayerDead;

    // A tooltip can replace a comment on a serialized field and do double duty.
    [Tooltip("This is another statistic for the player.")]
    [SerializeField] private float _anotherStat;

    // PROPERTIES:
    // - Preferable to a public field.
    // - Pascal case, without special characters.
    // - Be specific in naming!
    // - Do not abbreviate!

    // the private backing field.
    private int _maxHealth;

    // - DO NOT just type mBR or respawn.
    // - Public member variables are PascalCase.
    public int MinutesBetweenRespawn;

    // FUNCTIONS and SUMMARY:
    // - use PascalCase.
    // - use the Allman brace indentation style

    //EXAMPLE: Allman or BSD style puts opening brace on a new line.

    /// <summary>
    /// This function checks the max health of the player.
    /// To use this, just type /// three times.
    /// </summary>
    public void CheckMaxHealth()
    {
        // Local variables are camelCase.
        int _index = 0;

        for(int i = 0; i < _maxHealth; i++)
        {
            print("Max Health: " + _maxHealth);
        }
        _index++;
    }

    // Most of these are available on https://www.youtube.com/watch?v=GEOqwtzmeP0
}
