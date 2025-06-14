using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Tracking
{
    public class Value<T>
    {
        public T CurrentValue { get; set; }
        public T OldValue { get; set; }

        public virtual bool Changed
        {
            get { return !CurrentValue.Equals(OldValue); }
        }
    }

    public class IntValue : Value<int>
    {
        public override bool Changed
        {
            get { return CurrentValue != OldValue; }
        }
        public bool ChangedTo(int value)
        {
            return Changed && CurrentValue == value;
        }
        public bool ChangedBy(int amount)
        {
            return Changed && (Math.Abs(OldValue - CurrentValue) == amount);
        }
        public bool ChangedByAtLeast(int amount)
        {
            return Changed && (Math.Abs(OldValue - CurrentValue) >= amount);
        }

        public bool RaisedAbove(int value)
        {
            return Changed && (OldValue <= value) && (CurrentValue > value);
        }
        public bool RaisedToOrAbove(int value)
        {
            return Changed && (OldValue < value) && (CurrentValue >= value);
        }
        public bool RaisedTo(int value)
        {
            return Changed && (OldValue < value) && (CurrentValue == value);
        }
        public bool RaisedBy(int amount)
        {
            return Changed && (OldValue < CurrentValue) && (Math.Abs(OldValue - CurrentValue) == amount);
        }
        public bool RaisedByAtLeast(int amount)
        {
            return Changed && (OldValue < CurrentValue) && (Math.Abs(OldValue - CurrentValue) >= amount);
        }

        public bool LoweredBelow(int value)
        {
            return Changed && (OldValue >= value) && (CurrentValue < value);
        }
        public bool LoweredToOrBelow(int value)
        {
            return Changed && (OldValue > value) && (CurrentValue <= value);
        }
        public bool LoweredTo(int value)
        {
            return Changed && (OldValue > value) && (CurrentValue == value);
        }
        public bool LoweredBy(int amount)
        {
            return Changed && (OldValue > CurrentValue) && (Math.Abs(OldValue - CurrentValue) == amount);
        }
        public bool LoweredByAtLeast(int amount)
        {
            return Changed && (OldValue > CurrentValue) && (Math.Abs(OldValue - CurrentValue) >= amount);
        }

        public bool IsBetween(int lowValue, int highValue)
        {
            return CurrentValue > lowValue && CurrentValue < highValue;
        }
        public bool ChangedToBetween(int lowValue, int highValue)
        {
            return Changed && !(OldValue > lowValue && OldValue < highValue) && (CurrentValue > lowValue && CurrentValue < highValue);
        }

        public bool IsAmong(params int[] values)
        {
            return IsAmong(values);
        }
        public bool IsAmong(IEnumerable<int> values)
        {
            return values.Any(i => i == CurrentValue);
        }
        public bool ChangedToAmong(params int[] values)
        {
            return ChangedToAmong(values);
        }
        public bool ChangedToAmong(IEnumerable<int> values)
        {
            return values.All(i => i != OldValue) && values.Any(i => i == CurrentValue);
        }
    }

    public class FloatValue : Value<float>
    {
        public override bool Changed
        {
            get { return CurrentValue != OldValue; }
        }
        public bool ChangedTo(float value)
        {
            return Changed && CurrentValue == value;
        }
        public bool ChangedBy(float amount)
        {
            return Changed && (Math.Abs(OldValue - CurrentValue) == amount);
        }
        public bool ChangedByAtLeast(float amount)
        {
            return Changed && (Math.Abs(OldValue - CurrentValue) >= amount);
        }

        public bool RaisedAbove(float value)
        {
            return Changed && (OldValue <= value) && (CurrentValue > value);
        }
        public bool RaisedToOrAbove(float value)
        {
            return Changed && (OldValue < value) && (CurrentValue >= value);
        }
        public bool RaisedTo(float value)
        {
            return Changed && (OldValue < value) && (CurrentValue == value);
        }
        public bool RaisedBy(float amount)
        {
            return Changed && (OldValue < CurrentValue) && (Math.Abs(OldValue - CurrentValue) == amount);
        }
        public bool RaisedByAtLeast(float amount)
        {
            return Changed && (OldValue < CurrentValue) && (Math.Abs(OldValue - CurrentValue) >= amount);
        }

        public bool LoweredBelow(float value)
        {
            return Changed && (OldValue >= value) && (CurrentValue < value);
        }
        public bool LoweredToOrBelow(float value)
        {
            return Changed && (OldValue > value) && (CurrentValue <= value);
        }
        public bool LoweredTo(float value)
        {
            return Changed && (OldValue > value) && (CurrentValue == value);
        }
        public bool LoweredBy(float amount)
        {
            return Changed && (OldValue > CurrentValue) && (Math.Abs(OldValue - CurrentValue) == amount);
        }
        public bool LoweredByAtLeast(float amount)
        {
            return Changed && (OldValue > CurrentValue) && (Math.Abs(OldValue - CurrentValue) >= amount);
        }

        public bool IsBetween(float lowValue, float highValue)
        {
            return CurrentValue > lowValue && CurrentValue < highValue;
        }
        public bool ChangedToBetween(float lowValue, float highValue)
        {
            return Changed && !(OldValue > lowValue && OldValue < highValue) && (CurrentValue > lowValue && CurrentValue < highValue);
        }

        public bool IsAmong(params float[] values)
        {
            return IsAmong(values);
        }
        public bool IsAmong(IEnumerable<float> values)
        {
            return values.Any(i => i == CurrentValue);
        }
        public bool ChangedToAmong(params float[] values)
        {
            return ChangedToAmong(values);
        }
        public bool ChangedToAmong(IEnumerable<float> values)
        {
            return values.All(i => i != OldValue) && values.Any(i => i == CurrentValue);
        }
    }
}
