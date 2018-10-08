using UnityEngine;

public delegate void TriggerDelegate(ITriggerDelegate triggerDelegate, Collider collider);
public interface ITriggerDelegate
{
        void AddDelegate(TriggerDelegate @delegate);
}
