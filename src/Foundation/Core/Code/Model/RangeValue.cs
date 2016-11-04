namespace Conjunction.Foundation.Core.Model
{
  public class RangeValue
  {
    public RangeValue(object lowerValue, object upperValue)
    {
      LowerValue = lowerValue;
      UpperValue = upperValue;
    }

    public object LowerValue { get; }

    public object UpperValue { get; }
  }
}