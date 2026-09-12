namespace Interfaces
{
    public interface Interactable
    {
        void Interact();
        void InteractHold();
        void OnFocusEnter();
        void OnFocusExit();

        int priority {get; set;}
    }
}
