Details for how to build prefabs
Specifically, which scripts should be on which gears, and whether they should be active
Gears that are in the ToolBox (ie, all placeable gear prefabs) should have these states:

Gear (A)
ToolBoxGearState(A)
InPlaceGearState(I)
HeldGearState(I)
Rotater(I)

A=Active, I=Inactive (checkbox)

They can also have whatever individual scripts they need to operate (ex: NullGearScript)
Gears that are placeable, and cannot be destroyed after placing (ex: NullGear) additionally need the InactiveGearState (I)

Gears that start in the scene and cannot be destroyed do not actually need any GearState scripts so they can just have
Rotater(A) 
and whatever scripts they need