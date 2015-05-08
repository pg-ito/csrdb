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

			{
				Console.WriteLine ("<hr>");
				Console.WriteLine ("<p>SELECT * FROM Staff, Rank WHERE staff.name = rank.name</p>");
				var in1 = new CSVRelationalStream (csv1);
				var in2 = new CSVRelationalStream (csv2);
				var in3 = new JoinStream (in1, "name", in2, "name", Operators.Equal);
				Print (in3);
			}
			{
				Console.WriteLine("<hr>");
				Console.WriteLine("<p>SELECT * FROM Staff CROSS JOIN Rank</p>");
				var in1 = new CSVRelationalStream(csv1);
				var in2 = new CSVRelationalStream(csv2);
				var in3 = new CrossJoinStream(in1, new RenameStream(in2, "name", "name2"));
				Print(in3);
			}
		}

		public static void Print(IStream st){
			bool isFirstLine = true;
			while(st.HasNext()){
				Dictionary<string,string> tuple = st.Next();
				if (isFirstLine) {
					string[] header = new List<string>( tuple.Keys).ToArray();
					Console.WriteLine ( string.Join(",",header));
					isFirstLine = false;
				}
				string[] tmp = new List<string> (tuple.Values).ToArray ();
				Console.WriteLine ( string.Join(",",tmp));
			}
		}

	}
}
