# Properties Read me
Attributes that manipulate with properties being displayed on InspectorGUI are placed under this folder.

# MinMaxSlider
MinMaxSlider is a type of property attribute to place upon Vector2. It uses the x value of the vector2 as the minimum value, and the y value of the vector2 as the maximum value, to create a specified range between them.

Examples:

```csharp
[MinMaxSlider(-1.0f, 1.0f)]
```
This example above will create a slider for a Vector2(float, float) between -1.0f and 1.0f.


```csharp
[MinMaxSlider(-1, 1, true)]
```
This example above will create a integer slider for a Vector2(int, int) between -1 and 1.

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
This example above will display the string field named "testString" on when the bool field named "showProperties" is marked false. Otherwise, hides it.


# EnumHide
EnumHide is an upgraded version of 'ConditionalHide' to place upon any serialized properties. Except it displays the property depending on an Enum. It takes in an additional parameter which is either the index of the expected enum value in integer or the name of it in string.

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
