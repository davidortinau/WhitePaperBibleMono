using System;
using System.Dynamic;
using Newtonsoft.Json;
using NUnit.Framework;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;

namespace WhitePaperBibileCoreTests
{
	public class TestData
	{
		public static string PapersJSON {
			get {
				return JsonConvert.SerializeObject (PaperNodeList);
			}
		}

		public static string TagsJSON {
			get {
				return JsonConvert.SerializeObject (TagNodeList);
			}
		}

		public static List<PaperNode> PaperNodeList {
			get {
				return new List<PaperNode> () {
					new PaperNode () {
						paper = new Paper () {
							title = "Hello World"
						}
					}
				};
			}
		}

		public static List<TagNode> TagNodeList {
			get {
				return new List<TagNode> () {
					new TagNode () {
						tag = new Tag () {
							name = "Faith"
						}
					}
				};
			}
		}
	}
}

