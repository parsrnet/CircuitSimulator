using System;

namespace CircuitSimulator
{
	class TestComponent : Component
	{
		public TestComponent() : base("Test")
		{
			CreateInput<int>();
			CreateOutput<int>(1337);
		}

		protected override void update() {
			Console.WriteLine("updated");
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			var test = new TestComponent();
			test.Update();

			Console.WriteLine(test.GetOutputValue<int>(0));
			Console.Read();
		}
	}
}
