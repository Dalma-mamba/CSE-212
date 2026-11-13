/// <summary>
/// A basic implementation of a FIFO queue of people.
/// </summary>
public class PersonQueue
{
    private readonly Queue<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the back of the queue.
    /// </summary>
    /// <param name="person">The person to add</param>
    public void Enqueue(Person person)
    {
        _queue.Enqueue(person);
    }

    /// <summary>
    /// Remove and return the person at the front of the queue.
    /// </summary>
    public Person Dequeue()
    {
        return _queue.Dequeue();
    }

    public bool IsEmpty()
    {
        return _queue.Count == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}
