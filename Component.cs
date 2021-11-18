using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator
{
	public abstract class Component
		: IUpdateable
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
		{
			return inputs.Create<T>();
		}
		protected int CreateOutput<T>(T defaultValue)
		{
			return outputs.Create<T>(defaultValue);
		}
		
		public T GetInputValue<T>(int index)
		{
			return inputs.Fetch<T>(index).Value;
		}
		public T GetOutputValue<T>(int index)
		{
			return outputs.Fetch<T>(index).Value;
		}

		public void SetOutput<T>(int index, T newValue)
		{
			outputs.Fetch<T>(index).Value = newValue;
		}

		protected abstract void update();
		public void Update()
		{
			update();
			outputs.Update();
		}
		
		public void Bind<T>(int outputIndex, Component c, int inputIndex)
		{
			outputs.Fetch<T>(outputIndex).Bind(c.inputs.Fetch<T>(inputIndex));
		}
		
		public string Name { get => name; }
		public int ID { get => id; }
	}
}
