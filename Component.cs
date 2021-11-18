using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator
{
	public abstract class Component
	{
		private static int count = 0;
		private string name;
		private readonly int id;

		public InputList inputs;
		public OutputList outputs;

		public Component(string _name)
		{
			name = _name;
			id = count++;

			inputs = new InputList(this);
			outputs = new OutputList(this);
		}
		
		protected int CreateInput<T>()
			where T : IComparable
		{
			return inputs.Create<T>();
		}
		protected int CreateOutput<T>(T defaultValue)
			where T : IComparable
		{
			return outputs.Create<T>(defaultValue);
		}
		
		public T GetInputValue<T>(int index)
			where T : IComparable
		{
			return inputs.Fetch<T>(index).Value;
		}
		public T GetOutputValue<T>(int index)
			where T : IComparable
		{
			return outputs.Fetch<T>(index).Value;
		}

		protected abstract void update();
		public void Update()
		{
			update();
			outputs.Update();
		}
		/*
		public void Bind(int outputIndex, Component c, int inputIndex)
		{
			outputs[outputIndex].Bind(c.inputs[inputIndex]);
		}
		*/
		public string Name { get => name; }
		public int ID { get => id; }
	}
}
