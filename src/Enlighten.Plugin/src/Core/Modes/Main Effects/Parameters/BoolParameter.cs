namespace Enlighten.Core
{
	public class BoolParameter : GenericParameter<bool>
	{
		public BoolParameter(bool defaultValue, string name, string description) : base(defaultValue, name, description)
		{ }

		protected override bool InterpolatePoints(int left, int right, float normalTime)
		{
			return SortedKeyframes[left].m_value;
		}
	}
}
