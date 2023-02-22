public interface IInteractive
{
    bool IsAvailable { get; set; }
    void Interact(Entity entity);
}