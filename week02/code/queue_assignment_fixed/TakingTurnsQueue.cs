/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the
/// back of the queue (per FIFO rules). When GetNextPerson is called, the next person
/// is returned and placed at the back again if they still have turns remaining.
/// A turns value of 0 or less means that the person stays in the queue forever.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the queue with a name and number of turns.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person and return them. Re-enqueue the person if they have
    /// turns remaining or have infinite turns (0 or less). Throws when empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Infinite turns: keep the original value and put the person back.
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // Finite turns: use one turn, then put the person back.
            person.Turns -= 1;
            _people.Enqueue(person);
        }

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
