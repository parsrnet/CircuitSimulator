using System;

namespace CircuitSimulator
{
	class Printer : Component
	{
		public Printer() : base("Printer")
		{
			CreateInput<string>();
		}

		public void Print()
		{
			Console.WriteLine(inputs.Fetch<string>(0).Value);
		}

		protected override void update()
		{
		}
	}
	class IntToString : Component
	{
		public IntToString() : base("ToString")
		{
			CreateInput<int>();
			CreateOutput<string>("");
		}

		protected override void update() {
			SetOutput<string>(0, GetInputValue<int>(0).ToString());
		}
	}
	class Constant<T> : Component
		where T : IComparable
	{
		public Constant(T value) : base("Constant")
		{
			CreateOutput<T>(value);
		}

		protected override void update()
		{
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Constant<int> cInt = new Constant<int>(13371337);

			IntToString toStr = new IntToString();
			cInt.Bind<int>(0, toStr, 0);

			Printer printer = new Printer();
			toStr.Bind<string>(0, printer, 0);

			cInt.Update();

			printer.Print();

			Console.Read();
		}
	}
}
