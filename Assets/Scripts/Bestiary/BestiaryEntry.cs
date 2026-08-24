using UnityEngine;

//REF:
//An interface is a syntax similar to abstract, but all of its functions will 
//only be implemented later on by inheritance.

public interface BestiaryEntry
{
    string EntryID { get; }
    string DisplayName { get; }
    string Description { get; }
    Sprite Icon { get; }
    BestiaryCategory Category { get; }

}