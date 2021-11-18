using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator
{
	public class OutputList
	{
		private Component component;

		private List<Tuple<object, Type>> outputs;

		public OutputList(Component c)
		{
			outputs = new List<Tuple<object, Type>>();

			component = c;
		}

		public int Create<T>(T defaultValue)
		{
			outputs.Add(new Tuple<object,Type>(new Output<T>(component, defaultValue), typeof(T)));
			return outputs.Count - 1;
		}
		
		public Output<T> Fetch<T>(int idx)
		{
			return (Output<T>) outputs[idx].Item1;
		}

		public void Update()
		{
			for (int i = 0; i < Count; i++)
			{
				IUpdateable toUpdate = outputs[i].Item1 as IUpdateable;
				toUpdate.Update();
			}
		}

		public int Count { get => outputs.Count; }
	}
	public class Output<T>
		: IUpdateable
	{
		private static int count = 0;

		private T value;
		private readonly int id;

		private Component component;

		private List<Input<T>> inputs;

		public Output(Component c, T defaultValue)
		{
			id = count++;
			component = c;
			value = defaultValue;

			inputs = new List<Input<T>>();
		}

		public void Update()
		{
			if (inputs?.Count > 0)
				foreach (Input<T> input in inputs)
					input.SetValue(value);
		}

		public void Bind(Input<T> input)
		{
			inputs.Add(input);
		}

		public T Value
		{
			get => value;
			set
			{
				this.value = value;
				Update();
			}
		}
		public int ID { get => id; }
	}
	public class OutputInt
	{
		private static int _count = 0;

		private int _value;
		private readonly int _id;

		private Component component;

		private List<InputInt> inputs;

		public OutputInt(Component c, int val)
		{
			_value = val;
			_id = _count++;

			component = c;
		}

		public void Update()
		{
			if (inputs.Count > 0)
				foreach (InputInt ii in inputs)
					ii.Update(_value);
		}

		public void Bind(InputInt input)
		{
			if (inputs == null)
				inputs = new List<InputInt>();

			inputs.Add(input);
		}

		public int Value
		{
			get => _value;
			set {
				if (_value != value)
				{
					_value = value;
					Update();
				}
			}
		}
		public int ID { get => _id; }
	}
}
