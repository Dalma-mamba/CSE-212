/// <summary>
/// This queue is circular. People are added to the back of the queue and,
/// when they still have turns remaining, are placed at the back again.
/// A turns value of 0 or less means the person has infinite turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a person with a name and number of turns.
    /// </summary>
    public void AddPerson(string name, int turns)
    {
        _people.Enqueue(new Person(name, turns));
    }

    /// <summary>
    /// Get the next person. Finite turns decrease by one; people with
    /// remaining or infinite turns are added to the back of the queue.
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
            // Zero or negative turns represent an infinite number of turns.
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            person.Turns--;
            _people.Enqueue(person);
        }
        // A person with exactly one turn left leaves the queue permanently.

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
