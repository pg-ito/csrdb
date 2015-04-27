using System;
using System.Collections.Generic;
namespace csrdb
{

	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			string csv1 = "name, age, job\n" +
			                   "清水,17,エンジニア\n" +
			                   "田中,34,デザイナー\n" +
			                   "佐藤, 21,マネージャー";

			string csv2 = "name, rank\n" +
			       "清水,78\n" +
			       "田中,46\n" +
			       "佐藤,33";

			IStream original = new CSVRelationalStream(csv1);
			Console.WriteLine("<p>SELECT * FROM Staff</p>");
			Print(original);
			Console.WriteLine("<hr>");

			original = new CSVRelationalStream(csv2);
			Console.WriteLine("<p>SELECT * FROM Rank</p>");
			Print(original);


			Console.WriteLine("<hr>");
			Console.WriteLine("<p>SELECT * FROM Staff WHERE age > 20</p>");
			var relation1 = new CSVRelationalStream(csv1);
			var relation2 = new SelectionStream(relation1, "age", Operators.GreaterThan, 20);
			Print(relation2);
		}

		public static void Print(IStream st){
			while(st.HasNext()){
				Dictionary<string,string> tuple = st.Next();
				string[] tmp = new string[tuple.Count];
				tuple.Values.CopyTo(tmp, 0);
				Console.WriteLine ( string.Join(",",tmp));
			}
		}

	}
}
