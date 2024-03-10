using System;
using UnityEngine.Events;

public class EventHandler
{
    // 1. C# events/delegates 
    // 2. C# events/delegates - Action, Func, Predicate
    // 3. UnityEvents 
    // 4. ScriptableObject Events


    // 1. C# events/delegates                                               // using System is required for delegates & events 
    // event can be subscribe and unsubscribe from different scripts but the event script can only call the event
    // delegate can be subscribe and unsubscribe from different scripts but the delegate can be called by any other script

    public delegate void OnDelegateCall();
    public static OnDelegateCall delegateCall;         // can be Invoke by other script as its an delegate

    public delegate void OnEventCall();
    public static event OnEventCall eventCall;         // cannot be Invoke by other script as its a event

    void DelegateEventFunction()
    {
        delegateCall += DelegateFunction;
        eventCall += EventFunction;

        delegateCall?.Invoke();
        eventCall?.Invoke();
    }

    private void DelegateFunction()
    {
        Console.Write("Delegate Function");
    }

    private void EventFunction()
    {
        Console.Write("Event Function");
    }

    // 2. C# events/delegates - Action, Func, Predicate

    public static event Action actionEvent;                    // Action can have 0 - 16 parameters and dose not return anything  - cannot be Invoke by other script as its a event 
    public static Action actionDelegate;                       // Action can have 0 - 16 parameters and dose not return anything  - can be Invoke by other script as its an delegate

    public static event Func<bool> funcBool;                   // Func can have 0 - 16 parameters and it returns a value 
    public static event Func<float, int> func;                 // check why dose this not work
    public static event Predicate<bool> predicate;             // Predicate can have 0 - 16 parameters and it returns a bool 

    private void CSharpEventFunctions()
    {
        actionEvent += actionFunction;
        actionEvent?.Invoke();

        //func += () => funcFunction(1.45f);
        //func?.Invoke();

        funcBool += () => funcBoolFunction(false);
        funcBool?.Invoke();

        predicate += predicateFunction;
        predicate?.Invoke(false);
    }

    private void actionFunction() { Console.Write("action function called"); }

    private int funcFunction(float value) { 
        Console.Write("action function called");
        return (int)value;
    }

    private bool funcBoolFunction(bool value)
    {
        Console.Write("action function called");
        return value;
    }
    private bool predicateFunction(bool a)
    {
        Console.Write("action function called");
        return false;
    }


    // 3. UnityEvent                                                                              // using UnityEngine.Events is required for UnityEvents
    // Unity events work in the same way as delegates 
    // but they allow you to create modular functionality in the inspector
    // they are avaible in the insceptor only if they are public
    public UnityEvent uEvent;                               
    
    // Unity Event can have max 4 parameters 
    public MyIntEvent intEvent;
    public MyFloatEvent floatEvent;
    public MyStringEvent stringEvent;
    public MyBoolEvent boolEvent;

    void UnityEventFunctions()
    {
        uEvent.AddListener(uEventFunction);
        uEvent?.Invoke();

        intEvent?.Invoke(1);
        floatEvent?.Invoke(1.0f, 1);
        stringEvent?.Invoke("-", 1.0f, 1);
        boolEvent?.Invoke(true, "-", 1.0f, 1);
    }

    public void uEventFunction() { }

    public void intEventFunction(int intV) { }
    public void floatEventFunction(float floatV, int intV) { }
    public void stringEventFunction(string stringV, float floatV, int intV) { }
    public void boolEventFunction(bool boolV, string stringV, float floatV, int intV) { }


    // 4. ScriptableObject Events
}

[System.Serializable]
public class MyIntEvent : UnityEvent<int> { }
[System.Serializable]
public class MyFloatEvent : UnityEvent<float, int> { }
[System.Serializable]
public class MyStringEvent : UnityEvent<string, float, int> { }
[System.Serializable]
public class MyBoolEvent : UnityEvent<bool, string, float, int> { }

class OtherScript
{
    void Function()
    {
        EventHandler.actionEvent += Function;
        EventHandler.actionDelegate += Function;

        //EventHandler.actionEvent?.Invoke();                            this wont work as other script cannot Invoke events
        EventHandler.actionDelegate?.Invoke();

        EventHandler.delegateCall += DelegateFunctionOther;
        EventHandler.eventCall += EventFunctionOther;

        EventHandler.delegateCall?.Invoke();
        //EventHandler.eventCall?.Invoke();                 this wont work as other script cannot Invoke events
    }

    private void DelegateFunctionOther()
    {
        Console.Write("Delegate Function");
    }

    private void EventFunctionOther()
    {
        Console.Write("Event Function");
    }
}
