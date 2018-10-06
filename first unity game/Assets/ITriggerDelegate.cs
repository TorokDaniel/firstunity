public delegate void TriggerDelegate(ITriggerDelegate triggerDelegate);
public interface ITriggerDelegate
{
        void AddDelegate(TriggerDelegate @delegate);
}
