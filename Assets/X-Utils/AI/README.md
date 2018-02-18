# Mechanim State Machine AI

This folder contains utility scripts that are useful for using Unity's Mechanim as the Finite State Machine on AI agents.

# Mechanim
Unity has a built-in StateMachine called "Mechanim" that is used to create state transitions of animations. It provides a set of visualization tools that you can create states and transitions in an efficient manner.
Thus, We can repurpose it and use it for creating State Machine AI in our games.

To use Mechanim as AI agent, do the following steps.
- Add "Animator" component to your AI agent. 
- Create a new "AnimatorController" under the unity project. Set the "Controller" field of the Animator Component to that controller.
- Clicks on the AnimatorController to edit it. You should be able to see a window where you can create and drag states.
- Create the states accordingly. For each states you created, assign a proper script that inherits from "CoreLogicBehaviour". 
- See "ExampleBehaviour" to understand how to create those behaviour scripts.
- Note: You can use the same AnimatorController that controls the actual animation of that character. Just remember to add a new layer.


# StateMachine Behaviour

The CoreLogicBehaviour functions as a StateMachine Behaviour script. During a state, it executes functions like OnStateEnter, OnStateUpdate and OnStateExit that you implemented by inheriting from it. You can also store necessary general information you want to retrive from the agent to that class.

Note that it is only advisible to make transitions by the "Mechanim" animator editor window, though it can only use certain types of parameters like floats or triggers. You can always workaround it by calling a trigger parameter when you feel the a state transition is sufficient to happen, but keep in mind that, decoupling state behaviours from state transitions could be a good idea in terms of system designing.

