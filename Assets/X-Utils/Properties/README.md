# Properties Read me
Attributes that manipulate with properties being displayed on InspectorGUI are placed under this folder.

# MinMaxSlider
MinMaxSlider is a type of property attribute to place upon Vector2. It uses the x value of the vector2 as the minimum value, and the y value of the vector2 as the maximum value, to create a specified range between them. The range can be modified either by dragging the slider bar, or by input the min and max boundary values directly.

Examples:

```csharp
[MinMaxSlider(0f, 1.0f)]
```
![Figure-Properties-MinMaxSlider-1](https://raw.github.com/xylitogum/X-Utils/master/Screenshots/range_example_1.png?raw=true "MinMaxSlider Example 1")

This example above will create a slider for a Vector2(float, float) between -1.0f and 1.0f.


# Disabled
Disable is a type of property attribute that hides the serialized property which it is placed upon.
```csharp
[Disabled]
public string testString;
```
![Figure-Properties-Disabled-1](https://raw.github.com/xylitogum/X-Utils/master/Screenshots/disabled_example_1.png?raw=true "Disabled Example 1")

This example above will display the string field named "testString", but the user is unable to edit its value through InspectorGUI.


# ConditionalHide
ConditionalHide is a type of property attribute to place upon any serialized properties. It reads another specified bool variable (with variable name given in string) and hides or displays the corresponding property depending on that value.

```csharp
public bool showProperties = false;
[ConditionalHide("showProperties", true)]
public string testString;
```
This example above will display the string field named "testString" on when the bool field named "showProperties" is marked true. Otherwise, hides it.

```csharp
public bool showProperties = false;
[ConditionalHide("showProperties", false)]
public string testString;
```
This example above will not hide the field when the bool is set to false, instead disables it.


# EnumHide
EnumHide is an upgraded version of 'ConditionalHide' to place upon any serialized properties, except it displays the property depending on an Enum. It takes in an additional parameter which is either the index of the expected enum value in integer or the name of it in string.

```csharp
public enum MovementType {
  Walk,
  Swim,
  Fly
}
public MovementType movementType = MovementType.Walk;

[EnumHide("movementType", 0, true)]
public float walkSpeed;
```
This example above will display the float field named "walkSpeed" on when the enum field named "movementType" is set to MovementType.Walk. Otherwise, hides it.

[Continues from above exmple]
```csharp
[EnumHide("movementType", 1, true)]
public float swimDepth;
[EnumHide("movementType", 2, true)]
public float flyHeight;
```
This example above will display the float field named "swimDepth" on when the enum field named "movementType" is set to MovementType.Swim, and display the float field named "flyHeight" on when the enum field named "movementType" is set to MovementType.Fly. Otherwise, hides them respectively.

```csharp
[EnumHide("movementType", "Swim", true)]
public float swimDepth;
[EnumHide("movementType", "Fly", true)]
public float flyHeight;
```
This example above will function exactly same as the one above it.


# EnumMask
EnumHide can be placed on Enums that are used as flags (marked with "System.Flags"). It will allow you to toggle the mask on each type, as well as adding "Everything" and "Nothing" options to it. Works similarly as a customized version of "LayerMask".
```csharp
[System.Flags]
public enum WeaponMask
{
    Knife = 1<<0,
    Handgun = 1<<1,
    Shotgun = 1<<2,
    Rifle = 1<<3,
    Lasergun = 1<<4,   
}

[EnumMask]
public WeaponMask allowedWeapons;
```
This example above will display the MaskField in the inspector allowing you to toggle the mask state of each weapon. As the naming explains, the use case here is something like a list of allowed weapons.

