using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator
{
	public class InputList
	{
		private Component component;

		private List<Tuple<object, Type>> inputs;

		public InputList(Component c)
		{
			inputs = new List<Tuple<object, Type>>();

			component = c;
		}

		public int Create<T>()
		{
			inputs.Add(new Tuple<object,Type>(new Input<T>(component), typeof(T)));
			return inputs.Count - 1;
		}
		
		public Input<T> Fetch<T>(int idx)
		{
			return inputs[idx].Item1 as Input<T>;
		}

		public int Count { get => inputs.Count; }
	}
	public class Input<T>
		: IUpdateable
	{
		private static int count = 0;

		private T value;
		private readonly int id;

		Component component;
		public Input(Component c)
		{
			id = count++;
			component = c;
		}

		public void Update()
		{
			component.Update();
			/*
			if (value.CompareTo(newValue) != 0)
			{
				value = newValue;
				component.Update();
			}
			*/
		}

		public void SetValue(T newValue)
		{
			value = newValue;
			Update();
		}

		public T Value { get => value; set => this.value = value; }
		public int ID { get => id; }
	}
	public class InputInt
	{
		private static int _count = 0;

		private int _value;
		private readonly int _id;

		private Component component;

		public InputInt(Component c)
		{
			_id = _count++;
			component = c;
			_value = 0;
		}

		public void Update(int newValue)
		{
			if (_value != newValue)
			{
				_value = newValue;
				component.Update();
			}
		}

		public int Value { get => _value; }
		public int ID { get => _id; }
	}
}
