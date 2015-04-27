﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace csrdb
{

	public class SelectionStream :BaseStream, IStream
	{
		public delegate  bool Opfunc(int a, int b);
		protected IStream input;
		protected string attribute;
		protected Opfunc selector;
		protected string arg;

		public SelectionStream (IStream input, string attribute, Opfunc selector, object arg)
		{
			this.input = input;
			this.attribute = attribute;
			this.selector = selector;
			this.arg = arg.ToString();
		}

		public Dictionary<string,string> Next(){
			Dictionary<string,string> tuple = this.input.Next();
			if ( this.selector( Int32.Parse(tuple[this.attribute]), Int32.Parse( this.arg ) ) ) {
				return tuple;
			}

			if (this.input.HasNext()) {
				return this.Next();
			}
			return null;
		}

		public bool HasNext(){
			return this.input.HasNext();
		}

		override public void Close(){
			this.input.Close();
		}
	}
}

