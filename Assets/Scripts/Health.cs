using System;

public class Health
{
    public event Action OnDeath;

    private uint _points;
    private uint _min;
    private uint _max;


    public Health(uint min = 0, uint max = 100)
    {
        if (min > max)
        {
            max += min;
            min = max - min;
            max -= min;
        }
        
        _min = min;
        _max = max;

        _points = _max;
    }

    public void Restore(uint value)
    {
        _points += value;

        if (_points > _max)
            _points = _max;
    }

    public void Reduce(uint value)
    {
        if (value >= _points)
        {
            _points = 0;
            OnDeath?.Invoke();
        }
        else
        {
            _points -= value;
        }
    }

    public uint Get()
    {
        return _points;
    }
}
