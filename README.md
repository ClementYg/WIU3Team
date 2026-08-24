WIUTeam3 Documentation

This repository contains the 2D game project for WIU. This markdown aims to explain every feature implemented and how to use them.

-> Interaction System
To make an interactable (the actual interactive gameobject), It requires the concrete implementation of an Interactable (e.g. ItemInteract), a World-Space Canvas that contains the frame
For the ButtonInteract, InspectInteract and Pressure Plates, they use pluggable Interaction ScriptableObjects.
Pressure Plates use Collider2Ds, hence they don't need the Interactable implementation scripts to be triggered. They also have Press and Release actions lists that trigger respectively

-> Inventory System
{Insert Text (e.g. how to add item, remove item, etc)}

-> Dialogue System
The dialogue system requires 3 components to get it to work: CharacterData, DialogueNode and DialogueConversation.
CharacterData stores frontend data for UI and sound, DialogueNode stores the actual dialogue and the following DialogueNode, DialogueConversation stores the dialogue that plays the first time and on repeat
To trigger a DialogueConversation, it can be done using the Interaction API

-> Cutscene System
To create the Cutscene, simply create a Cutscene ScriptableObject and lay out the steps of the cutscene using the inspector
For CutsceneCameraStep, you need to create a new transform or use an existing transform to blend into
The blocking parameter in each step determines whether to trigger the next step while the current step is still ongoing or not
To trigger a Cutscene, it can be done using the Interaction API

-> Puzzle System
{Insert Text (e.g. how to add puzzle into the scene, modify the puzzle, etc)}

-> Quest System
{Insert Text (e.g. how to create a quest, complete a quest, etc)}

-> Bestiary System
{Insert Text (e.g. how to create an entry, unlock an entry etc}
